# CI/CD Pipeline Configuration

## GitHub Actions Workflow

### Main Pipeline (`.github/workflows/main.yml`)
```yaml
name: Main CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

env:
  DOTNET_VERSION: '8.0.x'
  NODE_VERSION: '18.x'

jobs:
  # Backend Tests
  backend-tests:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore
    
    - name: Run unit tests
      run: dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage"
    
    - name: Run integration tests
      run: dotnet test --no-build --verbosity normal --filter "Category=Integration"
    
    - name: Upload coverage reports
      uses: codecov/codecov-action@v3

  # Frontend Tests
  frontend-tests:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    
    - name: Setup Node.js
      uses: actions/setup-node@v3
      with:
        node-version: ${{ env.NODE_VERSION }}
        cache: 'npm'
    
    - name: Install dependencies
      run: npm ci
    
    - name: Run linting
      run: npm run lint
    
    - name: Run unit tests
      run: npm run test:unit
    
    - name: Run e2e tests
      run: npm run test:e2e
    
    - name: Build
      run: npm run build

  # Security Scanning
  security-scan:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    
    - name: Run security scan
      uses: securecodewarrior/github-action-add-sarif@v1
      with:
        sarif-file: security-scan-results.sarif

  # Build and Deploy
  build-and-deploy:
    needs: [backend-tests, frontend-tests, security-scan]
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'
    
    steps:
    - uses: actions/checkout@v4
    
    - name: Build Docker images
      run: |
        docker build -t projecta-api:latest ./02-Implementation/Backend
        docker build -t projecta-frontend:latest ./02-Implementation/Frontend
    
    - name: Deploy to staging
      run: |
        echo "Deploying to staging environment"
        # Add deployment commands here
    
    - name: Deploy to production
      if: github.ref == 'refs/heads/main'
      run: |
        echo "Deploying to production environment"
        # Add production deployment commands here
```

## Docker Configuration

### Backend Dockerfile
```dockerfile
# Backend Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProjectA.API/ProjectA.API.csproj", "ProjectA.API/"]
COPY ["ProjectA.Core/ProjectA.Core.csproj", "ProjectA.Core/"]
COPY ["ProjectA.Infrastructure/ProjectA.Infrastructure.csproj", "ProjectA.Infrastructure/"]
RUN dotnet restore "ProjectA.API/ProjectA.API.csproj"
COPY . .
WORKDIR "/src/ProjectA.API"
RUN dotnet build "ProjectA.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectA.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectA.API.dll"]
```

### Frontend Dockerfile
```dockerfile
# Frontend Dockerfile
FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci --only=production
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

### Docker Compose
```yaml
# docker-compose.yml
version: '3.8'

services:
  api:
    build: ./02-Implementation/Backend
    ports:
      - "5000:80"
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=ProjectA;User Id=sa;Password=YourPassword123!;TrustServerCertificate=true;
    depends_on:
      - db
      - redis

  frontend:
    build: ./02-Implementation/Frontend
    ports:
      - "3000:80"
    depends_on:
      - api

  db:
    image: postgres:15
    environment:
      - POSTGRES_DB=ProjectA
      - POSTGRES_USER=sa
      - POSTGRES_PASSWORD=YourPassword123!
    volumes:
      - postgres_data:/var/lib/postgresql/data
    ports:
      - "5432:5432"

  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"

volumes:
  postgres_data:
```

## Deployment Configuration

### Azure Deployment
```yaml
# azure-pipelines.yml
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'

stages:
- stage: Build
  displayName: Build stage
  jobs:
  - job: Build
    displayName: Build
    steps:
    - task: DotNetCoreCLI@2
      displayName: Restore
      inputs:
        command: 'restore'
        projects: '**/*.csproj'
    
    - task: DotNetCoreCLI@2
      displayName: Build
      inputs:
        command: 'build'
        projects: '**/*.csproj'
        arguments: '--configuration $(buildConfiguration)'
    
    - task: DotNetCoreCLI@2
      displayName: Test
      inputs:
        command: 'test'
        projects: '**/*Tests.csproj'
        arguments: '--configuration $(buildConfiguration) --collect Code coverage'

- stage: Deploy
  displayName: Deploy stage
  dependsOn: Build
  condition: and(succeeded(), eq(variables['Build.SourceBranch'], 'refs/heads/main'))
  jobs:
  - deployment: Deploy
    displayName: Deploy
    environment: 'production'
    strategy:
      runOnce:
        deploy:
          steps:
          - task: AzureWebApp@1
            displayName: 'Deploy Azure Web App'
            inputs:
              azureSubscription: 'Azure-Service-Connection'
              appName: 'projecta-api'
              package: '$(Pipeline.Workspace)/**/*.zip'
```

### AWS Deployment
```yaml
# aws-deploy.yml
name: AWS Deployment

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    
    - name: Configure AWS credentials
      uses: aws-actions/configure-aws-credentials@v2
      with:
        aws-access-key-id: ${{ secrets.AWS_ACCESS_KEY_ID }}
        aws-secret-access-key: ${{ secrets.AWS_SECRET_ACCESS_KEY }}
        aws-region: us-east-1
    
    - name: Deploy to ECS
      run: |
        aws ecs update-service \
          --cluster projecta-cluster \
          --service projecta-api \
          --force-new-deployment
    
    - name: Deploy to S3
      run: |
        aws s3 sync ./02-Implementation/Frontend/dist s3://projecta-frontend-bucket
```

## Quality Gates

### Code Quality Checks
- **Code Coverage**: Minimum 80% for backend, 70% for frontend
- **Security Scan**: No high or critical vulnerabilities
- **Performance**: API response time < 2 seconds
- **Linting**: No linting errors
- **Build**: Successful build on all platforms

### Deployment Gates
- **Staging Tests**: All tests pass in staging environment
- **Smoke Tests**: Critical user flows work in production
- **Rollback Plan**: Automated rollback capability
- **Monitoring**: Health checks and monitoring in place

---
*CI/CD configuration will be implemented and refined as the project progresses through development phases.*
