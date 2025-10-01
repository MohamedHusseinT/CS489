# Domain Model Analysis - Advantis Dental Surgeries (ADS)

## System Analysis & Design

### Main Entities Identified

1. **OfficeManager** - Manages the system operations
2. **Dentist** - Provides dental services
3. **Patient** - Receives dental services
4. **Appointment** - Scheduled meeting between Dentist and Patient
5. **Surgery** - Physical location where services are provided
6. **Bill** - Financial record for services provided

### Key Relationships

1. **OfficeManager** manages **Dentist** registrations (1:many)
2. **OfficeManager** manages **Patient** enrollments (1:many)
3. **OfficeManager** books **Appointment** (1:many)
4. **Dentist** has **Appointment** with **Patient** (many:many through Appointment)
5. **Appointment** is scheduled at **Surgery** (many:1)
6. **Patient** has **Bill** for services (1:many)
7. **Dentist** provides services that generate **Bill** (1:many)

### Business Rules

1. Each Dentist has a unique ID
2. Dentist cannot have more than 5 appointments per week
3. Patient with outstanding bills cannot request new appointments
4. Each appointment is associated with one Surgery location
5. Appointment confirmations are sent via email

### Attributes for Each Entity

#### OfficeManager
- managerId: String (Primary Key)
- firstName: String
- lastName: String
- email: String
- phoneNumber: String

#### Dentist
- dentistId: String (Primary Key)
- firstName: String
- lastName: String
- email: String
- phoneNumber: String
- specialization: String

#### Patient
- patientId: String (Primary Key)
- firstName: String
- lastName: String
- email: String
- phoneNumber: String
- mailingAddress: String
- dateOfBirth: Date

#### Appointment
- appointmentId: String (Primary Key)
- appointmentDate: Date
- appointmentTime: Time
- status: String (Scheduled, Completed, Cancelled)
- notes: String
- confirmationSent: Boolean

#### Surgery
- surgeryId: String (Primary Key)
- surgeryName: String
- address: String
- phoneNumber: String
- city: String

#### Bill
- billId: String (Primary Key)
- amount: Decimal
- dueDate: Date
- status: String (Paid, Outstanding, Overdue)
- serviceDescription: String
- billDate: Date

### Multiplicities and Roles

1. **OfficeManager** 1..* manages **Dentist** 1..1
2. **OfficeManager** 1..* manages **Patient** 1..1
3. **OfficeManager** 1..* books **Appointment** 1..1
4. **Dentist** 1..* has **Appointment** 1..1 with **Patient** 1..*
5. **Appointment** 1..1 is scheduled at **Surgery** 1..*
6. **Patient** 1..1 has **Bill** 1..*
7. **Dentist** 1..1 provides services for **Bill** 1..*
