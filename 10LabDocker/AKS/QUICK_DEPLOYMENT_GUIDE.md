# Quick Deployment Guide - Lab 10 AKS

## 🚀 Ready to Deploy to Cloud!

Your Lab 10 implementation is now complete with Kubernetes deployment configurations for both Azure AKS and AWS EKS.

## 📁 What's Been Created

### Azure AKS (`AKS/Azure/`)
- ✅ Kubernetes manifests for both applications
- ✅ Automated deployment script (`deploy.sh`)
- ✅ Cleanup script (`cleanup.sh`)
- ✅ Complete documentation

### AWS EKS (`AKS/AWS/`)
- ✅ Kubernetes manifests for both applications
- ✅ Automated deployment script (`deploy.sh`)
- ✅ Cleanup script (`cleanup.sh`)
- ✅ Complete documentation

## 🎯 Next Steps

### Option 1: Deploy to Azure AKS
```bash
cd AKS/Azure
./deploy.sh
```

### Option 2: Deploy to AWS EKS
```bash
cd AKS/AWS
./deploy.sh
```

### Option 3: Deploy to Both (Recommended)
```bash
# Deploy to Azure first
cd AKS/Azure
./deploy.sh

# Then deploy to AWS
cd ../AWS
./deploy.sh
```

## 📋 Prerequisites Check

### For Azure AKS:
- [ ] Azure CLI installed (`az --version`)
- [ ] Azure account logged in (`az login`)
- [ ] kubectl installed (`kubectl version`)

### For AWS EKS:
- [ ] AWS CLI installed (`aws --version`)
- [ ] AWS credentials configured (`aws configure`)
- [ ] kubectl installed (`kubectl version`)
- [ ] eksctl installed (`eksctl version`)

## 🌐 Expected Results

After deployment, you'll get:
- **Azure AKS URL**: `http://<EXTERNAL-IP>/swagger`
- **AWS EKS URL**: `http://<EXTERNAL-IP>/swagger`

## 📸 Screenshots to Take

1. **Deployment Process**: Terminal showing deployment commands
2. **Kubernetes Pods**: `kubectl get pods`
3. **Kubernetes Services**: `kubectl get services`
4. **Azure AKS Swagger UI**: Browser screenshot
5. **AWS EKS Swagger UI**: Browser screenshot
6. **API Response**: Browser showing JSON data

## 🧹 Cleanup

When done testing:
```bash
# Azure cleanup
cd AKS/Azure && ./cleanup.sh

# AWS cleanup
cd AKS/AWS && ./cleanup.sh
```

## 🎉 Lab 10 Status

✅ **Docker Containerization**: Complete  
✅ **Local Docker Compose**: Working  
✅ **Azure AKS Configuration**: Ready  
✅ **AWS EKS Configuration**: Ready  
✅ **Kubernetes Manifests**: Created  
✅ **Deployment Scripts**: Automated  
✅ **Documentation**: Complete  

**Ready for cloud deployment!** 🚀
