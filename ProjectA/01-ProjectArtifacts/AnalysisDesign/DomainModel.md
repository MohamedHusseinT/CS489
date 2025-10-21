# Analysis & Design - Domain Model - Advantis Dental Surgeries (ADS)

## Domain Model Class Diagram

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                        ADS Domain Model                                        │
└─────────────────────────────────────────────────────────────────────────────────┘

┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│     Patient     │    │     Address     │    │     Dentist     │    │     Surgery     │
├─────────────────┤    ├─────────────────┤    ├─────────────────┤    ├─────────────────┤
│ + Id: int       │    │ + Id: int       │    │ + Id: int       │    │ + Id: int       │
│ + PatientNumber:│    │ + Street: str   │    │ + FirstName: str│    │ + Name: string  │
│   string        │    │ + City: string  │    │ + LastName: str │    │ + PhoneNumber: │
│ + FirstName: str│    │ + State: string │    │ + Specialization│    │   string        │
│ + LastName: str │    │ + ZipCode: str  │    │ + PhoneNumber:  │    │ + Email: string │
│ + PhoneNumber:  │    │ + Country: str   │    │ + Email: string│    │ + AddressId:   │
│   string        │    │ + CreatedDate:  │    │ + CreatedDate:  │    │   int           │
│ + Email: string │    │   DateTime      │    │   DateTime      │    │ + CreatedDate:  │
│ + DateOfBirth:  │    │ + UpdatedDate:  │    │ + UpdatedDate:  │    │   DateTime      │
│   DateTime      │    │   DateTime      │    │   DateTime      │    │ + UpdatedDate:  │
│ + MailingAddress│    │ + IsActive: bool│    │ + IsActive: bool│    │   DateTime      │
│   string        │    └─────────────────┘    │ + IsActive: bool│    │ + IsActive: bool│
│ + AddressId: int│                            └─────────────────┘    └─────────────────┘
│ + CreatedDate:  │                                     │                       │
│   DateTime      │                                     │                       │
│ + UpdatedDate:  │                                     │                       │
│   DateTime      │                                     │                       │
│ + IsActive: bool│                                     │                       │
└─────────────────┘                                     │                       │
         │                                               │                       │
         │ 1:1                                          │                       │
         │                                               │                       │
         ▼                                               │                       │
┌─────────────────┐                                     │                       │
│   PatientAddress│                                     │                       │
├─────────────────┤                                     │                       │
│ + PatientId: int│                                     │                       │
│ + AddressId: int│                                     │                       │
│ + AddressType:  │                                     │                       │
│   string        │                                     │                       │
│ + IsPrimary:    │                                     │                       │
│   bool          │                                     │                       │
│ + CreatedDate:  │                                     │                       │
│   DateTime      │                                     │                       │
└─────────────────┘                                     │                       │
                                                         │                       │
                                                         ▼                       ▼
                                              ┌─────────────────┐    ┌─────────────────┐
                                              │   Appointment   │    │   SurgeryAddress│
                                              ├─────────────────┤    ├─────────────────┤
                                              │ + Id: int       │    │ + SurgeryId: int│
                                              │ + PatientId: int│    │ + AddressId: int│
                                              │ + DentistId: int│    │ + CreatedDate:  │
                                              │ + SurgeryId: int│    │   DateTime      │
                                              │ + AppointmentDate│    └─────────────────┘
                                              │   DateTime      │
                                              │ + StartTime:   │
                                              │   DateTime      │
                                              │ + EndTime:      │
                                              │   DateTime      │
                                              │ + Notes: string │
                                              │ + Status: string│
                                              │ + CreatedDate:  │
                                              │   DateTime      │
                                              │ + UpdatedDate:  │
                                              │   DateTime      │
                                              │ + IsActive: bool│
                                              └─────────────────┘
