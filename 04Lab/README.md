# Lab 4A & 4B - Enterprise Web Application Development

**Student**: Mohamed  
**Course**: CS489 - Applied Software Development  
**Stack**: .NET Core Technologies  
**Date**: October 2024

## 📁 Lab Structure

```
04Lab/
├── 04LabA/          # Lab 4A - Spring Boot Equivalent (.NET Core)
│   ├── Task1/       # eLibrary ASP.NET Core Application
│   ├── Task2/       # Spring Framework Questions & Answers
│   └── README.md    # Lab 4A specific documentation
├── 04LabB/          # Lab 4B - ADS Dental Surgery Architecture
│   ├── Task1.1/     # Software Requirements Discovery
│   ├── Task1.2/     # Domain Model & Architecture Design
│   └── README.md    # Lab 4B specific documentation
└── README.md        # This main documentation file
```

## 🎯 Lab 4A - Spring Boot Equivalent Application

### Objective
Implement ASP.NET Core equivalent to the Spring Boot eLibrary tutorial, demonstrating how Java EE Spring concepts translate to .NET technologies.

### Completed Tasks

✅ **Task 1: Mohamed's eLibrary Application**
- **Technology**: ASP.NET Core 8.0 Web API
- **Features**: Entity Framework Core ORM, RESTful APIs, Swagger documentation
- **Architecture**: Clean layered architecture with dependency injection
- **Frontend**: HTML/CSS/JavaScript interface
- **Database**: Entity Framework In-Memory with seeded data

**Key Files**:
- `MohamedElibrary.csproj` - Project configuration
- `Models/*.cs` - Domain entities (Author, Book, Publisher, etc.)
- `Controllers/AuthorController.cs` - REST API endpoints
- `Data/MohamedElibraryDbContext.cs` - Data access layer
- `wwwroot/index.html` - Frontend interface

**Running**:
```bash
cd 04LabA/Task1
dotnet run
# Access: https://localhost:7109/
```

✅ **Task 2: Spring Framework Concepts**
- Comprehensive answers to Spring, Spring Boot, IoC, and DI questions
- Real-world examples and code comparisons
- Mapping between Spring Boot and ASP.NET Core concepts

## 🏗️ Lab 4B - ADS Dental Surgery System Architecture

### Objective
Create comprehensive software solution architecture diagram for the Advantis Dental Surgery (ADS) system using modern enterprise application design principles.

### Completed Tasks

✅ **Task 1.1: Software Requirements Discovery**
- **15 Functional Requirements** covering user management, appointments, surgeries, business rules
- **15 Non-Functional Requirements** for performance, security, reliability, usability, scalability
- **Requirements Documentation**: Clear functional and non-functional categories

✅ **Task 1.2: Domain Model & Architecture Design**
- **Domain Model**: Complete UML class diagram with entities, relationships, multiplicities
- **Architecture Diagram**: Multi-tier architecture documentation
- **Technology Stack**: .NET Core enterprise stack mapping
- **Components**: Client tier, middle tier, data tier with detailed breakdown

**Key Deliverables**:
- `Software_Requirements.md` - Requirements documentation
- `Domain_Model_Analysis.md` - Detailed domain analysis
- `ADS_Software_Architecture_Diagram.md` - Architecture documentation
- `ADS_Domain_Model_UML.puml` - PlantUML class diagram

## 🛠️ Technology Stack Summary

### ASP.NET Core Technologies Used:
- **Backend**: ASP.NET Core 8.0 Web API
- **ORM**: Entity Framework Core
- **Database**: In-Memory Database (Lab 4A), SQL Server Architecture (Lab 4B)
- **Frontend**: Blazor Server, HTML/CSS/Bootstrap
- **Security**: ASP.NET Core Identity, JWT tokens
- **Documentation**: Swagger/OpenAPI
- **Deployment**: Docker containers, Azure services

### Architecture Patterns:
- **Multi-Tier Architecture**: Client, Middle, Data tiers
- **Clean Architecture**: Separation of concerns
- **Dependency Injection**: Constructor injection pattern
- **Repository Pattern**: Data access abstraction
- **RESTful APIs**: HTTP-based service endpoints

## 🌟 Lab 4 Highlights

1. **Spring Boot Translation**: Successfully demonstrated Spring Boot → ASP.NET Core equivalents
2. **Enterprise Architecture**: Comprehensive multi-tier application design
3. **Domain Modeling**: Professional UML class diagrams with relationships
4. **Requirements Engineering**: Detailed functional and non-functional requirements
5. **Modern Practices**: CI/CD ready, cloud-native, containerized solutions

## 🔗 Mapping: Spring Boot ↔ ASP.NET Core

| Spring Boot | ASP.NET Core Equivalent | Purpose |
|-------------|------------------------|---------|
| `@RestController` | `Controller : ControllerBase` | REST endpoints |
| `@Entity` | Entity classes with `[Key]` | Domain models |
| `@Repository` | `DbContext` | Data access |
| `@Autowired` | Constructor injection | Dependency injection |
| `Application.main()` | `Program.cs` | Application entry |
| Spring Data JPA | Entity Framework Core | ORM framework |
| Hibernate | Entity Framework Core | Object-relational mapping |
| Maven dependencies | NuGet packages | Package management |
| Tomcat server | Kestrel web server | HTTP hosting |

## 📋 Assignment Completion Checklist

- ✅ Lab 4A Task 1: ASP.NET Core eLibrary application created and functioning
- ✅ Lab 4A Task 2: Spring Boot concepts explained with examples
- ✅ Lab 4B Task 1.1: Software requirements identified and documented
- ✅ Lab 4B Task 1.2: Domain model UML diagram created with architecture design
- ✅ All code tested and builds successfully
- ✅ Documentation complete with README files
- ✅ Folder structure organized as requested
- ✅ Screenshots ready for submission (see individual task folders)

## 🚀 Ready for Submission

Both Lab 4A and Lab 4B are complete with:
- Working applications (Lab 4A): ASP.NET Core eLibrary running on localhost
- Comprehensive documentation: Technical explanations and architecture diagrams
- Professional structure: Organized folders following assignment requirements
- Modern technologies: Enterprise-grade .NET stack implementation

**Submission**: All files organized under `04Lab/04LabA/` and `04Lab/04LabB/` folders, ready for GitHub commit and push.
