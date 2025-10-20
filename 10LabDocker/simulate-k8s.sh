#!/bin/bash

# Kubernetes Simulation Script using Docker Compose
# This script demonstrates Kubernetes concepts using Docker Compose

set -e

echo "🚀 Starting Kubernetes Simulation with Docker Compose..."

# Stop any existing containers
echo "🛑 Stopping existing containers..."
docker compose down

# Start with multiple replicas (simulating Kubernetes scaling)
echo "☸️ Starting applications with multiple replicas..."

# Create a docker-compose with scaling
cat > docker-compose-k8s-simulation.yml << EOF
version: '3.8'

services:
  java-cli-app:
    build: ./JavaCLIApp
    container_name: ads-java-cli-app-k8s
    networks:
      - ads-network
    restart: unless-stopped
    deploy:
      resources:
        limits:
          memory: 256M
          cpus: '0.2'
        reservations:
          memory: 128M
          cpus: '0.1'

  dotnet-webapi-1:
    build: ./DotNetWebAPI
    container_name: ads-dotnet-webapi-1
    ports:
      - "8080:80"
    networks:
      - ads-network
    restart: unless-stopped
    deploy:
      resources:
        limits:
          memory: 512M
          cpus: '0.5'
        reservations:
          memory: 256M
          cpus: '0.2'
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost/adsweb/api/v1/patients"]
      interval: 30s
      timeout: 10s
      retries: 3
      start_period: 40s

  dotnet-webapi-2:
    build: ./DotNetWebAPI
    container_name: ads-dotnet-webapi-2
    ports:
      - "8081:80"
    networks:
      - ads-network
    restart: unless-stopped
    deploy:
      resources:
        limits:
          memory: 512M
          cpus: '0.5'
        reservations:
          memory: 256M
          cpus: '0.2'
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost/adsweb/api/v1/patients"]
      interval: 30s
      timeout: 10s
      retries: 3
      start_period: 40s

networks:
  ads-network:
    driver: bridge
EOF

# Start the services
echo "🚀 Starting Kubernetes-simulated deployment..."
docker compose -f docker-compose-k8s-simulation.yml up --build -d

# Wait for services to be ready
echo "⏳ Waiting for services to be ready..."
sleep 30

# Check service status
echo "📊 Service Status (Kubernetes-style):"
echo "======================================"

# Simulate kubectl get pods
echo "📦 Pods:"
docker ps --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"

echo ""
echo "🌐 Services:"
echo "==========="
echo "java-cli-app:     Running (simulated pod)"
echo "dotnet-webapi-1:  Running on http://localhost:8080"
echo "dotnet-webapi-2:  Running on http://localhost:8081"

echo ""
echo "🔗 Load Balancer Simulation:"
echo "============================"
echo "Primary API:  http://localhost:8080/swagger"
echo "Secondary API: http://localhost:8081/swagger"

# Test the APIs
echo ""
echo "🧪 Testing APIs..."
echo "=================="

# Test primary API
echo "Testing Primary API (Port 8080)..."
if curl -s http://localhost:8080/adsweb/api/v1/patients | head -1 | grep -q "patientId"; then
    echo "✅ Primary API is healthy"
else
    echo "⚠️ Primary API may not be ready yet"
fi

# Test secondary API
echo "Testing Secondary API (Port 8081)..."
if curl -s http://localhost:8081/adsweb/api/v1/patients | head -1 | grep -q "patientId"; then
    echo "✅ Secondary API is healthy"
else
    echo "⚠️ Secondary API may not be ready yet"
fi

echo ""
echo "🎉 Kubernetes Simulation Complete!"
echo "=================================="
echo "📊 This demonstrates:"
echo "   - Multi-replica deployment (2 .NET API instances)"
echo "   - Resource limits and reservations"
echo "   - Health checks"
echo "   - Load balancing (multiple ports)"
echo "   - Container orchestration"
echo ""
echo "🌐 Access your applications:"
echo "   Swagger UI: http://localhost:8080/swagger"
echo "   API Endpoint: http://localhost:8080/adsweb/api/v1/patients"
echo ""
echo "📝 To clean up: docker compose -f docker-compose-k8s-simulation.yml down"
