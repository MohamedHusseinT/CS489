# ADS Dental Surgery Database Setup Instructions

## Lab 05A - CS489 Applied Software Development
**Student:** Mohamed

---

## Prerequisites

- **SQL Server** (SQL Server Express, Developer, or Standard Edition)
- **SQL Server Management Studio (SSMS)** or Azure Data Studio
- **Windows/macOS/Linux** with .NET 8.0 SDK

## Database Setup Steps

### Step 1: Install SQL Server (if not already installed)

#### Option A: SQL Server Express (Free)
1. Download SQL Server Express from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
2. Run the installer and follow the setup wizard
3. Choose "Basic" installation for simplicity
4. Note the server name (usually `(localdb)\MSSQLLocalDB` or `localhost\SQLEXPRESS`)

#### Option B: SQL Server Developer Edition (Free)
1. Download from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
2. Choose "Developer" edition (free for development)
3. Complete the installation

### Step 2: Install SQL Server Management Studio (SSMS)
1. Download SSMS from: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms
2. Install and launch SSMS

### Step 3: Create the Database

1. **Open SSMS** and connect to your SQL Server instance
2. **Open the SQL Script:**
   - File → Open → File
   - Navigate to: `05ALab/myADSDentalSurgeryDBScript.sql`
   - Click Open

3. **Execute the Script:**
   - Press `F5` or click "Execute"
   - The script will:
     - Create the `ADSDentalSurgery` database
     - Create all tables (Dentists, Patients, Surgeries, Appointments, Bills)
     - Insert sample data
     - Create indexes, views, and stored procedures

4. **Verify Database Creation:**
   - In Object Explorer, refresh the Databases folder
   - You should see `ADSDentalSurgery` database
   - Expand it to see all tables

### Step 4: Test the Database

1. **Run the Test Application:**
   ```bash
   cd 05ALab/ADSDatabaseTest
   dotnet run
   ```

2. **Execute Required Queries in SSMS:**
   - Open a new query window
   - Make sure `ADSDentalSurgery` database is selected
   - Copy and paste each required query from the script

## Required Queries for Screenshots

### Query 1: All Dentists (sorted by last name)
```sql
SELECT 
    dentist_id,
    first_name,
    last_name,
    contact_phone,
    email,
    specialization,
    created_date
FROM Dentists
ORDER BY last_name ASC;
```

### Query 2: Appointments for Dentist ID = 1 (Tony Smith)
```sql
SELECT 
    a.appointment_id,
    a.appointment_date,
    a.appointment_time,
    a.status,
    p.patient_id,
    p.first_name AS patient_first_name,
    p.last_name AS patient_last_name,
    p.contact_phone AS patient_phone,
    p.email AS patient_email,
    p.mailing_address AS patient_address,
    p.date_of_birth AS patient_dob,
    s.surgery_name,
    s.location_address AS surgery_address
FROM Appointments a
INNER JOIN Patients p ON a.patient_id = p.patient_id
INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id
WHERE a.dentist_id = 1
ORDER BY a.appointment_date, a.appointment_time;
```

### Query 3: Appointments at Surgery Location ID = 1
```sql
SELECT 
    a.appointment_id,
    a.appointment_date,
    a.appointment_time,
    a.status,
    d.first_name + ' ' + d.last_name AS dentist_name,
    d.specialization,
    p.first_name + ' ' + p.last_name AS patient_name,
    p.contact_phone AS patient_phone,
    p.email AS patient_email
FROM Appointments a
INNER JOIN Dentists d ON a.dentist_id = d.dentist_id
INNER JOIN Patients p ON a.patient_id = p.patient_id
WHERE a.surgery_id = 1
ORDER BY a.appointment_date, a.appointment_time;
```

### Query 4: Patient Appointments on Specific Date
```sql
SELECT 
    a.appointment_id,
    a.appointment_time,
    a.status,
    d.first_name + ' ' + d.last_name AS dentist_name,
    d.specialization,
    d.contact_phone AS dentist_phone,
    d.email AS dentist_email,
    s.surgery_name,
    s.location_address AS surgery_address,
    s.telephone AS surgery_phone
FROM Appointments a
INNER JOIN Dentists d ON a.dentist_id = d.dentist_id
INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id
WHERE a.patient_id = 3 AND a.appointment_date = '2013-09-12'
ORDER BY a.appointment_time;
```

## Screenshot Requirements

For each query, take a screenshot showing:
1. **The SQL query code** in the query window
2. **The execution results** in the results pane
3. **The database name** in the status bar (ADSDentalSurgery)

Save screenshots as:
- `Query1_Dentists.png`
- `Query2_DentistAppointments.png`
- `Query3_SurgeryAppointments.png`
- `Query4_PatientAppointments.png`

## Troubleshooting

### Common Issues:

1. **"Cannot connect to server"**
   - Check if SQL Server service is running
   - Verify server name in connection dialog
   - Try `(localdb)\MSSQLLocalDB` for LocalDB

2. **"Database does not exist"**
   - Make sure you executed the full script
   - Check if `ADSDentalSurgery` appears in Object Explorer

3. **"Invalid column name"**
   - Ensure you're connected to the correct database
   - Refresh Object Explorer to see latest schema

4. **Permission errors**
   - Run SSMS as Administrator
   - Check SQL Server authentication settings

### Alternative: Use Azure SQL Database

If local SQL Server is not available:
1. Create a free Azure SQL Database
2. Update connection string in the script
3. Execute the script against Azure SQL

## Database Schema Overview

- **5 Tables:** Dentists, Patients, Surgeries, Appointments, Bills
- **Sample Data:** 3 dentists, 4 patients, 3 surgeries, 6 appointments, 6 bills
- **Relationships:** Proper foreign key constraints
- **Business Rules:** Weekly appointment limits, outstanding bill checks
- **Performance:** Indexes on frequently queried columns

---

**Note:** This database design follows 3NF normalization and implements all business requirements from the problem statement.