```

## Entity Descriptions

### Core Entities

#### Patient
- **Purpose**: Represents dental patients and their information
- **Key Attributes**: PatientNumber (unique), FirstName, LastName, DateOfBirth, ContactInfo
- **Relationships**: One-to-One with Address, One-to-Many with Appointments
- **Business Rules**: PatientNumber auto-generated, email must be unique

#### Address
- **Purpose**: Stores physical addresses for patients and surgeries
- **Key Attributes**: Street, City, State, ZipCode, Country
- **Relationships**: One-to-Many with Patients, One-to-Many with Surgeries
- **Business Rules**: ZipCode format validation, required fields validation

#### Dentist
- **Purpose**: Represents dental professionals and their information
- **Key Attributes**: FirstName, LastName, Specialization, ContactInfo
- **Relationships**: One-to-Many with Appointments
- **Business Rules**: Specialization must be from predefined list, email must be unique

#### Surgery
- **Purpose**: Represents surgery rooms and their locations
- **Key Attributes**: Name, PhoneNumber, Email, AddressId
- **Relationships**: One-to-One with Address, One-to-Many with Appointments
- **Business Rules**: Name must be unique, contact information required

#### Appointment
- **Purpose**: Represents scheduled appointments between patients, dentists, and surgeries
- **Key Attributes**: AppointmentDate, StartTime, EndTime, Notes, Status
- **Relationships**: Many-to-One with Patient, Dentist, Surgery
- **Business Rules**: No overlapping appointments, valid time ranges

### Junction Entities

#### PatientAddress
- **Purpose**: Links patients to their addresses with type information
- **Key Attributes**: AddressType (Home, Work, Emergency), IsPrimary
- **Business Rules**: Only one primary address per patient, address types validated

#### SurgeryAddress
- **Purpose**: Links surgeries to their physical locations
- **Key Attributes**: SurgeryId, AddressId
- **Business Rules**: Each surgery must have exactly one address

## Design Patterns

### Repository Pattern
- **Purpose**: Abstracts data access logic
- **Implementation**: Generic repository with specific entity repositories (PatientRepository, AddressRepository, etc.)

### Service Pattern
- **Purpose**: Encapsulates business logic
- **Implementation**: Service classes for each entity (PatientService, AddressService, etc.)

### DTO Pattern
- **Purpose**: Transfers data between layers
- **Implementation**: Separate DTOs for requests (PatientRequest) and responses (PatientResponse)

## Business Rules

### Patient Management
1. Patient numbers are auto-generated and unique
2. Email addresses must be unique across all patients
3. Date of birth cannot be in the future
4. Phone numbers must follow valid format
5. Patients can have multiple addresses but only one primary

### Appointment Management
1. Appointments cannot be scheduled in the past
2. No overlapping appointments for the same dentist
3. No overlapping appointments for the same surgery
4. Appointment duration must be reasonable (15 minutes to 4 hours)
5. Appointments require all three entities: Patient, Dentist, Surgery

### Address Management
1. All addresses must have complete street, city, state, zip code
2. Zip codes must follow valid format for the country
3. Each surgery must have exactly one address
4. Patients can have multiple addresses with different types

### Data Integrity
1. All entities have CreatedDate and UpdatedDate
2. Soft delete pattern for important entities (IsActive flag)
3. Foreign key constraints maintain referential integrity
4. Unique constraints prevent duplicate data

## Validation Rules

### Patient Entity
- PatientNumber: Required, auto-generated, unique
- FirstName: Required, 2-50 characters, letters only
- LastName: Required, 2-50 characters, letters only
- Email: Required, valid email format, unique
- PhoneNumber: Required, valid phone format
- DateOfBirth: Required, cannot be future date
- IsActive: Required, boolean

### Address Entity
- Street: Required, 5-100 characters
- City: Required, 2-50 characters
- State: Required, 2-50 characters
- ZipCode: Required, valid format for country
- Country: Required, 2-50 characters
- IsActive: Required, boolean

### Dentist Entity
- FirstName: Required, 2-50 characters
- LastName: Required, 2-50 characters
- Specialization: Required, from predefined list
- PhoneNumber: Required, valid phone format
- Email: Required, valid email format, unique
- IsActive: Required, boolean

### Surgery Entity
- Name: Required, 2-100 characters, unique
- PhoneNumber: Required, valid phone format
- Email: Required, valid email format, unique
- AddressId: Required, valid foreign key
- IsActive: Required, boolean

### Appointment Entity
- PatientId: Required, valid foreign key
- DentistId: Required, valid foreign key
- SurgeryId: Required, valid foreign key
- AppointmentDate: Required, cannot be past date
- StartTime: Required, valid time format
- EndTime: Required, must be after StartTime
- Status: Required, from predefined list (Scheduled, Completed, Cancelled)
- IsActive: Required, boolean

---
*This domain model represents the core business entities and relationships for the Advantis Dental Surgeries management system.*
