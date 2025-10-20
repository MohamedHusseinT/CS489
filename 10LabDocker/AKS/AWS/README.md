# AWS EKS Deployment Configuration

## Overview
This directory contains Kubernetes manifests and deployment scripts for deploying the ADS Dental Surgeries applications to Amazon Elastic Kubernetes Service (EKS).

## Applications
- **Java CLI Application**: Simple Hello World application
- **.NET Web API**: RESTful API for dental surgery management

## Prerequisites
- AWS CLI installed and configured
- kubectl installed
- Docker images pushed to Amazon Elastic Container Registry (ECR)
- eksctl installed (recommended)

## Deployment Steps

### 1. Create Amazon ECR Repositories
```bash
# Create ECR repositories
aws ecr create-repository --repository-name ads-dental/java-cli-app
aws ecr create-repository --repository-name ads-dental/dotnet-webapi

# Get login token
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin <ACCOUNT_ID>.dkr.ecr.us-east-1.amazonaws.com
```

### 2. Build and Push Docker Images
```bash
# Build and tag images
docker build -t <ACCOUNT_ID>.dkr.ecr.us-east-1.amazonaws.com/ads-dental/java-cli-app:latest ./JavaCLIApp
docker build -t <ACCOUNT_ID>.dkr.ecr.us-east-1.amazonaws.com/ads-dental/dotnet-webapi:latest ./DotNetWebAPI

# Push images
docker push <ACCOUNT_ID>.dkr.ecr.us-east-1.amazonaws.com/ads-dental/java-cli-app:latest
docker push <ACCOUNT_ID>.dkr.ecr.us-east-1.amazonaws.com/ads-dental/dotnet-webapi:latest
```

### 3. Create EKS Cluster
```bash
# Create EKS cluster using eksctl
eksctl create cluster \
  --name ads-dental-eks \
  --region us-east-1 \
  --nodegroup-name workers \
  --node-type t3.medium \
  --nodes 2 \
  --nodes-min 1 \
  --nodes-max 3 \
  --managed

# Or using AWS CLI
aws eks create-cluster \
  --name ads-dental-eks \
  --role-arn arn:aws:iam::<ACCOUNT_ID>:role/eksServiceRole \
  --resources-vpc-config subnetIds=subnet-12345,subnet-67890,securityGroupIds=sg-12345
```

### 4. Deploy Applications
```bash
# Deploy Java CLI application
kubectl apply -f java-cli-deployment.yaml

# Deploy .NET Web API
kubectl apply -f dotnet-webapi-deployment.yaml
kubectl apply -f dotnet-webapi-service.yaml
```

### 5. Access Applications
```bash
# Get external IP for .NET Web API
kubectl get services

# Access Swagger UI
# http://<EXTERNAL-IP>:80/swagger
```

## Files
- `java-cli-deployment.yaml`: Kubernetes deployment for Java CLI app
- `dotnet-webapi-deployment.yaml`: Kubernetes deployment for .NET Web API
- `dotnet-webapi-service.yaml`: Kubernetes service for .NET Web API
- `deploy.sh`: Automated deployment script
- `cleanup.sh`: Cleanup script to remove resources
