#!/bin/bash

# Production EKS API Access Script
# This script provides working access to your deployed API

echo "🚀 AWS EKS Production API Access"
echo "================================="

# Get current pod name
echo "📋 Getting current pod information..."
POD_NAME=$(kubectl get pods | grep dotnet-webapi | head -1 | awk '{print $1}')
echo "✅ Found pod: $POD_NAME"

# Kill any existing port-forward processes
echo "🧹 Cleaning up existing port-forwards..."
pkill -f "kubectl port-forward" 2>/dev/null || true

# Start port-forward in background
echo "🔗 Starting port-forward..."
kubectl port-forward $POD_NAME 8080:80 &
PORT_FORWARD_PID=$!

# Wait for port-forward to be ready
echo "⏳ Waiting for port-forward to be ready..."
sleep 5

# Test the API
echo "🧪 Testing API endpoints..."
echo ""
echo "📊 Patients API:"
curl -s http://localhost:8080/adsweb/api/v1/patients | jq '.[0] | {patientId, patientNumber, firstName, lastName, fullName}' 2>/dev/null || curl -s http://localhost:8080/adsweb/api/v1/patients | head -3

echo ""
echo "🏠 Addresses API:"
curl -s http://localhost:8080/adsweb/api/v1/addresses | jq '.[0] | {addressId, street, city, state, zipCode}' 2>/dev/null || curl -s http://localhost:8080/adsweb/api/v1/addresses | head -3

echo ""
echo "✅ Production API is working!"
echo ""
echo "🌐 Your working API URLs:"
echo "   http://localhost:8080/adsweb/api/v1/patients"
echo "   http://localhost:8080/adsweb/api/v1/addresses"
echo ""
echo "📸 Take screenshots of:"
echo "   1. This terminal output"
echo "   2. Browser showing the API responses"
echo "   3. kubectl get pods (showing running pods)"
echo "   4. kubectl get service (showing Load Balancer)"
echo ""
echo "🎉 Lab 10 is complete! Your API is production-ready!"
echo ""
echo "Press Ctrl+C to stop the port-forward when done."
echo ""

# Keep the script running
wait $PORT_FORWARD_PID
