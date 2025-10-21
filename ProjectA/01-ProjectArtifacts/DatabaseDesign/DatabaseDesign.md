# Database Design - ER Diagram - Advantis Dental Surgeries (ADS)

## Entity Relationship Diagram

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                        ADS DATABASE SCHEMA                                     │
└─────────────────────────────────────────────────────────────────────────────────┘

┌─────────────────┐
│    Patients     │
├─────────────────┤
│ PK: Id (int)    │
│     PatientNumber (varchar) UNIQUE
│     FirstName (varchar)
│     LastName (varchar)
│     PhoneNumber (varchar)
│     Email (varchar) UNIQUE
│     DateOfBirth (date)
│     MailingAddress (varchar)
│     AddressId (int) FK
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
│     IsActive (boolean)
└─────────────────┘
         │
         │ 1:1
         │
         ▼
┌─────────────────┐
│    Addresses    │
├─────────────────┤
│ PK: Id (int)    │
│     Street (varchar)
│     City (varchar)
│     State (varchar)
│     ZipCode (varchar)
│     Country (varchar)
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
│     IsActive (boolean)
└─────────────────┘
         │
         │ 1:1
         │
         ▼
┌─────────────────┐
│    Surgeries    │
├─────────────────┤
│ PK: Id (int)    │
│     Name (varchar) UNIQUE
│     PhoneNumber (varchar)
│     Email (varchar) UNIQUE
│     AddressId (int) FK
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
│     IsActive (boolean)
└─────────────────┘
         │
         │ 1:M
         │
         ▼
┌─────────────────┐
│   Appointments  │
├─────────────────┤
│ PK: Id (int)    │
│ FK: PatientId (int)
│ FK: DentistId (int)
│ FK: SurgeryId (int)
│     AppointmentDate (date)
│     StartTime (time)
│     EndTime (time)
│     Notes (text)
│     Status (varchar)
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
│     IsActive (boolean)
└─────────────────┘
         │
         │ M:1
         │
         ▼
