# Lab 4B - ADS Dental Surgery System Architecture

## Overview

This lab focuses on creating a comprehensive software solution architecture diagram for the ADS (Advantis Dental Surgery) system, demonstrating modern enterprise application design using .NET technologies.

## Lab Structure

### Task 1.1: Software Requirements Discovery
- **Location**: `Task1.1/`
- **Document**: `Software_Requirements.md`
- **Content**: Functional and non-functional requirements for ADS system
- **Focus**: Requirements gathering and documentation

### Task 1.2: Domain Modeling & Architecture Diagram
- **Location**: `Task1.2/`
- **Documents**: 
  - `Domain_Model_Analysis.md` - UML domain model analysis
  - `ADS_Software_Architecture_Diagram.md` - Architecture documentation
  - `Class_Diagram.puml` - PlantUML class diagram source
  - `README.md` - Task-specific documentation

## Completed Deliverables

✅ **Task 1.1 - Software Requirements**
- **15 Functional Requirements** covering:
  - User Management System (F1.1-F1.5)
  - Appointment Management System (F2.1-F2.8)
  - Surgery Location Management (F3.1-F3.3)
  - Business Rules and Constraints ( F4.1-F4.4)
  - Reporting and Notifications (F5.1-F5.4)

- **15 Non-Functional Requirements** covering:
  - Performance Requirements (NF1-NF3)
  - Security Requirements (NF4-NF6)
  - Reliability Requirements (NF7-NF9)
  - Usability Requirements (NF10-NF12)
  - Scalability Requirements (NF13-NF15)

✅ **Task 1.2 - Domain Modeling & Architecture**
- **Domain Model Analysis** including:
  - Core Entity identification (User, Dentist, Patient, OfficeManager, Surgery, Appointment, Bill, AppointmentRequest)
  - Relationship mapping with multiplicities
  - Business rules and constraints
  - Domain validation rules

- **Software Architecture Diagram** featuring:
  - Multi-tier architecture (Client, Middle, Data tiers)
  - .NET technology stack components
  - Logical layer separation
  - Security architecture
  - Deployment considerations

## Architecture Overview

### Physical Tiers
1. **Client Tier**: Desktop/Mobile browsers with HTTPS
2. **Middle Tier**: .NET Core Application Server with load balancing
3. **Data Tier**: SQL Server clusters with automated backup

### Logical Layers
1. **Presentation Layer**: Blazor Server Components + Bootstrap CSS
2. **API Layer**: ASP.NET Core Web API Controllers
3. **Business Layer**: Domain services and business logic
4. **Data Access Layer**: Entity Framework Core with Repository pattern
5. **Data Layer**: SQL Server/Azure SQL Database

### Key Technologies
- **Frontend**: Blazor Server, Bootstrap, Font Awesome
- **Backend**: ASP.NET Core Web API
- **Database**: Entity Framework Core, SQL Server
- **Security**: ASP.NET Core Identity, JWT tokens
- **Deployment**: Docker, Kubernetes, Azure services

## Domain Model Highlights

### Core Entities
- **User (Abstract)**: Common attributes for all users
- **Dentist**: Specialist dental professionals with licenses
- **Patient**: Service recipients with demographics
- **OfficeManager**: Administrative staff with permissions
- **Surgery**: Physical locations where services are provided
- **Appointment**: Scheduled meetings between dentists and patients
- **Bill**: Financial transactions for services
- **AppointmentRequest**: Patient-initiated scheduling requests

### Key Relationships
- **Many-to-Many**: Dentist ↔ Surgery (multiple locations)
- **One-to-Many**: Patient → Appointment, Patient → Bill
- **Many-to-One**: Appointment → Dentist, Appointment → Surgery

### Business Rules Implemented
1. Dentists limited to 5 appointments per week
2. Patients with outstanding bills cannot request new appointments
3. Future date validation for all appointments
4. Unique constraints on licenses, emails, and IDs

## Architecture Benefits

1. **Scalability**: Microservices-ready architecture
2. **Security**: Multi-layered security with encryption
3. **Maintainability**: Clear separation of concerns
4. **Performance**: Caching and load balancing support
5. **Cloud-Ready**: Azure integration and containerization

This comprehensive architecture provides a solid foundation for implementing the ADS Dental Surgery system with enterprise-grade features and modern development practices.
