# Lab 10 - Docker Containerization

## CS489 Applied Software Development
**Student**: Mohamed Hussein  
**Date**: October 2025  
**Assignment**: Docker Containerization for Java CLI and .NET Web API

---

## 📋 Overview

This lab demonstrates Docker containerization for two applications:
1. **Java CLI Application** - A simple "Hello World Docker" application
2. **.NET Web API Application** - The ADS Dental Surgeries Web API from Lab 7

---

## 🐳 Docker Setup

### Prerequisites
- Docker Desktop installed and running
- Java 17+ (for building Java app locally)
- .NET 8.0 SDK (for building .NET app locally)

### Verify Docker Installation
```bash
docker --version
docker-compose --version
```

---

## 📁 Project Structure

```
10LabDocker/
├── JavaCLIApp/                    # Java CLI Application
│   ├── Dockerfile                 # Java app containerization
│   ├── pom.xml                    # Maven configuration
│   ├── src/                       # Java source code
│   └── target/                    # Compiled Java artifacts
├── DotNetWebAPI/                  # .NET Web API Application
│   ├── Dockerfile                 # .NET app containerization
│   ├── .dockerignore              # Docker ignore rules
│   ├── *.csproj                   # .NET project file
│   ├── Controllers/               # API controllers
│   ├── Models/                    # Data models
│   ├── Services/                  # Business logic
│   └── Data/                      # Database context
├── docker-compose.yml             # Multi-container orchestration
├── screenshots/                   # Docker execution screenshots
└── README.md                      # This documentation
```

---

## 🚀 Quick Start

### Option 1: Using Docker Compose (Recommended)
```bash
# Build and start all containers
docker-compose up --build

# Run in background
docker-compose up -d --build

# Stop all containers
docker-compose down
```

### Option 2: Individual Container Commands

#### Java CLI Application
```bash
# Build the Java container
docker build -t ads-java-cli-app ./JavaCLIApp

# Run the Java container
docker run --name java-cli-test ads-java-cli-app

# Run with custom name
docker run --name my-java-app ads-java-cli-app
```

#### .NET Web API Application
```bash
# Build the .NET container
docker build -t ads-dotnet-webapi ./DotNetWebAPI

# Run the .NET container
docker run -p 8080:80 --name dotnet-api-test ads-dotnet-webapi

# Run with custom port
docker run -p 5000:80 --name my-dotnet-api ads-dotnet-webapi
```

---

## 🔧 Container Details

### Java CLI Application Container

**Dockerfile Features:**
- Base image: `openjdk:17`
- Multi-stage build for optimization
- JAR file execution
- Continuous loop with 2-second intervals

**Application Behavior:**
- Prints "Hello World Docker!"
- Runs infinite loop with iteration counter
- Demonstrates container lifecycle

**Build Command:**
```bash
docker build -t ads-java-cli-app ./JavaCLIApp
```

**Run Command:**
```bash
docker run --name java-cli-test ads-java-cli-app
```

### .NET Web API Application Container

**Dockerfile Features:**
- Multi-stage build (SDK for build, Runtime for execution)
- Base images: `mcr.microsoft.com/dotnet/sdk:8.0` and `mcr.microsoft.com/dotnet/aspnet:8.0`
- Port 80 exposure
- Optimized for production deployment

**Application Features:**
- RESTful Web API for ADS Dental Surgeries
- SQLite database with Entity Framework Core
- Swagger/OpenAPI documentation
- Patient and Address management endpoints

**Build Command:**
```bash
docker build -t ads-dotnet-webapi ./DotNetWebAPI
```

**Run Command:**
```bash
docker run -p 8080:80 --name dotnet-api-test ads-dotnet-webapi
```

**Access Points:**
- API: `http://localhost:8080`
- Swagger UI: `http://localhost:8080`

---

## 🧪 Testing the Containers

### Test Java CLI Application
```bash
# Start the container
docker run --name java-test ads-java-cli-app

# In another terminal, check logs
docker logs java-test

# Stop the container
docker stop java-test
docker rm java-test
```

### Test .NET Web API Application
```bash
# Start the container
docker run -p 8080:80 --name dotnet-test ads-dotnet-webapi

# Test the API
curl http://localhost:8080/adsweb/api/v1/patients

# Check Swagger UI
open http://localhost:8080

# Stop the container
docker stop dotnet-test
docker rm dotnet-test
```

### Test with Docker Compose
```bash
# Start all services
docker-compose up -d

# Check running containers
docker-compose ps

# View logs
docker-compose logs java-cli-app
docker-compose logs dotnet-webapi

# Stop all services
docker-compose down
```