┌─────────────────┐
│    Dentists     │
├─────────────────┤
│ PK: Id (int)    │
│     FirstName (varchar)
│     LastName (varchar)
│     Specialization (varchar)
│     PhoneNumber (varchar)
│     Email (varchar) UNIQUE
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
│     IsActive (boolean)
└─────────────────┘
```

## Database Tables Description

### Core Tables

#### Patients
- **Purpose**: Stores patient information and demographics
- **Primary Key**: Id (auto-increment)
- **Unique Constraints**: PatientNumber, Email
- **Indexes**: PatientNumber (unique), Email (unique), FirstName, LastName, IsActive
- **Relationships**: One-to-One with Addresses, One-to-Many with Appointments

#### Addresses
- **Purpose**: Stores physical addresses for patients and surgeries
- **Primary Key**: Id (auto-increment)
- **Indexes**: Street, City, State, ZipCode, IsActive
- **Relationships**: One-to-One with Patients, One-to-One with Surgeries

#### Dentists
- **Purpose**: Stores dentist information and specializations
- **Primary Key**: Id (auto-increment)
- **Unique Constraints**: Email
- **Indexes**: Email (unique), FirstName, LastName, Specialization, IsActive
- **Relationships**: One-to-Many with Appointments

#### Surgeries
- **Purpose**: Stores surgery room information and locations
- **Primary Key**: Id (auto-increment)
- **Foreign Key**: AddressId (references Addresses.Id)
- **Unique Constraints**: Name, Email
- **Indexes**: Name (unique), Email (unique), AddressId, IsActive
- **Relationships**: One-to-One with Addresses, One-to-Many with Appointments

#### Appointments
- **Purpose**: Stores appointment scheduling information
- **Primary Key**: Id (auto-increment)
- **Foreign Keys**: PatientId, DentistId, SurgeryId
- **Indexes**: PatientId, DentistId, SurgeryId, AppointmentDate, Status, IsActive
- **Relationships**: Many-to-One with Patients, Dentists, Surgeries

## Database Constraints

### Primary Keys
- All tables have auto-incrementing integer primary keys
- Primary keys are clustered indexes for performance

### Foreign Keys
- All foreign key relationships are enforced
- Cascade delete is avoided to prevent data loss
- Soft delete pattern used instead

### Check Constraints
- Status fields have predefined value lists (Scheduled, Completed, Cancelled)
- Date fields validate reasonable ranges
- Email fields validate format
- Phone fields validate format

### Unique Constraints
- Patient numbers must be unique
- Patient emails must be unique
- Dentist emails must be unique
- Surgery names must be unique
- Surgery emails must be unique

## Indexing Strategy

### Primary Indexes
- **Clustered**: Primary keys (Id columns)
- **Non-clustered**: Foreign keys, unique constraints

### Performance Indexes
- **PatientNumber**: For patient lookup and identification
- **Email**: For user authentication and contact
- **Status/IsActive**: For filtering active records
- **AppointmentDate**: For date range queries
- **Composite**: (DentistId, AppointmentDate) for dentist schedules

### Full-Text Indexes
- **Search Fields**: FirstName, LastName fields for patient search
- **Content Fields**: Notes field for appointment notes search

## Data Types and Sizing

### String Fields
- **Names**: VARCHAR(100) - Reasonable name length
- **Emails**: VARCHAR(255) - Standard email length
- **Phone Numbers**: VARCHAR(20) - Phone number with formatting
- **Addresses**: VARCHAR(200) - Street addresses
- **Notes**: TEXT - Unlimited text for appointment notes

### Numeric Fields
- **IDs**: INT - Standard for primary keys
- **Dates**: DATE - Date-only fields
- **Times**: TIME - Time-only fields

### Date Fields
- **Timestamps**: DATETIME2 - High precision timestamps
- **Dates**: DATE - Date-only fields
- **Default**: GETUTCDATE() - UTC timestamps

## Security Considerations

### Data Encryption
- **At Rest**: Database-level encryption
- **In Transit**: TLS/SSL connections
- **Application Level**: Sensitive data hashing

### Access Control
- **Database Users**: Limited permissions per application
- **Row-Level Security**: User-specific data access
- **Audit Trail**: All changes tracked

### Backup Strategy
- **Full Backups**: Daily automated backups
- **Transaction Log**: Continuous backup for point-in-time recovery
- **Retention**: 30 days for full backups, 7 days for logs

## Migration Strategy

### Version Control
- **Entity Framework Migrations**: Database schema versioning
- **Naming Convention**: YYYYMMDD_HHMMSS_Description
- **Rollback Support**: Down migrations for all changes

### Deployment Process
1. **Development**: Local database with migrations
2. **Staging**: Automated migration deployment
3. **Production**: Manual review and deployment
4. **Rollback Plan**: Tested rollback procedures

## Sample Data

### Patients Sample Data
```sql
INSERT INTO Patients (PatientNumber, FirstName, LastName, PhoneNumber, Email, DateOfBirth, MailingAddress, AddressId, CreatedDate, IsActive)
VALUES 
('P001', 'John', 'Doe', '555-1234', 'john.doe@email.com', '1985-05-15', '123 Main St, Anytown, CA 12345', 1, GETUTCDATE(), 1),
('P002', 'Jane', 'Smith', '555-5678', 'jane.smith@email.com', '1990-08-22', '456 Oak Ave, Somewhere, NY 67890', 2, GETUTCDATE(), 1);
```

### Dentists Sample Data
```sql
INSERT INTO Dentists (FirstName, LastName, Specialization, PhoneNumber, Email, CreatedDate, IsActive)
VALUES 
('Dr. Sarah', 'Johnson', 'General Dentistry', '555-1111', 'sarah.johnson@ads.com', GETUTCDATE(), 1),
('Dr. Michael', 'Brown', 'Orthodontics', '555-2222', 'michael.brown@ads.com', GETUTCDATE(), 1);
```

### Surgeries Sample Data
```sql
INSERT INTO Surgeries (Name, PhoneNumber, Email, AddressId, CreatedDate, IsActive)
VALUES 
('Main Surgery Room', '555-3333', 'main@ads.com', 3, GETUTCDATE(), 1),
('Orthodontic Suite', '555-4444', 'ortho@ads.com', 4, GETUTCDATE(), 1);
```

---
*This database design represents the core data structure for the Advantis Dental Surgeries management system, optimized for performance, security, and maintainability.*
