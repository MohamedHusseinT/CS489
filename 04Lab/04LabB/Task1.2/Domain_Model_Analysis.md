# ADS Dental Surgery System - Domain Model Analysis

## Domain Modeling Approach

The ADS Dental Surgery system domain model is designed using Object-Oriented Analysis principles with entities representing real-world concepts and relationships modeling business interactions.

## Core Domain Entities

### 1. User (Abstract Base Class)
- **Purpose**: Represents common attributes for all system users
- **Attributes**: 
  - `userId` (long, Primary Key)
  - `firstName` (string, required)
  - `lastName` (string, required) 
  - `email` (string, required, unique)
  - `phoneNumber` (string, required)
  - `passwordHash` (string, required)
  - `createdDate` (DateTime)
  - `lastLogin` (DateTime, optional)

### 2. Dentist (Inherits from User)
- **Purpose**: Represents dental professionals in the ADS network
- **Attributes**:
  - `dentistId` (long, Primary Key - inherited)
  - `specialization` (string, required)
  - `licenseNumber` (string, required, unique)
  - `hireDate` (DateTime, required)
  - `activeStatus` (boolean, default true)

### 3. Patient (Inherits from User)
- **Purpose**: Represents individuals seeking dental services
- **Attributes**:
  - `patientId` (long, Primary Key - inherited)
  - `dateOfBirth` (DateTime, required)
  - `mailingAddress` (string, required)
  - `emergencyContact` (string, optional)
  - `insuranceInformation` (string, optional)

### 4. OfficeManager (Inherits from User)
- **Purpose**: Represents administrative staff managing the system
- **Attributes**:
  - `employeeId` (string, required, unique)
  - `department` (string, required)
  - `permissions` (string, required)

### 5. Surgery (Location)
- **Purpose**: Represents physical locations where dental services are provided
- **Attributes**:
  - `surgeryId` (long, Primary Key)
  - `name` (string, required)
  - `locationAddress` (string, required)
  - `telephoneNumber` (string, required)
  - `email` (string, required)
  - `capacity` (int, required)
  - `activeStatus` (boolean, default true)

### 6. Appointment
- **Purpose**: Represents scheduled meetings between patients and dentists
- **Attributes**:
  - `appointmentId` (long, Primary Key)
  - `appointmentDate` (DateTime, required)
  - `appointmentTime` (TimeSpan, required)
  - `duration` (int, required - in minutes)
  - `status` (enum: Scheduled, Completed, Cancelled, Rescheduled)
  - `notes` (string, optional)
  - `confirmationDate` (DateTime, optional)
  - `createdDate` (DateTime, required)

### 7. Bill
- **Purpose**: Represents charges for services rendered
- **Attributes**:
  - `billId` (long, Primary Key)
  - `billAmount` (decimal, required)
  - `billDate` (DateTime, required)
  - `dueDate` (DateTime, required)
  - `status` (enum: Pending, Paid, Overdue)
  - `serviceDescription` (string, required)
  - `paymentMethod` (string, optional)
  - `paymentDate` (DateTime, optional)

### 8. AppointmentRequest
- **Purpose**: Represents patient requests for appointment scheduling
- **Attributes**:
  - `requestId` (long, Primary Key)
  - `requestDate` (DateTime, required)
  - `preferredDate` (DateTime, optional)
  - `preferredTime` (string, optional)
  - `urgency` (enum: Low, Medium, High)
  - `status` (enum: Pending, Approved, Rejected)
  - `notes` (string, optional)
  - `contactMethod` (string, required - Phone or Online)

## Domain Relationships

### 1. Dentist-Surgery Relationship
- **Type**: Many-to-Many
- **Multiplicity**: A Dentist can work at multiple Surgery locations, and a Surgery can have multiple Dentists
- **Role**: "WorksAt"

### 2. Patient-Surgery Relationship  
- **Type**: One-to-Many (1:∞)
- **Multiplicity**: A Patient can have appointments at multiple Surgery locations
- **Role**: "ReceivesServicesAt"

### 3. Appointment Relationships
- **Appointment-Patient**: Many-to-One (∞:1)
  - Multiplicity: A Patient can have multiple appointments; An appointment belongs to one Patient
  - Role: "ScheduledFor"

- **Appointment-Dentist**: Many-to-One (∞:1)  
  - Multiplicity: A Dentist can have multiple appointments; An appointment belongs to one Dentist
  - Role: "AssignedTo"

- **Appointment-Surgery**: Many-to-One (∞:1)
  - Multiplicity: A Surgery can have multiple appointments; An appointment belongs to one Surgery location
  - Role: "LocatedAt"

### 4. Bill-Patient Relationship
- **Type**: One-to-Many (1:∞)
- **Multiplicity**: A Patient can have multiple bills; A bill belongs to one Patient
- **Role**: "OwedBy"

### 5. AppointmentRequest-Patient Relationship
- **Type**: One-to-Many (1:∞)
- **Multiplicity**: A Patient can make multiple appointment requests; A request belongs to one Patient
- **Role**: "RequestedBy"

## Business Rules Representation

### Constraints and Validations
1. **Dentist Weekly Limit**: Business rule preventing dentists from having more than 5 appointments per week
2. **Outstanding Bill Check**: Business rule preventing patients with unpaid bills from scheduling new appointments
3. **Future Date Validation**: Business rule ensuring appointment dates are in the future
4. **Unique Constraints**: License numbers, emails, and IDs must be unique

### Enums and Classifications
1. **Appointment Status**: Scheduled, Completed, Cancelled, Rescheduled
2. **Bill Status**: Pending, Paid, Overdue  
3. **Request Urgency**: Low, Medium, High
4. **Contact Methods**: Phone, Online, In-Person

This domain model provides a comprehensive foundation for the ADS Dental Surgery system, capturing all essential entities, relationships, and business rules necessary for effective system design and implementation.
