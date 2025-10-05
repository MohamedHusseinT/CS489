#!/bin/bash

# ADS Dental Surgery Database Setup for macOS
# Lab 05A - CS489 Applied Software Development
# Student: Mohamed

echo "🏥 Advantis Dental Surgeries (ADS) Database Setup for macOS"
echo "============================================================"

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    echo "❌ Docker is not installed. Please install Docker Desktop first:"
    echo "   https://www.docker.com/products/docker-desktop/"
    echo "   Or run: brew install --cask docker"
    exit 1
fi

# Check if Docker is running
if ! docker info &> /dev/null; then
    echo "❌ Docker is not running. Please start Docker Desktop first."
    exit 1
fi

echo "✅ Docker is installed and running"

# Stop and remove existing container if it exists
echo "🧹 Cleaning up existing containers..."
docker stop sqlserver 2>/dev/null || true
docker rm sqlserver 2>/dev/null || true

# Pull and run SQL Server
echo "🐳 Starting SQL Server in Docker..."
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" \
   -p 1433:1433 --name sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest

# Wait for SQL Server to start
echo "⏳ Waiting for SQL Server to start..."
sleep 30

# Check if container is running
if docker ps | grep -q sqlserver; then
    echo "✅ SQL Server is running successfully!"
    echo ""
    echo "📋 Connection Details:"
    echo "   Server: localhost,1433"
    echo "   Authentication: SQL Server Authentication"
    echo "   Login: sa"
    echo "   Password: YourStrong@Passw0rd"
    echo ""
    echo "🔧 Next Steps:"
    echo "   1. Install Azure Data Studio: brew install --cask azure-data-studio"
    echo "   2. Connect to SQL Server using the details above"
    echo "   3. Execute the myADSDentalSurgeryDBScript.sql file"
    echo ""
    echo "📁 SQL Script Location:"
    echo "   $(pwd)/myADSDentalSurgeryDBScript.sql"
else
    echo "❌ Failed to start SQL Server. Check Docker logs:"
    docker logs sqlserver
fi

