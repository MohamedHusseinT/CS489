# SQL Server Setup for macOS - Complete Guide

## Lab 05A - CS489 Applied Software Development
**Student:** Mohamed  
**Date:** October 2025

---

## 🍎 macOS SQL Server Alternatives

Since SQL Server doesn't run natively on macOS, here are the best alternatives:

### **Option 1: Docker (Recommended) ⭐**

#### Prerequisites:
- **Docker Desktop** for macOS
- **Azure Data Studio** (free SQL client)

#### Step 1: Install Docker Desktop
```bash
# Using Homebrew (recommended)
brew install --cask docker

# Or download from: https://www.docker.com/products/docker-desktop/
```

#### Step 2: Install Azure Data Studio
```bash
# Using Homebrew
brew install --cask azure-data-studio

# Or download from: https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio
```

#### Step 3: Run the Setup Script
```bash
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/05ALab
./setup-sqlserver-macos.sh
```

#### Step 4: Connect to SQL Server
1. **Open Azure Data Studio**
2. **Create New Connection:**
   - **Server:** `localhost,1433`
   - **Authentication Type:** SQL Login
   - **User name:** `sa`
   - **Password:** `YourStrong@Passw0rd`
   - **Database name:** (leave empty)
3. **Click Connect**

#### Step 5: Execute the Database Script
1. **Open the SQL Script:**
   - File → Open File
   - Navigate to: `05ALab/myADSDentalSurgeryDBScript.sql`
2. **Execute the Script:**
   - Press `F5` or click "Run"
   - Wait for completion

---

### **Option 2: Azure SQL Database (Cloud)**

#### Step 1: Create Azure Account
- Go to: https://azure.microsoft.com/en-us/free/
- Create free account (12 months free tier)

#### Step 2: Create SQL Database
1. **Azure Portal** → Create Resource → SQL Database
2. **Configuration:**
   - **Database name:** `ADSDentalSurgery`
   - **Server:** Create new (e.g., `ads-sql-server`)
   - **Pricing tier:** Basic ($5/month) or Free tier
3. **Create Database**

#### Step 3: Connect and Execute
1. **Get Connection String** from Azure Portal
2. **Use Azure Data Studio** to connect
3. **Execute the SQL script**

---

### **Option 3: SQLite (Simplified Alternative)**

If you prefer a simpler, file-based database:

#### Step 1: Install SQLite
```bash
# SQLite is pre-installed on macOS
sqlite3 --version
```

#### Step 2: Create SQLite Database
```bash
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/05ALab
sqlite3 ADSDentalSurgery.db
```

#### Step 3: Execute Modified Script
I'll create a SQLite-compatible version of the script.

---

## 🚀 Quick Start (Docker Method)

### Automated Setup:
```bash
# Navigate to the lab folder
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/05ALab

# Run the setup script
./setup-sqlserver-macos.sh

# Install Azure Data Studio (if not already installed)
brew install --cask azure-data-studio
```

### Manual Docker Setup:
```bash
# Pull SQL Server image
docker pull mcr.microsoft.com/mssql/server:2022-latest

# Run SQL Server container
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" \
   -p 1433:1433 --name sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest

# Check if running
docker ps
```

---

## 🔧 Troubleshooting

### Docker Issues:
```bash
# Check Docker status
docker info

# View container logs
docker logs sqlserver

# Restart container
docker restart sqlserver

# Remove and recreate
docker stop sqlserver
docker rm sqlserver
# Then run setup script again
```

### Connection Issues:
- **Port 1433 in use:** Change port mapping to `-p 1434:1433`
- **Authentication failed:** Ensure password is `YourStrong@Passw0rd`
- **Server not found:** Wait 30-60 seconds after container start

### Azure Data Studio Issues:
- **Connection timeout:** Check if Docker container is running
- **Login failed:** Verify username is `sa` and password is correct
- **Database not found:** Execute the SQL script first

---

## 📊 Database Management Commands

### Docker Commands:
```bash
# Start SQL Server
docker start sqlserver

# Stop SQL Server
docker stop sqlserver

# View logs
docker logs sqlserver

# Connect to SQL Server inside container
docker exec -it sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd'
```

### SQL Commands (inside container):
```sql
-- List databases
SELECT name FROM sys.databases;

-- Use ADS database
USE ADSDentalSurgery;

-- List tables
SELECT name FROM sys.tables;

-- Exit
EXIT
```

---

## 🎯 Next Steps After Setup

1. **✅ SQL Server Running** (Docker container active)
2. **✅ Azure Data Studio Installed** (SQL client ready)
3. **✅ Database Created** (ADSDentalSurgery database exists)
4. **✅ Sample Data Loaded** (All tables populated)
5. **📸 Execute Queries** (Take screenshots for submission)

### Required Screenshots:
- Query 1: All Dentists (sorted by last name)
- Query 2: Appointments for Dentist ID = 1
- Query 3: Appointments at Surgery Location ID = 1  
- Query 4: Patient Appointments on Specific Date

---

## 💡 Pro Tips

1. **Keep Docker Running:** SQL Server container must stay running
2. **Use Azure Data Studio:** Better than command line for queries
3. **Save Connection:** Save the connection in Azure Data Studio
4. **Backup Database:** Export database before making changes
5. **Use IntelliSense:** Azure Data Studio provides SQL autocomplete

---

**Note:** The Docker method is recommended as it provides the most authentic SQL Server experience and matches the production environment.

