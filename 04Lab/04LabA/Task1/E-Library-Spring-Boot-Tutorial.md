# eLibrary - ASP.NET Core Web Application Tutorial (Equivalent to Spring Boot)

## Overview
This tutorial covers creating an ASP.NET Core web application equivalent to the Spring Boot eLibrary application using .NET technologies.

## Prerequisites
- .NET 8.0 SDK
- Visual Studio Code or Visual Studio
- Basic knowledge of C# and web development

## Step-by-Step Guide

### Step 1: Create the ASP.NET Core Web API Project
```bash
dotnet new webapi -n MohamedElibrary
cd MohamedElibrary
```

### Step 2: Add Entity Framework Core (Equivalent to JPA/Hibernate)
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

### Step 3: Create Entity Models

**Author.cs**
```csharp
using System.ComponentModel.DataAnnotations;

public class Author
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
```

**PublishingCompany.cs**
```csharp
using System.ComponentModel.DataAnnotations;

public class PublishingCompany
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<PublishingCompany_PublisherName> PublishingCompany_PublisherNames { get; set; } = new List<PublishingCompany_PublisherName>();
}
```

**PublishingCompany_PublisherName.cs**
```csharp
using System.ComponentModel.DataAnnotations;

public class PublishingCompany_PublisherName
{
    [Key]
    public int Id { get; set; }
    public int PublishingCompanyId { get; set; }
    public virtual PublishingCompany PublishingCompany { get; set; } = null!;
    public int PublisherNameId {	get; set; }
    public virtual PublisherName PublisherName {	get; set; } = null!;
}
```

**PublisherName.cs**
```csharp
using System.ComponentModel.DataAnnotations;

public class PublisherName
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Role { get; set; } = string.Empty;
    public int PublisherId { get; set; }
    public virtual PublisherPublisher Publisher { get; set; } = null!;
    public virtual ICollection<PublishingCompany_PublisherName> PublishingCompany_PublisherNames { get; set; } = new List<PublishingCompany_PublisherName>();
}
```

**PublisherPublisher.cs**
```csharp
using System.ComponentModel.DataAnnotations;

public class PublisherPublisher
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<PublisherName> PublisherNames { get; set; } = new List<PublisherName>();
}
```

**Book.cs**
```csharp
using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Isbn { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    public DateTime DatePublished { get; set; }
    public int PublisherPublisherId { get; set; }
    public virtual PublisherPublisher PublisherPublisher { get; set; } = null!;
    public int AuthorId { get; set; }
    public virtual Author Author { get; set; } = null!;
    public int PublishingCompanyId { get; set; }
    public virtual PublishingCompany PublishingCompany { get; set; } = null!;
}
```

### Step 4: Create DbContext (Equivalent to Spring Data Repository)

**MohamedElibraryDbContext.cs**
```csharp
using Microsoft.EntityFrameworkCore;

public class MohamedElibraryDbContext : DbContext
{
    public MohamedElibraryDbContext(DbContextOptions<MohamedElibraryDbContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<PublisherName> PublisherNames { get; set; }
    public DbSet<PublisherPublisher> PublisherPublishers { get; set; }
    public DbSet<PublishingCompany> PublishingCompanies { get; set; }
    public DbSet<PublishingCompany_PublisherName> PublishingCompany_PublisherNames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Data equivalent to Spring Boot data.sql
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "Bui Lam Nam" },
            new Author { Id = 2, Name = "Tran Dao Son" },
            new Author { Id = 3, Name = "Ham Quang Thai" },
            new Author { Id = 4, Name = "Tran Hoai Mai" },
            new Author { Id = 5, Name = "Ngo Vinh Long" }
        );

        modelBuilder.Entity<PublisherPublisher>().HasData(
            new PublisherPublisher { Id = 1, Name = "Bui Lam Nam Dong Tac Gia" },
            new PublisherPublisher { Id = 2, Name = "Han Song" },
            new PublisherPublisher { Id = 3, Name = "Chieu Mai"},
            new PublisherPublisher { Id = 4, Name = "Tran Dao Vu Tong" },
            new PublisherPublisher { Id = 5, Name = "Phung Mai Xuyen"}
        );

        // Add other seed data...
    }
}
```

### Step 5: Register Services in Program.cs

**Program.cs**
```csharp
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MohamedElibraryDbContext>(options =>
    options.UseInMemoryDatabase("Author_Publisher"));
builder.Services.AddScoped<MohamedElibraryDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed data
SeedData(app);

app.Run();

void SeedData(IApplicationBuilder app)
{
    var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MohamedElibraryDbContext>();
    context.Database.EnsureCreated();
}
```

### Step 6: Create Controllers (Equivalent to Spring Boot RestControllers)

**AuthorController.cs**
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly MohamedElibraryDbContext _context;

    public AuthorController(MohamedElibraryDbContext context)
        => _context = context;

    [HttpGet("{id}")]
    public IActionResult AuthorBookList(int id)
    {
        if (_context.Authors.Any(x => x.Id == id))
            return NotFound();
        return AuthorBookList(id);
    }

    [HttpGet("{id}/Books")]
    public async Task<ActionResult<List<Book>>> AuthorBookList(long id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
            return NotFound();

        return author.Books.ToList();
    }
}
```

### Step 7: Run the Application
```bash
dotnet run
```

The application will be available at `https://localhost:7109`

## Key .NET Equivalents to Spring Boot Concepts:

| Spring Boot | .NET Equivalent | Purpose |
|-------------|------------------|---------|
| Spring Container | .NET Dependency Injection | Dependency Management |
| @RestController | Controller : ControllerBase | REST API Endpoints |
| @Repository | DbContext/Entity Framework | Data Access Layer |
| @Entity | Entity Classes with Data Annotations | Domain Models |
| data.sql | ModelBuilder.HasData() | Database Seeding |
| Spring MVC | ASP.NET Core MVC | Web Framework |

This ASP.NET Core implementation provides the same functionality as the Spring Boot tutorial but using .NET technologies and conventions.
