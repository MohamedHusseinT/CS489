#!/bin/bash

# Azure AKS Cleanup Script
# This script removes all Azure resources created for the ADS Dental Surgeries deployment

set -e

# Configuration
RESOURCE_GROUP="ads-dental-rg"
AKS_NAME="ads-dental-aks"

echo "🧹 Starting Azure AKS Cleanup..."

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    echo "❌ Azure CLI is not installed."
    exit 1
fi

# Login to Azure (if not already logged in)
if ! az account show &> /dev/null; then
    echo "Please login to Azure..."
    az login
fi

# Delete Kubernetes resources
echo "🗑️ Deleting Kubernetes resources..."
kubectl delete -f dotnet-webapi-service.yaml --ignore-not-found=true
kubectl delete -f dotnet-webapi-deployment.yaml --ignore-not-found=true
kubectl delete -f java-cli-deployment.yaml --ignore-not-found=true

# Delete AKS cluster
echo "☸️ Deleting AKS cluster..."
az aks delete --resource-group $RESOURCE_GROUP --name $AKS_NAME --yes

# Delete resource group (this will delete all resources in the group)
echo "📦 Deleting resource group and all resources..."
az group delete --name $RESOURCE_GROUP --yes

echo "✅ Azure AKS cleanup completed!"
echo "🎉 All resources have been removed."
