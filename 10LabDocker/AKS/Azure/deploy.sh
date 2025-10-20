#!/bin/bash

# Azure AKS Deployment Script
# This script automates the deployment of ADS Dental Surgeries applications to Azure AKS

set -e

# Configuration
RESOURCE_GROUP="ads-dental-rg"
ACR_NAME="adsdentalacr"
AKS_NAME="ads-dental-aks"
LOCATION="eastus"

echo "🚀 Starting Azure AKS Deployment..."

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    echo "❌ Azure CLI is not installed. Please install it first."
    exit 1
fi

# Check if kubectl is installed
if ! command -v kubectl &> /dev/null; then
    echo "❌ kubectl is not installed. Please install it first."
    exit 1
fi

# Login to Azure (if not already logged in)
echo "🔐 Checking Azure login status..."
if ! az account show &> /dev/null; then
    echo "Please login to Azure..."
    az login
fi

# Create resource group
echo "📦 Creating resource group..."
az group create --name $RESOURCE_GROUP --location $LOCATION

# Create Azure Container Registry
echo "🐳 Creating Azure Container Registry..."
az acr create --resource-group $RESOURCE_GROUP --name $ACR_NAME --sku Basic

# Login to ACR
echo "🔑 Logging into ACR..."
az acr login --name $ACR_NAME

# Build and push Docker images
echo "🏗️ Building and pushing Docker images..."

# Build Java CLI app
echo "Building Java CLI app..."
docker build -t $ACR_NAME.azurecr.io/java-cli-app:latest ../JavaCLIApp
docker push $ACR_NAME.azurecr.io/java-cli-app:latest

# Build .NET Web API
echo "Building .NET Web API..."
docker build -t $ACR_NAME.azurecr.io/dotnet-webapi:latest ../DotNetWebAPI
docker push $ACR_NAME.azurecr.io/dotnet-webapi:latest

# Create AKS cluster
echo "☸️ Creating AKS cluster..."
az aks create \
  --resource-group $RESOURCE_GROUP \
  --name $AKS_NAME \
  --node-count 2 \
  --enable-addons monitoring \
  --attach-acr $ACR_NAME \
  --generate-ssh-keys

# Get AKS credentials
echo "🔑 Getting AKS credentials..."
az aks get-credentials --resource-group $RESOURCE_GROUP --name $AKS_NAME

# Deploy applications
echo "🚀 Deploying applications to Kubernetes..."

# Update image references in YAML files
sed -i "s/adsdentalacr.azurecr.io/$ACR_NAME.azurecr.io/g" java-cli-deployment.yaml
sed -i "s/adsdentalacr.azurecr.io/$ACR_NAME.azurecr.io/g" dotnet-webapi-deployment.yaml

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
EXTERNAL_IP=$(kubectl get service dotnet-webapi-service -o jsonpath='{.status.loadBalancer.ingress[0].ip}')
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

echo "🎉 Azure AKS deployment completed!"
echo "📝 To clean up resources, run: ./cleanup.sh"
