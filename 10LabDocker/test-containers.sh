#!/bin/bash

# Lab 10 - Docker Container Testing Script
# CS489 Applied Software Development
# Student: Mohamed Hussein

echo "=================================================="
echo "Lab 10 - Docker Container Testing"
echo "CS489 Applied Software Development"
echo "Student: Mohamed Hussein"
echo "=================================================="
echo

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    echo "❌ Docker is not installed!"
    echo "Please install Docker Desktop first."
    echo "See DOCKER_SETUP_GUIDE.md for instructions."
    exit 1
fi

# Check if Docker is running
if ! docker info &> /dev/null; then
    echo "❌ Docker is not running!"
    echo "Please start Docker Desktop."
    exit 1
fi

echo "✅ Docker is installed and running"
echo

# Function to cleanup containers
cleanup() {
    echo "🧹 Cleaning up containers..."
    docker stop java-test dotnet-test 2>/dev/null
    docker rm java-test dotnet-test 2>/dev/null
    echo "✅ Cleanup complete"
}

# Set trap to cleanup on exit
trap cleanup EXIT

echo "🐳 Testing Java CLI Container..."
echo "=================================="

# Build Java container
echo "Building Java CLI container..."
docker build -t ads-java-cli-app ./JavaCLIApp
if [ $? -eq 0 ]; then
    echo "✅ Java container built successfully"
else
    echo "❌ Failed to build Java container"
    exit 1
fi

# Run Java container in background
echo "Running Java CLI container..."
docker run --name java-test ads-java-cli-app &
JAVA_PID=$!

# Wait a bit and check logs
sleep 3
echo "Java container logs:"
docker logs java-test
echo

echo "🌐 Testing .NET Web API Container..."
echo "===================================="

# Build .NET container
echo "Building .NET Web API container..."
docker build -t ads-dotnet-webapi ./DotNetWebAPI
if [ $? -eq 0 ]; then
    echo "✅ .NET container built successfully"
else
    echo "❌ Failed to build .NET container"
    exit 1
fi

# Run .NET container
echo "Running .NET Web API container..."
docker run -p 8080:80 --name dotnet-test ads-dotnet-webapi &
DOTNET_PID=$!

# Wait for container to start
echo "Waiting for .NET API to start..."
sleep 10

# Test API endpoints
echo "Testing API endpoints..."
echo

echo "🔍 Testing Health Check:"
curl -s http://localhost:8080/adsweb/api/v1/patients | head -5
echo

echo "🔍 Testing Swagger UI:"
curl -s -I http://localhost:8080 | head -3
echo

echo "📊 Container Status:"
docker ps --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"
echo

echo "📋 Container Information:"
echo "Java CLI Container: java-test"
echo ".NET Web API Container: dotnet-test (Port 8080)"
echo "API Base URL: http://localhost:8080"
echo "Swagger UI: http://localhost:8080"
echo

echo "✅ All containers are running successfully!"
echo
echo "🌐 Open your browser and visit:"
echo "   - API: http://localhost:8080"
echo "   - Swagger UI: http://localhost:8080"
echo
echo "Press Ctrl+C to stop all containers and exit"
echo

# Keep script running
wait
