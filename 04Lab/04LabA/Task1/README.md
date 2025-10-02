# Mohamed's eLibrary - ASP.NET Core Web Application

## Overview

This project implements the equivalent of a Spring Boot eLibrary application using .NET Core technologies. It demonstrates how Spring Boot concepts translate to ASP.NET Core development.

## Technology Stack

- **Backend**: ASP.NET Core 8.0 Web API
- **Database**: Entity Framework Core with In-Memory Database
- **ORM**: Entity Framework Core (equivalent to Hibernate)
- **Dependency Injection**: Built-in .NET DI Container
- **API Documentation**: Swagger/OpenAPI

## Project Structure

```
MohamedElibrary/
├── Controllers/
│   └── AuthorController.cs          # REST API Controllers (≈ @RestController)
├── Data/
│   └── MohamedElibraryDbContext.cs  # Database Context (≈ Spring Data Repository)
├── Models/
│   ├── Author.cs                   # Entity Models (≈ @Entity)
│   ├── Book.cs
│   ├── PublisherPublisher.cs
│   ├── PublisherName.cs
│   ├── PublishingCompany.cs
│   └── PublishingCompany_PublisherName.cs
├── wwwroot/
│   └── index.html                  # Frontend HTML page
├── Program.cs                      # Application Entry Point (≈ Application.java)
└── MohamedElibrary.csproj         # Project Configuration

```

## Key .NET Equivalents

| Snow Boot | .NET Core Equivalent | Purpose |
|-----------|---------------------|---------|
| `@RestController` | `Controller : ControllerBase` | REST API endpoints |
| `@Entity` | Class with `[Key]` attributes | Domain models |
| `@Repository` | `DbContext` | Data access layer |
| `@Service` | Service classes with DI | Business logic |
| `@Autowired` | Constructor Injection | Dependency injection |
| `data.sql` | `ModelBuilder.HasData()` | Database seeding |
| Spring Boot Starter | NuGet Packages | Dependency management |

## Running the Application

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio Code or Visual Studio

### Steps
1. **Navigate to project directory:**
   ```bash
   cd 04Lab/04LabA/Task1
   ```

2. **Restore packages:**
   ```bash
   dotnet restore
   ```

3. **Build the application:**
   ```bash
   dotnet build
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

5. **Open in browser:**
   - API Documentation: `https://localhost:7109/swagger`
   - Frontend UI: `https://localhost:7109/`
   - Sample API: `https://localhost:7109/api/author/1`

## API Endpoints

### Authors API
- `GET /api/author/{id}` - Get author's books
- `GET /api/author/{id}/Books` - Alternative endpoint

### Sample Data
The application includes seeded data with:
- **Authors**: Bui Lam Nam, Tran Dao Son, Ham Quang Thai, Tran Hoai Mai, Ngo Vinh Long
- **Books**: Effective Java, Core Java Volume I, Spring in Action
- **Publishers**: Various publishing companies

## Key Features Demonstrated

1. **Dependency Injection**: Constructor injection pattern for `AuthorController`
2. **Entity Framework Core**: ORM with relationships and data seeding
3. **RESTful APIs**: REST endpoints with proper HTTP methods
4. **Data Validation**: Model validation with data annotations
5. **Static Files**: Serving HTML frontend from `wwwroot` folder
6. **API Documentation**: Automatic Swagger/OpenAPI documentation

## Architecture Comparison

### Spring Boot Approach:
```java
@RestController
@RequestMapping("/api/author")
public class AuthorController {
    @Autowired
    private AuthorRepository authorRepository;
    
    @GetMapping("/{id}")
    public List<Book> getAuthorBooks(@PathVariable Long id) {
        return authorRepository.findBooksByAuthor(id);
    }
}
```

### ASP.NET Core Approach:
```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase {
    private readonly MohamedElibraryDbContext _context;
    
    public AuthorController(MohamedElibraryDbContext context) {
        _context = context; // Constructor injection
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> AuthorBookList(int id) {
        // Implementation using Entity Framework Core
    }
}
```

## Screenshots

See the `/screenshots` folder for sample screenshots of:
- Homepage with "Mohamed's eLibrary" banner
- API testing interface
- Swagger documentation

## Conclusion

This project successfully demonstrates how Spring Boot concepts translate to ASP.NET Core, showing that both frameworks provide similar capabilities for building robust, scalable web applications with modern development practices.