---

## 📊 Docker Commands Reference

### Container Management
```bash
# List running containers
docker ps

# List all containers (including stopped)
docker ps -a

# Stop a container
docker stop <container_name>

# Remove a container
docker rm <container_name>

# Remove a container (force)
docker rm -f <container_name>
```

### Image Management
```bash
# List images
docker images

# Remove an image
docker rmi <image_name>

# Remove unused images
docker image prune

# Remove all unused images
docker image prune -a
```

### Logs and Debugging
```bash
# View container logs
docker logs <container_name>

# Follow logs in real-time
docker logs -f <container_name>

# Execute command in running container
docker exec -it <container_name> /bin/bash

# Inspect container details
docker inspect <container_name>
```

### Docker Compose Commands
```bash
# Build and start services
docker-compose up --build

# Start services in background
docker-compose up -d

# Stop services
docker-compose down

# View service logs
docker-compose logs <service_name>

# Scale services
docker-compose up --scale java-cli-app=3
```

---

## 🔍 Container Inspection

### Java CLI Application
```bash
# Build and inspect
docker build -t ads-java-cli-app ./JavaCLIApp
docker inspect ads-java-cli-app

# Check container size
docker images | grep ads-java-cli-app
```

### .NET Web API Application
```bash
# Build and inspect
docker build -t ads-dotnet-webapi ./DotNetWebAPI
docker inspect ads-dotnet-webapi

# Check container size
docker images | grep ads-dotnet-webapi
```

---

## 📸 Screenshots

The `screenshots/` folder contains:
- Docker Desktop running
- Container build process
- Container execution
- API testing in browser
- Terminal commands and outputs

---

## 🛠️ Troubleshooting

### Common Issues

#### Port Already in Use
```bash
# Find process using port
lsof -i :8080

# Kill process
kill -9 <PID>

# Or use different port
docker run -p 8081:80 ads-dotnet-webapi
```

#### Container Won't Start
```bash
# Check container logs
docker logs <container_name>

# Check Docker daemon
docker info

# Restart Docker Desktop
```

#### Build Failures
```bash
# Clean Docker cache
docker system prune -a

# Rebuild without cache
docker build --no-cache -t ads-java-cli-app ./JavaCLIApp
```

### Debugging Commands
```bash
# Enter running container
docker exec -it <container_name> /bin/bash

# Check container processes
docker exec -it <container_name> ps aux

# Check network connectivity
docker exec -it <container_name> ping google.com
```

---

## 🎯 Lab Requirements Met

✅ **Docker Desktop Setup** - Verified installation and functionality  
✅ **Java CLI Containerization** - Complete Docker implementation  
✅ **.NET Web API Containerization** - Complete Docker implementation  
✅ **Multi-container Orchestration** - Docker Compose setup  
✅ **Screenshots** - Terminal and IDE execution captured  
✅ **Documentation** - Comprehensive README and instructions  

---

## 📚 Learning Outcomes

1. **Docker Fundamentals** - Container creation and management
2. **Multi-stage Builds** - Optimized container images
3. **Docker Compose** - Multi-container orchestration
4. **Port Mapping** - Container-to-host communication
5. **Image Optimization** - Efficient container design
6. **Container Lifecycle** - Build, run, stop, remove operations

---

## 🔄 Comparison: Before vs After Docker

### Before Docker
- Applications tied to host environment
- Dependency conflicts
- Difficult deployment
- Inconsistent environments

### After Docker
- Portable applications
- Isolated environments
- Easy deployment
- Consistent behavior across platforms

---

## 🚀 Production Considerations

### Security
- Use non-root users in containers
- Scan images for vulnerabilities
- Use specific base image versions
- Implement proper secrets management

### Performance
- Multi-stage builds for smaller images
- Use .dockerignore to exclude unnecessary files
- Optimize layer caching
- Monitor resource usage

### Monitoring
- Implement health checks
- Use container orchestration (Kubernetes)
- Set up logging aggregation
- Monitor container metrics

---

## 📝 Next Steps

1. **Push to GitHub** - Commit all Docker files and documentation
2. **Docker Hub** - Push images to Docker Hub for sharing
3. **Kubernetes** - Deploy containers to Kubernetes cluster
4. **CI/CD** - Integrate Docker builds into CI/CD pipeline

---

**Lab 10 Complete!** 🎉

All Docker containerization requirements have been successfully implemented and tested.
