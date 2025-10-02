# ADS Dental Surgery System - Software Solution Architecture Diagram

## Architecture Overview

The ADS (Advantis Dental Surgery) system follows a Multi-Tier Architecture pattern designed for enterprise web applications, using .NET technology stack equivalent to the Java EE Spring Boot architecture shown in the reference diagram.

## Physical Tiers

### 1. Client Tier
- **Desktop/Mobile Browser**: End-user access point using modern web browsers
- **Devices**: Desktop computers, tablets, and mobile devices
- **Communication**: HTTPS protocol for secure data transmission

### 2. Middle Tier  
- **.NET Core Application Server**: Hosts the application logic and web services
- **Components**: Blazor Server, Web API, SignalR for real-time updates
- **Infrastructure**: Load-balanced application servers for scalability

### 3. Data Tier
- **Database Servers**: SQL Server/Azure SQL Database clusters
- **Backup Systems**: Automated backup and disaster recovery
- **Data Integration**: Enterprise integration services

## Logical Layers and Components

### Client-Side Components
- **Rich HTML with Blazor Razor Components**: Modern single-page application interface
- **Bootstrap CSS Framework**: Responsive design and styling
- **JavaScript Interop**: Direct API communication for enhanced functionality
- **Font Awesome Icons**: Professional UI elements

### Application Server - Web Container (.NET Core)

#### User Interface Application (ADS.UI)
- **Blazor Server Pages**: 
  - Dashboard for Office Manager
  - Appointment scheduling interface
  - Patient portal for appointment management
  - Dentist interface for schedule viewing
- **Razor Components**: Reusable UI components
- **Authentication**: Identity framework integration
- **Outgoing Requests**:
  - HTTP/REST calls to ADS.API
  - Real-time updates via SignalR

#### Web Service Application (ADS.API)
- **Technology**: ASP.NET Core Web API with vertical Spring-equivalent layers:
  - **Controllers Layer**: RESTful Web API controllers (≈ Spring MVC)
  - **Service Layer**: Business logic and domain services (≈ Spring Service Layer)  
  - **Repository Layer**: Data access abstraction (≈ Spring DAO)
  - **Entity Framework Core**: ORM for database interactions (≈ Hibernate ORM)

### Service Layer Architecture

#### Spring-NET Cross-Pollination Layer (Equivalent to Spring DI):
- **Dependency Injection Container**: .NET Built-in DI Container
- **Service Registration**: Transient, Scoped, Singleton lifetimes
- **Configuration**: appsettings.json configuration management

#### RESTful Web Service Layer:
- **Technology**: ASP.NET Core Web API Controllers
- **Purpose**: Handle RESTful HTTP requests and responses
- **Features**: OpenAPI/Swagger documentation, model binding, validation

#### Domain Service Layer:
- **Technology**: C# POCO (Plain Old CLR Object) business services
- **Purpose**: Encapsulate business logic and domain rules
- **Features**: Appointment scheduling logic, billing validation, user management

#### Repository Pattern Layer:
- **Technology**: Entity Framework Core DbContext
- **Purpose**: Abstract and encapsulate all access to data sources
- **Features**: LINQ queries, change tracking, database migrations

#### Entity Framework Core ORM:
- **Technology**: Microsoft.EntityFrameworkCore
- **Purpose**: Object-Relational Mapping between C# objects and database tables
- **Features**: Code First approach, SQL generation, connection management

### Enterprise Information Services

#### Database Integration:
- **Primary Database**: SQL Server / Azure SQL Database
- **Features**: ACID transactions, clustering, automatic backups
- **Security**: Encryption at rest and in transit

#### Legacy System Integration:
- **ESB/API Gateway**: Azure API Management
- **Purpose**: Integration with existing dental practice management systems
- **Features**: Message transformation, routing, monitoring

#### External Services:
- **Email Service**: SendGrid/SMTP integration for appointment confirmations
- **Payment Gateway**: For billing and payment processing
- **Calendar Services**: Outlook/Google Calendar integration

## Technology Stack Mapping

### .NET Equivalents to Java EE Spring Architecture:

| Java EE Spring Component | .NET Equivalent | Purpose |
|--------------------------|-----------------|---------|
| Spring Boot Application | ASP.NET Core Web Application | Main application framework |
| Spring MVC Controllers | ASP.NET Core API Controllers | RESTful web service layer |
| Spring Service Layer | C# Domain Services | Business logic layer |
| Spring Data Repository | Entity Framework DbSet | Data access objects |
| Hibernate ORM | Entity Framework Core | Object-relational mapping |
| Spring Security | ASP.NET Core Identity | Authentication and authorization |
| Maven/Gradle Build | MSBuild/.NET CLI | Dependency management and build |
| Tomcat Server | Kestrel Web Server | HTTP server hosting |
| JPA Entities | EF Core Entity Classes | Domain models |

## Security Architecture

### Authentication & Authorization:
- **ASP.NET Core Identity**: User management and authentication
- **JWT Tokens**: Stateless authentication for API access
- **Role-based Authorization**: Office Manager, Dentist, Patient roles
- **Claims-based Security**: Granular permission management

### Data Protection:
- **Encryption**: AES-256 encryption for sensitive data
- **HTTPS Only**: All communication encrypted in transit
- **SQL Injection Prevention**: Parameterized queries via EF Core
- **XSS Protection**: Built-in ASP.NET Core protections

## Deployment Architecture

### Container Orchestration:
- **Docker Containers**: Application packaging and deployment
- **Kubernetes**: Container orchestration and scaling
- **Azure Container Instances**: Managed container hosting

### Cloud Services:
- **Azure App Service**: Hosted application platform
- **Azure SQL Database**: Managed database service
- **Azure Application Gateway**: Load balancing and SSL termination
- **Azure Key Vault**: Secrets management

This architecture provides a robust, scalable, and maintainable solution for the ADS Dental Surgery system while leveraging modern .NET technologies and enterprise best practices.
