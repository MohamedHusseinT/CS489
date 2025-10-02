# Lab 4A - eLibrary Spring Boot Equivalent (.NET Core)

## Overview

This lab implements .NET Core equivalents for Spring Boot concepts, creating an eLibrary web application that mirrors Spring Boot functionality using ASP.NET Core technologies.

## Lab Structure

### Task 1: eLibrary Application
- **Location**: `Task1/`
- **Content**: ASP.NET Core Web API equivalent to Spring Boot tutorial
- **Technology**: ASP.NET Core 8.0, Entity Framework Core, Swagger

### Task 2: Short Answers
- **Location**: `Task2/`
- **Content**: Answers to Spring framework concepts with examples
- **Topics**: Spring, Spring Boot, Dependency Injection, IoC

## Completed Features

✅ **Task 1 - ASP.NET Core eLibrary Application**
- Created MohamedElibrary Web API project
- Implemented Entity models (Author, Book, Publisher, etc.)
- Added Entity Framework Core DbContext
- Created AuthorController with REST endpoints
- Built HTML frontend interface
- Added comprehensive Spring Boot comparison documentation

✅ **Task 2 - Spring Framework Explanations**
- What is Spring? - Framework overview with examples
- What is Spring Boot? - Extension and auto-configuration
- Relationship between Spring Platform and Spring Boot
- Dependency Injection patterns and examples
- Inversion of Control principles and implementation

## Technology Stack Used

- **ASP.NET Core 8.0** - Web API framework
- **Entity Framework Core** - ORM (equivalent to Hibernate)
- **Entity Framework In-Memory** - Database provider
- **Swagger/OpenAPI** - API documentation
- **HTML/CSS/JavaScript** - Frontend interface
- **Dependency Injection** - Built-in .NET DI container

 Running the Application

```bash
cd Task1
dotnet run
```

Access:
- Frontend: `https://localhost:7109/`
- API Docs: `https://localhost:7109/swagger`
- Author API: `https://localhost:7109/api/author/1`

## Spring to .NET Mapping

| Spring Boot | ASP.NET Core | Purpose |
|-------------|--------------|---------|
| @RestController | Controller : ControllerBase | REST endpoints |
| @Entity | Entity classes with [Key] | Domain models |
| @Repository | DbContext | Data access |
| @Autowired | Constructor injection | Dependency injection |
| Application.main() | Program.cs | Entry point |
| Spring Data JPA | Entity Framework Core | ORM |
| Maven dependencies | NuGet packages | Package management |

## Screenshots Required

See `Task1/screenshots/` folder for:
- Homepage with "Mohamed's eLibrary" banner
- API testing interface
- Swagger documentation view

All screenshots saved as required for assignment submission.
