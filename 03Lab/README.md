# Lab 3: Software Requirements Discovery and Domain Modeling

## Advantis Dental Surgeries (ADS) - Web Application System

### Overview
This lab assignment focuses on software requirements discovery and domain modeling for Advantis Dental Surgeries, LLC (ADS), a company managing a growing network of dental surgeries across the South West region.

### Problem Statement
ADS requires a web-based software solution to manage their dental surgery operations, including dentist registration, patient enrollment, appointment management, and surgery information.

### Lab Structure

#### Task 1.1: Software Requirements Discovery
**Location**: `03Lab/Task1.1/`
**Deliverable**: `Software_Requirements.md`

**Contents**:
- Functional Requirements (10 categories, 30+ requirements)
- Non-Functional Requirements (Performance, Security, Usability, Reliability)
- Business Rules (8 key rules)
- System Integration Requirements
- Data Management Requirements

#### Task 1.2: Domain Model UML Class Diagram
**Location**: `03Lab/Task1.2/`
**Deliverables**:
- `Domain_Model_UML.puml` - PlantUML source file
- `Domain_Model_Text_Diagram.txt` - Text-based diagram
- `Domain_Model_Analysis.md` - Detailed analysis
- `README.md` - Documentation

### Main Entities Identified

1. **OfficeManager** - Manages system operations
2. **Dentist** - Provides dental services
3. **Patient** - Receives dental services
4. **Appointment** - Scheduled meeting between Dentist and Patient
5. **Surgery** - Physical location where services are provided
6. **Bill** - Financial record for services provided

### Key Relationships

- OfficeManager manages Dentist registrations (1:many)
- OfficeManager enrolls Patients (1:many)
- OfficeManager books Appointments (1:many)
- Dentist has Appointments with Patients (many:many through Appointment)
- Appointment is scheduled at Surgery (many:1)
- Patient has Bills (1:many)
- Dentist provides services that generate Bills (1:many)

### Business Rules

1. Each Dentist has a unique ID number
2. A Dentist cannot have more than 5 appointments in any given week
3. Patients with outstanding, unpaid bills cannot request new appointments
4. All appointments must be associated with a specific Surgery location
5. Appointment confirmations must be sent via email to Patients
6. Only Office Managers can book appointments
7. Patients can only view and modify their own appointments
8. Dentists can only view their own appointments

### System Features

#### User Management
- Dentist registration with unique ID assignment
- Patient enrollment with complete information
- Role-based access control (Office Manager, Dentist, Patient)

#### Appointment Management
- Phone and online appointment requests
- Appointment booking by Office Managers
- Email confirmation system
- Appointment viewing for Dentists and Patients
- Appointment cancellation and modification

#### Business Logic
- Weekly appointment limits for Dentists (max 5)
- Outstanding bill checking for new appointments
- Surgery location association
- Status tracking and confirmation

#### Data Management
- Secure data storage and backup
- Data privacy and healthcare compliance
- Audit trail and reporting capabilities

### Technical Specifications

#### Functional Requirements Categories
1. User Management Requirements
2. Authentication and Authorization Requirements
3. Appointment Management Requirements
4. Appointment Viewing Requirements
5. Appointment Modification Requirements
6. Surgery Information Requirements
7. Billing and Payment Requirements
8. System Integration Requirements
9. Data Management Requirements
10. Reporting and Analytics Requirements

#### Non-Functional Requirements
- **Performance**: 3-second response time, concurrent user support
- **Security**: Secure authentication, data protection, healthcare compliance
- **Usability**: Intuitive interface, mobile accessibility
- **Reliability**: 99.9% uptime, backup and recovery

### UML Diagram Features

- **Classes**: 6 main entities with attributes and operations
- **Relationships**: 7 key relationships with multiplicities
- **Primary Keys**: Marked with <<PK>> stereotype
- **Foreign Keys**: Marked with <<FK>> stereotype
- **Business Rules**: Documented as notes on relevant classes
- **Cardinalities**: Clearly specified for all relationships

### How to View the UML Diagram

1. **PlantUML Online**: Copy `Domain_Model_UML.puml` content to [PlantUML Online Server](http://www.plantuml.com/plantuml/uml/)
2. **VS Code**: Install PlantUML extension and open the .puml file
3. **Local PlantUML**: Install PlantUML and run the .puml file
4. **Text Version**: View `Domain_Model_Text_Diagram.txt` for ASCII representation

### Files Structure

```
03Lab/
├── README.md (this file)
├── Task1.1/
│   └── Software_Requirements.md
└── Task1.2/
    ├── README.md
    ├── Domain_Model_Analysis.md
    ├── Domain_Model_UML.puml
    └── Domain_Model_Text_Diagram.txt
```

### Submission

This lab assignment is complete and ready for submission. All requirements have been identified and documented, and the domain model UML class diagram has been created with proper relationships, multiplicities, and business rules.

**Repository URL**: `https://github.com/MohamedHusseinT/CS489`
