# Task 1.2: Domain Model UML Class Diagram

## Advantis Dental Surgeries (ADS) - Domain Model

### Overview
This document contains the domain model UML class diagram for the Advantis Dental Surgeries web application system. The diagram shows the main entities, their attributes, relationships, and multiplicities.

### Main Entities

#### 1. OfficeManager
- **Role**: Manages system operations
- **Key Attributes**: managerId, firstName, lastName, email, phoneNumber
- **Key Operations**: registerDentist(), enrollPatient(), bookAppointment(), sendConfirmationEmail()

#### 2. Dentist
- **Role**: Provides dental services
- **Key Attributes**: dentistId, firstName, lastName, email, phoneNumber, specialization
- **Key Operations**: signIn(), viewAppointments(), getWeeklyAppointmentCount()
- **Business Rule**: Cannot have more than 5 appointments per week

#### 3. Patient
- **Role**: Receives dental services
- **Key Attributes**: patientId, firstName, lastName, email, phoneNumber, mailingAddress, dateOfBirth
- **Key Operations**: signIn(), requestAppointment(), viewAppointments(), cancelAppointment(), changeAppointment(), hasOutstandingBills()
- **Business Rule**: Cannot request new appointments if they have outstanding bills

#### 4. Appointment
- **Role**: Scheduled meeting between Dentist and Patient
- **Key Attributes**: appointmentId, appointmentDate, appointmentTime, status, notes, confirmationSent
- **Foreign Keys**: dentistId, patientId, surgeryId, managerId
- **Key Operations**: schedule(), confirm(), cancel(), reschedule()

#### 5. Surgery
- **Role**: Physical location where services are provided
- **Key Attributes**: surgeryId, surgeryName, address, phoneNumber, city
- **Key Operations**: getLocationInfo(), getContactInfo()

#### 6. Bill
- **Role**: Financial record for services provided
- **Key Attributes**: billId, amount, dueDate, status, serviceDescription, billDate
- **Foreign Keys**: patientId, dentistId
- **Key Operations**: calculateAmount(), markAsPaid(), checkOutstandingStatus()

### Relationships and Multiplicities

1. **OfficeManager** 1..* manages **Dentist** 1..1
2. **OfficeManager** 1..* enrolls **Patient** 1..1
3. **OfficeManager** 1..* books **Appointment** 1..1
4. **Dentist** 1..1 has **Appointment** 1..* with **Patient** 1..*
5. **Appointment** *..1 is scheduled at **Surgery** 1..1
6. **Patient** 1..1 has **Bill** 1..*
7. **Dentist** 1..1 provides services for **Bill** 1..*

### Key Business Rules

1. Each Dentist has a unique ID number
2. A Dentist cannot have more than 5 appointments in any given week
3. Patients with outstanding, unpaid bills cannot request new appointments
4. All appointments must be associated with a specific Surgery location
5. Appointment confirmations must be sent via email to Patients
6. Only Office Managers can book appointments
7. Patients can only view and modify their own appointments
8. Dentists can only view their own appointments

### Files Included

- `Domain_Model_UML.puml` - PlantUML source file for the class diagram
- `Domain_Model_Analysis.md` - Detailed analysis of entities and relationships
- `README.md` - This documentation file

### How to View the UML Diagram

1. **Using PlantUML Online**: Copy the contents of `Domain_Model_UML.puml` and paste it into [PlantUML Online Server](http://www.plantuml.com/plantuml/uml/)

2. **Using PlantUML Local**: Install PlantUML and run:
   ```bash
   java -jar plantuml.jar Domain_Model_UML.puml
   ```

3. **Using VS Code**: Install the PlantUML extension and open the .puml file

### Diagram Features

- **Classes**: Each entity is represented as a class with attributes and operations
- **Relationships**: Shows associations, multiplicities, and roles
- **Primary Keys**: Marked with <<PK>> stereotype
- **Foreign Keys**: Marked with <<FK>> stereotype
- **Business Rules**: Documented as notes on relevant classes
- **Cardinalities**: Clearly specified for all relationships

### System Architecture Considerations

The domain model supports:
- **Scalability**: Multiple surgeries, dentists, and patients
- **Security**: Role-based access control through different user types
- **Data Integrity**: Foreign key relationships ensure referential integrity
- **Business Logic**: Built-in constraints for appointment limits and billing
- **Audit Trail**: Tracking of appointment status and confirmation
