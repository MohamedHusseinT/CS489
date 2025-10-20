#!/bin/bash

# AWS EKS Cleanup Script
# This script removes all AWS resources created for the ADS Dental Surgeries deployment

set -e

# Configuration
AWS_REGION="us-east-1"
EKS_CLUSTER_NAME="ads-dental-eks"
ECR_REPO_PREFIX="ads-dental"

echo "🧹 Starting AWS EKS Cleanup..."

# Check if AWS CLI is installed
if ! command -v aws &> /dev/null; then
    echo "❌ AWS CLI is not installed."
    exit 1
fi

# Check if eksctl is installed
if ! command -v eksctl &> /dev/null; then
    echo "❌ eksctl is not installed."
    exit 1
fi

# Delete Kubernetes resources
echo "🗑️ Deleting Kubernetes resources..."
kubectl delete -f dotnet-webapi-service.yaml --ignore-not-found=true
kubectl delete -f dotnet-webapi-deployment.yaml --ignore-not-found=true
kubectl delete -f java-cli-deployment.yaml --ignore-not-found=true

# Delete EKS cluster
echo "☸️ Deleting EKS cluster..."
eksctl delete cluster --name $EKS_CLUSTER_NAME --region $AWS_REGION

# Delete ECR repositories
echo "🐳 Deleting ECR repositories..."
aws ecr delete-repository --repository-name $ECR_REPO_PREFIX/java-cli-app --region $AWS_REGION --force || echo "Repository not found"
aws ecr delete-repository --repository-name $ECR_REPO_PREFIX/dotnet-webapi --region $AWS_REGION --force || echo "Repository not found"

echo "✅ AWS EKS cleanup completed!"
echo "🎉 All resources have been removed."
