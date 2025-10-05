#!/bin/bash

# Generate ERD Image from PlantUML
# Lab 05A - CS489 Applied Software Development
# Student: Mohamed

echo "🎨 Generating ERD Image for ADS Dental Surgery Database"
echo "======================================================="

# Check if PlantUML is installed
if ! command -v plantuml &> /dev/null; then
    echo "❌ PlantUML is not installed."
    echo "📦 Installing PlantUML..."
    
    # Install PlantUML using Homebrew
    if command -v brew &> /dev/null; then
        brew install plantuml
    else
        echo "❌ Homebrew not found. Please install PlantUML manually:"
        echo "   1. Download from: http://plantuml.com/download"
        echo "   2. Or install Java and run: java -jar plantuml.jar ADS_ERD.puml"
        exit 1
    fi
fi

# Generate the ERD image
echo "🖼️  Generating ERD image..."
plantuml ADS_ERD.puml

if [ -f "ADS_ERD.png" ]; then
    echo "✅ ERD image generated successfully: ADS_ERD.png"
    echo "📁 File location: $(pwd)/ADS_ERD.png"
    
    # Open the image (macOS)
    if command -v open &> /dev/null; then
        echo "🖥️  Opening ERD image..."
        open ADS_ERD.png
    fi
else
    echo "❌ Failed to generate ERD image."
    echo "💡 Alternative: Use online PlantUML editor at http://www.plantuml.com/plantuml/uml/"
    echo "   Copy the contents of ADS_ERD.puml and paste it there."
fi

echo ""
echo "📸 Screenshot Instructions:"
echo "   1. Take screenshot of the ERD image (ADS_ERD.png)"
echo "   2. Save as: ERD_ADS_Dental_Surgery.png"
echo "   3. Include in your lab submission"

