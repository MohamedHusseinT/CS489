# Lab 10 Docker Screenshots Guide

## Required Screenshots for Lab 10

### 1. Docker Desktop Application Running
- **Screenshot**: Docker Desktop application window showing containers running
- **Location**: Take screenshot of Docker Desktop GUI
- **Filename**: `01-docker-desktop-running.png`

### 2. Docker Compose Build Process
- **Screenshot**: Terminal showing `docker compose up --build -d` command execution
- **Location**: Terminal window showing the build process
- **Filename**: `02-docker-compose-build.png`

### 3. Container Status (docker compose ps)
- **Screenshot**: Terminal showing `docker compose ps` output
- **Location**: Terminal window showing running containers
- **Filename**: `03-container-status.png`

### 4. Java CLI Application Logs
- **Screenshot**: Terminal showing `docker logs ads-java-cli-app` output
- **Location**: Terminal window showing Java app logs
- **Filename**: `04-java-cli-logs.png`

### 5. .NET Web API Logs
- **Screenshot**: Terminal showing `docker logs ads-dotnet-webapi` output
- **Location**: Terminal window showing .NET API logs
- **Filename**: `05-dotnet-api-logs.png`

### 6. .NET Web API Test (curl command)
- **Screenshot**: Terminal showing API test with curl command
- **Location**: Terminal window showing API response
- **Filename**: `06-api-test-curl.png`

### 7. Swagger UI Access
- **Screenshot**: Browser showing Swagger UI at http://localhost:8080/swagger
- **Location**: Browser window showing API documentation
- **Filename**: `07-swagger-ui.png`

### 8. Docker Images List
- **Screenshot**: Terminal showing `docker images` command output
- **Location**: Terminal window showing built images
- **Filename**: `08-docker-images.png`

### 9. Container Network Information
- **Screenshot**: Terminal showing `docker network ls` output
- **Location**: Terminal window showing Docker networks
- **Filename**: `09-docker-networks.png`

### 10. Final Container Status
- **Screenshot**: Terminal showing final `docker compose ps` status
- **Location**: Terminal window showing all containers running
- **Filename**: `10-final-status.png`

## Instructions for Taking Screenshots

1. **Docker Desktop**: Open Docker Desktop application and take screenshot
2. **Terminal Screenshots**: Use Command+Shift+4 on Mac to capture terminal windows
3. **Browser Screenshots**: Use Command+Shift+4 or browser screenshot tools
4. **Save Location**: Save all screenshots in `/Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/10LabDocker/screenshots/`

## Commands to Run for Screenshots

```bash
# Set PATH for Docker commands
export PATH="/Applications/Docker.app/Contents/Resources/bin:$PATH"

# Navigate to project directory
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/10LabDocker

# Check container status
docker compose ps

# View Java CLI logs
docker logs ads-java-cli-app

# View .NET API logs
docker logs ads-dotnet-webapi

# Test API endpoint
curl http://localhost:8080/adsweb/api/v1/patients

# List Docker images
docker images

# List Docker networks
docker network ls

# Access Swagger UI in browser
open http://localhost:8080/swagger
```

## Current Status
✅ Both containers are running successfully
✅ Java CLI application is executing and logging
✅ .NET Web API is running and responding to requests
✅ Database is initialized with seed data
✅ API endpoints are accessible
