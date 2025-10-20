#!/bin/bash

# Local Kubernetes Deployment Script (Docker Desktop)
# This script deploys the ADS Dental Surgeries applications to local Kubernetes

set -e

echo "🚀 Starting Local Kubernetes Deployment..."

# Check if Docker Desktop Kubernetes is enabled
if ! kubectl cluster-info &> /dev/null; then
    echo "❌ Kubernetes is not enabled in Docker Desktop."
    echo "Please enable Kubernetes in Docker Desktop:"
    echo "1. Open Docker Desktop"
    echo "2. Go to Settings > Kubernetes"
    echo "3. Check 'Enable Kubernetes'"
    echo "4. Click 'Apply & Restart'"
    exit 1
fi

echo "✅ Kubernetes cluster is running"

# Check if kubectl is installed
if ! command -v kubectl &> /dev/null; then
    echo "❌ kubectl is not installed. Please install it first."
    exit 1
fi

# Build Docker images locally
echo "🏗️ Building Docker images locally..."

# Build Java CLI app
echo "Building Java CLI app..."
docker build -t java-cli-app:latest ./JavaCLIApp

# Build .NET Web API
echo "Building .NET Web API..."
docker build -t dotnet-webapi:latest ./DotNetWebAPI

# Create Kubernetes manifests for local deployment
echo "📝 Creating Kubernetes manifests..."

# Java CLI Deployment
cat > java-cli-local.yaml << EOF
apiVersion: apps/v1
kind: Deployment
metadata:
  name: java-cli-app
  labels:
    app: java-cli-app
spec:
  replicas: 1
  selector:
    matchLabels:
      app: java-cli-app
  template:
    metadata:
      labels:
        app: java-cli-app
    spec:
      containers:
      - name: java-cli-app
        image: java-cli-app:latest
        imagePullPolicy: Never
        resources:
          requests:
            memory: "128Mi"
            cpu: "100m"
          limits:
            memory: "256Mi"
            cpu: "200m"
---
apiVersion: v1
kind: Service
metadata:
  name: java-cli-service
  labels:
    app: java-cli-app
spec:
  selector:
    app: java-cli-app
  ports:
  - port: 8080
    targetPort: 8080
  type: ClusterIP
EOF

# .NET Web API Deployment
cat > dotnet-webapi-local.yaml << EOF
apiVersion: apps/v1
kind: Deployment
metadata:
  name: dotnet-webapi
  labels:
    app: dotnet-webapi
spec:
  replicas: 2
  selector:
    matchLabels:
      app: dotnet-webapi
  template:
    metadata:
      labels:
        app: dotnet-webapi
    spec:
      containers:
      - name: dotnet-webapi
        image: dotnet-webapi:latest
        imagePullPolicy: Never
        ports:
        - containerPort: 80
        resources:
          requests:
            memory: "256Mi"
            cpu: "200m"
          limits:
            memory: "512Mi"
            cpu: "500m"
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: "Production"
        - name: ASPNETCORE_URLS
          value: "http://+:80"
        livenessProbe:
          httpGet:
            path: /adsweb/api/v1/patients
            port: 80
          initialDelaySeconds: 30
          periodSeconds: 10
        readinessProbe:
          httpGet:
            path: /adsweb/api/v1/patients
            port: 80
          initialDelaySeconds: 5
          periodSeconds: 5
---
apiVersion: v1
kind: Service
metadata:
  name: dotnet-webapi-service
  labels:
    app: dotnet-webapi
spec:
  selector:
    app: dotnet-webapi
  ports:
  - port: 80
    targetPort: 80
    protocol: TCP
  type: LoadBalancer
EOF

# Deploy applications
echo "🚀 Deploying applications to local Kubernetes..."

# Deploy Java CLI app
kubectl apply -f java-cli-local.yaml

# Deploy .NET Web API
kubectl apply -f dotnet-webapi-local.yaml

# Wait for deployments to be ready
echo "⏳ Waiting for deployments to be ready..."
kubectl wait --for=condition=available --timeout=300s deployment/java-cli-app
kubectl wait --for=condition=available --timeout=300s deployment/dotnet-webapi

# Get service information
echo "🌐 Getting service information..."
kubectl get services
kubectl get pods

# Get external IP (for LoadBalancer service)
echo "🔍 Getting external IP..."
EXTERNAL_IP=$(kubectl get service dotnet-webapi-service -o jsonpath='{.status.loadBalancer.ingress[0].ip}')
if [ -n "$EXTERNAL_IP" ]; then
    echo "✅ Deployment completed successfully!"
    echo "🌐 .NET Web API is available at: http://$EXTERNAL_IP/swagger"
    echo "📊 Swagger UI: http://$EXTERNAL_IP/swagger"
    echo "🔗 API Endpoints:"
    echo "   - GET http://$EXTERNAL_IP/adsweb/api/v1/patients"
    echo "   - GET http://$EXTERNAL_IP/adsweb/api/v1/addresses"
else
    echo "⚠️ External IP not yet assigned. Using port-forward instead..."
    echo "🔗 To access the API, run:"
    echo "   kubectl port-forward service/dotnet-webapi-service 8080:80"
    echo "   Then visit: http://localhost:8080/swagger"
fi

echo "🎉 Local Kubernetes deployment completed!"
echo "📝 To clean up, run: kubectl delete -f java-cli-local.yaml -f dotnet-webapi-local.yaml"
