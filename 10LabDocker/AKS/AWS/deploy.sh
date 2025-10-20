#!/bin/bash

# AWS EKS Deployment Script
# This script automates the deployment of ADS Dental Surgeries applications to AWS EKS

set -e

# Configuration
AWS_REGION="us-east-1"
EKS_CLUSTER_NAME="ads-dental-eks"
ECR_REPO_PREFIX="ads-dental"

echo "🚀 Starting AWS EKS Deployment..."

# Check if AWS CLI is installed
if ! command -v aws &> /dev/null; then
    echo "❌ AWS CLI is not installed. Please install it first."
    exit 1
fi

# Check if kubectl is installed
if ! command -v kubectl &> /dev/null; then
    echo "❌ kubectl is not installed. Please install it first."
    exit 1
fi

# Check if eksctl is installed
if ! command -v eksctl &> /dev/null; then
    echo "❌ eksctl is not installed. Please install it first."
    exit 1
fi

# Get AWS account ID
AWS_ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
ECR_REGISTRY="$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com"

echo "🔍 AWS Account ID: $AWS_ACCOUNT_ID"
echo "🐳 ECR Registry: $ECR_REGISTRY"

# Check for SSH key, create if not exists
if [ ! -f ~/.ssh/id_rsa.pub ]; then
    echo "🔑 Creating SSH key pair..."
    ssh-keygen -t rsa -b 4096 -f ~/.ssh/id_rsa -N "" -C "eks-$(date +%Y%m%d)"
fi
echo "✅ SSH key available: ~/.ssh/id_rsa.pub"

# Create ECR repositories
echo "🐳 Creating ECR repositories..."
aws ecr create-repository --repository-name $ECR_REPO_PREFIX/java-cli-app --region $AWS_REGION || echo "Repository already exists"
aws ecr create-repository --repository-name $ECR_REPO_PREFIX/dotnet-webapi --region $AWS_REGION || echo "Repository already exists"

# Login to ECR
echo "🔑 Logging into ECR..."
aws ecr get-login-password --region $AWS_REGION | docker login --username AWS --password-stdin $ECR_REGISTRY

# Build and push Docker images
echo "🏗️ Building and pushing Docker images..."

# Build Java CLI app
echo "Building Java CLI app..."
docker build -t $ECR_REGISTRY/$ECR_REPO_PREFIX/java-cli-app:latest ../../JavaCLIApp
docker push $ECR_REGISTRY/$ECR_REPO_PREFIX/java-cli-app:latest

# Build .NET Web API
echo "Building .NET Web API..."
docker build -t $ECR_REGISTRY/$ECR_REPO_PREFIX/dotnet-webapi:latest ../../DotNetWebAPI
docker push $ECR_REGISTRY/$ECR_REPO_PREFIX/dotnet-webapi:latest

