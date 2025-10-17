# Docker Setup Guide for macOS

## 🐳 Installing Docker Desktop

### Step 1: Download Docker Desktop
1. Go to [Docker Desktop for Mac](https://www.docker.com/products/docker-desktop/)
2. Download the appropriate version for your Mac:
   - **Intel Chip**: Docker Desktop for Mac (Intel)
   - **Apple Silicon (M1/M2)**: Docker Desktop for Mac (Apple Silicon)

### Step 2: Install Docker Desktop
1. Open the downloaded `.dmg` file
2. Drag Docker to Applications folder
3. Launch Docker Desktop from Applications
4. Follow the setup wizard
5. Sign in or create a Docker account (optional)

### Step 3: Verify Installation
```bash
# Check Docker version
docker --version

# Check Docker Compose version
docker-compose --version

# Test Docker installation
docker run hello-world
```

### Step 4: Configure Docker Desktop
1. Open Docker Desktop
2. Go to Settings (gear icon)
3. Configure resources:
   - **Memory**: At least 4GB (8GB recommended)
   - **CPU**: At least 2 cores
   - **Disk**: At least 20GB free space

---

## 🚀 After Docker Installation

### Test the Lab 10 Containers

#### 1. Build Java CLI Container
```bash
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/10LabDocker

# Build Java container
docker build -t ads-java-cli-app ./JavaCLIApp

# Run Java container
docker run --name java-test ads-java-cli-app
```

#### 2. Build .NET Web API Container
```bash
# Build .NET container
docker build -t ads-dotnet-webapi ./DotNetWebAPI

# Run .NET container
docker run -p 8080:80 --name dotnet-test ads-dotnet-webapi
```

#### 3. Test with Docker Compose
```bash
# Build and start all containers
docker-compose up --build

# Access .NET API
open http://localhost:8080
```

---

## 📸 Screenshots to Take

After Docker is installed, take screenshots of:

1. **Docker Desktop Dashboard**
   - Running containers
   - Container logs
   - Resource usage

2. **Terminal Commands**
   - `docker --version`
   - `docker-compose --version`
   - `docker build` commands
   - `docker run` commands
   - `docker ps` output

3. **Container Execution**
   - Java CLI app output
   - .NET API in browser
   - Swagger UI interface
   - API endpoint testing

4. **Docker Compose**
   - `docker-compose up` output
   - `docker-compose ps` output
   - `docker-compose logs` output

---

## 🔧 Troubleshooting

### Docker Desktop Won't Start
1. Check system requirements
2. Restart Docker Desktop
3. Check for conflicting software
4. Reinstall Docker Desktop

### Permission Issues
```bash
# Add user to docker group (Linux)
sudo usermod -aG docker $USER

# Restart terminal session
```

### Port Conflicts
```bash
# Check what's using port 8080
lsof -i :8080

# Use different port
docker run -p 8081:80 ads-dotnet-webapi
```

### Build Failures
```bash
# Clean Docker cache
docker system prune -a

# Rebuild without cache
docker build --no-cache -t ads-java-cli-app ./JavaCLIApp
```

---

## 📋 Pre-Installation Checklist

- [ ] macOS 10.15 or later
- [ ] At least 4GB RAM available
- [ ] At least 20GB free disk space
- [ ] VirtualBox not installed (conflicts with Docker)
- [ ] Admin privileges for installation

---

## 🎯 Post-Installation Verification

Run these commands to verify everything works:

```bash
# 1. Check Docker installation
docker --version
docker-compose --version

# 2. Test basic Docker functionality
docker run hello-world

# 3. Build Lab 10 containers
docker build -t ads-java-cli-app ./JavaCLIApp
docker build -t ads-dotnet-webapi ./DotNetWebAPI

# 4. Test containers
docker run --name java-test ads-java-cli-app &
docker run -p 8080:80 --name dotnet-test ads-dotnet-webapi &

# 5. Test API
curl http://localhost:8080/adsweb/api/v1/patients

# 6. Clean up
docker stop java-test dotnet-test
docker rm java-test dotnet-test
```

---

## 📚 Additional Resources

- [Docker Documentation](https://docs.docker.com/)
- [Docker Desktop for Mac](https://docs.docker.com/desktop/mac/)
- [Docker Compose Documentation](https://docs.docker.com/compose/)
- [Best Practices for Docker](https://docs.docker.com/develop/dev-best-practices/)

---

**Note**: This guide assumes you're on macOS. For other operating systems, please refer to the official Docker documentation.
