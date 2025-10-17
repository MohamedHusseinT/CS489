# Lab 10 - Quick Start Guide

## 🚀 Get Started in 3 Steps

### Step 1: Install Docker Desktop
1. Download [Docker Desktop for Mac](https://www.docker.com/products/docker-desktop/)
2. Install and start Docker Desktop
3. Verify installation:
   ```bash
   docker --version
   docker-compose --version
   ```

### Step 2: Run the Test Script
```bash
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/10LabDocker
./test-containers.sh
```

### Step 3: Access the Applications
- **Java CLI**: Running in background (check logs with `docker logs java-test`)
- **.NET Web API**: http://localhost:8080
- **Swagger UI**: http://localhost:8080

---

## 🐳 Manual Commands

### Build and Run Java CLI
```bash
docker build -t ads-java-cli-app ./JavaCLIApp
docker run --name java-test ads-java-cli-app
```

### Build and Run .NET Web API
```bash
docker build -t ads-dotnet-webapi ./DotNetWebAPI
docker run -p 8080:80 --name dotnet-test ads-dotnet-webapi
```

### Use Docker Compose
```bash
docker-compose up --build
```

---

## 📸 Screenshots to Take

1. **Docker Desktop Dashboard**
2. **Terminal with `docker --version`**
3. **Container build process**
4. **Java CLI app running**
5. **.NET API in browser**
6. **Swagger UI interface**
7. **API endpoint testing**

---

## 🔧 Troubleshooting

### Docker Not Found
- Install Docker Desktop from official website
- Restart terminal after installation

### Port 8080 in Use
```bash
lsof -i :8080
kill -9 <PID>
```

### Build Failures
```bash
docker system prune -a
docker build --no-cache -t ads-java-cli-app ./JavaCLIApp
```

---

## 📚 Documentation

- **README.md** - Complete documentation
- **DOCKER_SETUP_GUIDE.md** - Docker installation guide
- **LAB10_IMPLEMENTATION_SUMMARY.md** - Implementation details

---

**Ready to containerize!** 🐳
