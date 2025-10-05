# Advantis Dental Surgeries (ADS) - E-R Model Documentation

## Lab 05A - CS489 Applied Software Development
**Student:** Mohamed  
**Date:** October 2025

---

## 1. E-R Model Overview

The Advantis Dental Surgeries (ADS) database system is designed to manage dental surgery operations including dentist registration, patient enrollment, appointment scheduling, and billing management.

## 2. Entities and Attributes

### 2.1 Dentist Entity
- **Primary Key:** dentist_id (INT, IDENTITY)
- **Attributes:**
  - first_name (NVARCHAR(50), NOT NULL)
  - last_name (NVARCHAR(50), NOT NULL)
  - contact_phone (NVARCHAR(20), NOT NULL)
  - email (NVARCHAR(100), NOT NULL, UNIQUE)
  - specialization (NVARCHAR(100), NOT NULL)
  - created_date (DATETIME2, DEFAULT GETDATE())

### 2.2 Patient Entity
- **Primary Key:** patient_id (INT, IDENTITY)
- **Attributes:**
  - first_name (NVARCHAR(50), NOT NULL)
  - last_name (NVARCHAR(50), NOT NULL)
  - contact_phone (NVARCHAR(20), NOT NULL)
  - email (NVARCHAR(100), NOT NULL, UNIQUE)
  - mailing_address (NVARCHAR(255), NOT NULL)
  - date_of_birth (DATE, NOT NULL)
  - created_date (DATETIME2, DEFAULT GETDATE())

### 2.3 Surgery Entity
- **Primary Key:** surgery_id (INT, IDENTITY)
- **Attributes:**
  - surgery_name (NVARCHAR(100), NOT NULL)
  - location_address (NVARCHAR(255), NOT NULL)
  - telephone (NVARCHAR(20), NOT NULL)
  - created_date (DATETIME2, DEFAULT GETDATE())

### 2.4 Appointment Entity
- **Primary Key:** appointment_id (INT, IDENTITY)
- **Attributes:**
  - dentist_id (INT, NOT NULL, FK)
  - patient_id (INT, NOT NULL, FK)
  - surgery_id (INT, NOT NULL, FK)
  - appointment_date (DATE, NOT NULL)
  - appointment_time (TIME, NOT NULL)
  - status (NVARCHAR(20), DEFAULT 'Scheduled', CHECK constraint)
  - notes (NVARCHAR(500))
  - created_date (DATETIME2, DEFAULT GETDATE())

### 2.5 Bill Entity
- **Primary Key:** bill_id (INT, IDENTITY)
- **Attributes:**
  - patient_id (INT, NOT NULL, FK)
  - appointment_id (INT, FK)
  - amount (DECIMAL(10,2), NOT NULL)
  - bill_date (DATE, NOT NULL)
  - due_date (DATE, NOT NULL)
  - status (NVARCHAR(20), DEFAULT 'Outstanding', CHECK constraint)
  - description (NVARCHAR(255))
  - created_date (DATETIME2, DEFAULT GETDATE())

## 3. Relationships

### 3.1 Dentist ↔ Appointment (1:M)
- **Relationship:** One dentist can have many appointments
- **Cardinality:** 1 to Many
- **Foreign Key:** appointment.dentist_id → dentist.dentist_id

### 3.2 Patient ↔ Appointment (1:M)
- **Relationship:** One patient can have many appointments
- **Cardinality:** 1 to Many
- **Foreign Key:** appointment.patient_id → patient.patient_id

### 3.3 Surgery ↔ Appointment (1:M)
- **Relationship:** One surgery location can host many appointments
- **Cardinality:** 1 to Many
- **Foreign Key:** appointment.surgery_id → surgery.surgery_id

### 3.4 Patient ↔ Bill (1:M)
- **Relationship:** One patient can have many bills
- **Cardinality:** 1 to Many
- **Foreign Key:** bill.patient_id → patient.patient_id

### 3.5 Appointment ↔ Bill (1:M)
- **Relationship:** One appointment can generate many bills
- **Cardinality:** 1 to Many
- **Foreign Key:** bill.appointment_id → appointment.appointment_id

## 4. Business Rules Implemented

### 4.1 Appointment Constraints
- **Weekly Limit:** Dentists cannot have more than 5 appointments per week
- **Status Validation:** Appointment status must be one of: 'Scheduled', 'Completed', 'Cancelled', 'No-Show'

### 4.2 Billing Constraints
- **Outstanding Bills:** Patients with outstanding bills cannot request new appointments
- **Status Validation:** Bill status must be one of: 'Outstanding', 'Paid', 'Overdue'

### 4.3 Data Integrity
- **Unique Constraints:** Email addresses must be unique for both dentists and patients
- **Required Fields:** All name, contact, and identification fields are mandatory
- **Date Validation:** Appointment dates and times are properly validated

## 5. Sample Data Mapping

Based on the provided sample data, the following mappings were used:

| Sample Data | Database Mapping |
|-------------|------------------|
| Tony Smith | dentist_id = 1 |
| Helen Pearson | dentist_id = 2 |
| Robin Plevin | dentist_id = 3 |
| P100 (Gillian White) | patient_id = 1 |
| P105 (Jill Bell) | patient_id = 2 |
| P108 (Ian MacKay) | patient_id = 3 |
| P110 (John Walker) | patient_id = 4 |
| S10 | surgery_id = 1 (ADS Downtown Surgery) |
| S13 | surgery_id = 3 (ADS South Side Surgery) |
| S15 | surgery_id = 2 (ADS North End Surgery) |

## 6. Database Features

### 6.1 Indexes
- Performance indexes on frequently queried columns
- Composite indexes for complex queries

### 6.2 Views
- **vw_AppointmentDetails:** Complete appointment information with related entities
- **vw_PatientOutstandingBills:** Patient billing summary

### 6.3 Stored Procedures
- **sp_CheckPatientOutstandingBills:** Validates patient payment status
- **sp_CheckDentistWeeklyLimit:** Enforces weekly appointment limits

## 7. Required SQL Queries

The database supports all required queries:

1. **All Dentists (sorted by last name)**
2. **Appointments by Dentist ID (with patient info)**
3. **Appointments by Surgery Location**
4. **Patient Appointments by Date**

## 8. Additional Features

- Comprehensive error handling
- Audit trails with created_date timestamps
- Flexible status management
- Scalable design for future enhancements
- Proper normalization to 3NF

---

**Note:** This E-R model follows standard database design principles and implements all business requirements specified in the problem statement.