# Check if EKS cluster exists, create if not
echo "☸️ Checking EKS cluster status..."
if eksctl get cluster --name $EKS_CLUSTER_NAME --region $AWS_REGION &> /dev/null; then
    echo "✅ EKS cluster '$EKS_CLUSTER_NAME' already exists, checking node groups..."
    
    # Update kubeconfig first
    echo "⚙️ Updating kubeconfig..."
    aws eks update-kubeconfig --name $EKS_CLUSTER_NAME --region $AWS_REGION
    
    # Check if node group exists
    if eksctl get nodegroup --cluster=$EKS_CLUSTER_NAME --region=$AWS_REGION --name=workers &> /dev/null; then
        echo "✅ Node group 'workers' already exists"
    else
        echo "🚀 Creating worker node group with comprehensive configuration..."
        
        # Clean up any failed CloudFormation stacks first
        echo "🧹 Cleaning up any failed CloudFormation stacks..."
        if aws cloudformation describe-stacks --stack-name eksctl-ads-dental-eks-nodegroup-workers --region $AWS_REGION &> /dev/null; then
            echo "🗑️ Deleting failed CloudFormation stack..."
            aws cloudformation delete-stack --stack-name eksctl-ads-dental-eks-nodegroup-workers --region $AWS_REGION
            echo "⏳ Waiting for stack deletion to complete..."
            aws cloudformation wait stack-delete-complete --stack-name eksctl-ads-dental-eks-nodegroup-workers --region $AWS_REGION || echo "Stack deletion completed or timed out"
        else
            echo "No failed stack to clean up"
        fi
        
        # First, ensure AWS VPC CNI addon is installed
        echo "🔧 Installing AWS VPC CNI addon..."
        eksctl create addon --name vpc-cni --cluster=$EKS_CLUSTER_NAME --region=$AWS_REGION --force || echo "Addon may already exist"
        
        # Create nodegroup with proper configuration
        eksctl create nodegroup \
          --cluster=$EKS_CLUSTER_NAME \
          --region=$AWS_REGION \
          --name=workers \
          --node-type=t3.medium \
          --nodes=2 \
          --nodes-min=1 \
          --nodes-max=3 \
          --managed \
          --subnet-ids=subnet-089dbcd513d750829,subnet-073c04cf215921099 \
          --ssh-access \
          --ssh-public-key=~/.ssh/id_rsa.pub
    fi
else
    echo "🚀 Creating EKS cluster with node group..."
    eksctl create cluster \
      --name $EKS_CLUSTER_NAME \
      --region $AWS_REGION \
      --nodegroup-name workers \
      --node-type t3.medium \
      --nodes 2 \
      --nodes-min 1 \
      --nodes-max 3 \
      --managed \
      --with-oidc \
      --ssh-access \
      --ssh-public-key=~/.ssh/id_rsa.pub
fi

# Update kubeconfig to connect to the cluster
echo "⚙️ Updating kubeconfig..."
aws eks update-kubeconfig --name $EKS_CLUSTER_NAME --region $AWS_REGION

# Wait for nodes to be ready
echo "⏳ Waiting for worker nodes to be ready..."
kubectl wait --for=condition=Ready nodes --all --timeout=300s

# Deploy applications
echo "🚀 Deploying applications to Kubernetes..."

# Update image references in YAML files
sed -i '' "s/<ACCOUNT_ID>/$AWS_ACCOUNT_ID/g" java-cli-deployment.yaml
sed -i '' "s/<ACCOUNT_ID>/$AWS_ACCOUNT_ID/g" dotnet-webapi-deployment.yaml

# Deploy Java CLI app
kubectl apply -f java-cli-deployment.yaml

# Deploy .NET Web API
kubectl apply -f dotnet-webapi-deployment.yaml
kubectl apply -f dotnet-webapi-service.yaml

# Wait for deployments to be ready
echo "⏳ Waiting for deployments to be ready..."
kubectl wait --for=condition=available --timeout=300s deployment/java-cli-app
kubectl wait --for=condition=available --timeout=300s deployment/dotnet-webapi

# Get service information
echo "🌐 Getting service information..."
kubectl get services

# Get external IP
EXTERNAL_IP=$(kubectl get service dotnet-webapi-service -o jsonpath='{.status.loadBalancer.ingress[0].hostname}')
if [ -n "$EXTERNAL_IP" ]; then
    echo "✅ Deployment completed successfully!"
    echo "🌐 .NET Web API is available at: http://$EXTERNAL_IP/swagger"
    echo "📊 Swagger UI: http://$EXTERNAL_IP/swagger"
    echo "🔗 API Endpoints:"
    echo "   - GET http://$EXTERNAL_IP/adsweb/api/v1/patients"
    echo "   - GET http://$EXTERNAL_IP/adsweb/api/v1/addresses"
else
    echo "⚠️ External IP not yet assigned. Please check with: kubectl get services"
fi

echo "🎉 AWS EKS deployment completed!"
echo "📝 To clean up resources, run: ./cleanup.sh"
