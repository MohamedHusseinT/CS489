# Azure AKS Deployment Configuration

## Overview
This directory contains Kubernetes manifests and deployment scripts for deploying the ADS Dental Surgeries applications to Azure Kubernetes Service (AKS).

## Applications
- **Java CLI Application**: Simple Hello World application
- **.NET Web API**: RESTful API for dental surgery management

## Prerequisites
- Azure CLI installed and configured
- kubectl installed
- Docker images pushed to Azure Container Registry (ACR)

## Deployment Steps

### 1. Create Azure Container Registry
```bash
# Create resource group
az group create --name ads-dental-rg --location eastus

# Create ACR
az acr create --resource-group ads-dental-rg --name adsdentalacr --sku Basic

# Login to ACR
az acr login --name adsdentalacr
```

### 2. Build and Push Docker Images
```bash
# Build and tag images
docker build -t adsdentalacr.azurecr.io/java-cli-app:latest ./JavaCLIApp
docker build -t adsdentalacr.azurecr.io/dotnet-webapi:latest ./DotNetWebAPI

# Push images
docker push adsdentalacr.azurecr.io/java-cli-app:latest
docker push adsdentalacr.azurecr.io/dotnet-webapi:latest
```

### 3. Create AKS Cluster
```bash
# Create AKS cluster
az aks create \
  --resource-group ads-dental-rg \
  --name ads-dental-aks \
  --node-count 2 \
  --enable-addons monitoring \
  --attach-acr adsdentalacr

# Get credentials
az aks get-credentials --resource-group ads-dental-rg --name ads-dental-aks
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
# http://<EXTERNAL-IP>:8080/swagger
```

## Files
- `java-cli-deployment.yaml`: Kubernetes deployment for Java CLI app
- `dotnet-webapi-deployment.yaml`: Kubernetes deployment for .NET Web API
- `dotnet-webapi-service.yaml`: Kubernetes service for .NET Web API
- `deploy.sh`: Automated deployment script
- `cleanup.sh`: Cleanup script to remove resources
