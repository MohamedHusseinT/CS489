#!/bin/bash

# Mohamed's eLibrary Test Runner
# This script helps test the ASP.NET Core application

echo "🚀 Starting Mohamed's eLibrary Application Test"
echo "==============================================="

# Kill any existing dotnet processes
echo "🧹 Cleaning up existing processes..."
pkill -f "dotnet run" || true
sleep 2

# Clean and build
echo "🔨 Building application..."
dotnet clean
dotnet build

if [ $? -ne 0 ]; then
    echo "❌ Build failed!"
    exit 1
fi

echo "✅ Build successful!"

# Start the application
echo "🚀 Starting application on http://localhost:5000..."
dotnet run --urls "http://localhost:5000" &

# Wait for application to start
echo "⏳ Waiting for application to start..."
sleep 8

# Test endpoints
echo "🧪 Testing API endpoints..."

# Test homepage
echo "📄 Testing homepage..."
curl -s -o /dev/null -w "Homepage: %{http_code}\n" http://localhost:5000/

# Test swagger
echo "📚 Testing Swagger docs..."
curl -s -o /dev/null -w "Swagger: %{http_code}\n" http://localhost:5000/swagger

# Test API endpoint
echo "📊 Testing Author API..."
curl -s -o /dev/null -w "Author API: %{http_code}\n" http://localhost:5000/api/author/1

echo ""
echo "🌐 Application URLs:"
echo "   Frontend: http://localhost:5000/"
echo "   Swagger:  http://localhost:5000/swagger"
echo "   Author API: http://localhost:5000/api/author/1"
echo ""
echo "📸 Ready for screenshots!"
echo "   Press Ctrl+C to stop the application"
echo ""

# Keep the application running
wait
