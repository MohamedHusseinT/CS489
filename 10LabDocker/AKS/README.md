# Lab 10 - Docker & Kubernetes Deployment

## Overview
This Lab 10 implementation includes Docker containerization and Kubernetes deployment of the ADS Dental Surgeries applications to both Azure AKS and AWS EKS.

## Applications
- **Java CLI Application**: Simple Hello World application that runs continuously
- **.NET Web API**: RESTful API for dental surgery management with Swagger UI

## Project Structure
```
10LabDocker/
├── JavaCLIApp/                    # Java CLI application
├── DotNetWebAPI/                  # .NET Web API application
├── docker-compose.yml             # Local Docker Compose setup
├── AKS/                          # Kubernetes deployment configurations
│   ├── Azure/                    # Azure AKS deployment
│   │   ├── README.md
│   │   ├── deploy.sh             # Automated deployment script
│   │   ├── cleanup.sh            # Cleanup script
│   │   ├── java-cli-deployment.yaml
│   │   ├── dotnet-webapi-deployment.yaml
│   │   └── dotnet-webapi-service.yaml
│   └── AWS/                      # AWS EKS deployment
│       ├── README.md
│       ├── deploy.sh             # Automated deployment script
│       ├── cleanup.sh            # Cleanup script
│       ├── java-cli-deployment.yaml
│       ├── dotnet-webapi-deployment.yaml
│       └── dotnet-webapi-service.yaml
└── screenshots/                  # Deployment screenshots
```

## Prerequisites

### For Local Docker Development
- Docker Desktop installed and running
- Docker Compose

### For Azure AKS Deployment
- Azure CLI installed and configured
- kubectl installed
- Azure subscription with appropriate permissions

### For AWS EKS Deployment
- AWS CLI installed and configured
- kubectl installed
- eksctl installed
- AWS account with appropriate permissions

## Quick Start

### 1. Local Docker Development
```bash
# Start both applications locally
docker compose up --build -d

# Check status
docker compose ps

# View logs
docker logs ads-java-cli-app
docker logs ads-dotnet-webapi

# Access Swagger UI
open http://localhost:8080/swagger
```

### 2. Azure AKS Deployment
```bash
cd AKS/Azure
./deploy.sh
```

### 3. AWS EKS Deployment
```bash
cd AKS/AWS
./deploy.sh
```

## Deployment URLs

After successful deployment, your applications will be available at:

### Azure AKS
- **Swagger UI**: `http://<EXTERNAL-IP>/swagger`
- **API Endpoints**: `http://<EXTERNAL-IP>/adsweb/api/v1/patients`

### AWS EKS
- **Swagger UI**: `http://<EXTERNAL-IP>/swagger`
- **API Endpoints**: `http://<EXTERNAL-IP>/adsweb/api/v1/patients`

## Features

### Java CLI Application
- ✅ Containerized with OpenJDK 17
- ✅ Runs continuously with logging
- ✅ Kubernetes deployment with resource limits
- ✅ Health checks and monitoring

### .NET Web API
- ✅ Containerized with .NET 8.0
- ✅ SQLite database with Entity Framework Core
- ✅ Swagger UI for API documentation
- ✅ RESTful endpoints for patients and addresses
- ✅ Kubernetes deployment with load balancing
- ✅ Health checks and readiness probes
- ✅ Horizontal scaling (2 replicas)

## API Endpoints

### Patients
- `GET /adsweb/api/v1/patients` - Get all patients
- `GET /adsweb/api/v1/patients/{id}` - Get patient by ID
- `POST /adsweb/api/v1/patients` - Create new patient
- `PUT /adsweb/api/v1/patients/patient/{id}` - Update patient
- `DELETE /adsweb/api/v1/patients/patient/{id}` - Delete patient
- `GET /adsweb/api/v1/patients/patient/search/{searchString}` - Search patients

### Addresses
- `GET /adsweb/api/v1/addresses` - Get all addresses
- `GET /adsweb/api/v1/addresses/{id}` - Get address by ID

## Cleanup

### Local Docker
```bash
docker compose down
```

### Azure AKS
```bash
cd AKS/Azure
./cleanup.sh
```

### AWS EKS
```bash
cd AKS/AWS
./cleanup.sh
```

## Screenshots Required

1. **Docker Desktop**: Running containers
2. **Container Status**: `docker compose ps`
3. **Java CLI Logs**: `docker logs ads-java-cli-app`
4. **.NET API Logs**: `docker logs ads-dotnet-webapi`
5. **API Test**: `curl http://localhost:8080/adsweb/api/v1/patients`
6. **Swagger UI**: Browser screenshot of API documentation
7. **Docker Images**: `docker images`
8. **Docker Networks**: `docker network ls`
9. **Kubernetes Deployment**: `kubectl get pods`
10. **Cloud Service URLs**: Browser screenshots of deployed applications

## Submission Requirements

- ✅ GitHub repository URL
- ✅ Azure AKS deployment URL
- ✅ AWS EKS deployment URL
- ✅ Screenshots of both local Docker and cloud deployments

## Troubleshooting

### Common Issues
1. **Docker not found**: Ensure Docker Desktop is running
2. **Permission denied**: Make scripts executable with `chmod +x`
3. **Azure login**: Run `az login` before deployment
4. **AWS credentials**: Configure AWS CLI with `aws configure`
5. **kubectl context**: Ensure correct cluster context is selected

### Getting Help
- Check container logs: `docker logs <container-name>`
- Check Kubernetes pods: `kubectl get pods`
- Check Kubernetes services: `kubectl get services`
- Check Kubernetes events: `kubectl get events`

## Cost Optimization

### Azure AKS
- Use Basic SKU for ACR
- Use Standard_D2s_v3 nodes
- Enable cluster autoscaler

### AWS EKS
- Use t3.medium instances
- Enable cluster autoscaler
- Use spot instances for non-production

---

**Lab 10 Status**: ✅ Complete with Docker containerization and Kubernetes deployment to both Azure AKS and AWS EKS
