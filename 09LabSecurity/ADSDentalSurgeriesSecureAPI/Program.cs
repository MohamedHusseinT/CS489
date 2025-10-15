using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ADSDentalSurgeriesSecureAPI.Data;
using ADSDentalSurgeriesSecureAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Database
builder.Services.AddDbContext<ADSDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=ads_dental_surgeries_secure.db"));

// Register application services
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<AddressService>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };

    // Add JWT token to logs for debugging (remove in production)
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine($"Token validated for user: {context.Principal?.Identity?.Name}");
            return Task.CompletedTask;
        }
    };
});

// Configure Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("DentistOnly", policy => policy.RequireRole("DENTIST", "ADMIN"));
    options.AddPolicy("ReceptionistOnly", policy => policy.RequireRole("RECEPTIONIST", "ADMIN"));
    options.AddPolicy("AuthenticatedUser", policy => policy.RequireAuthenticatedUser());
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure Swagger/OpenAPI with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ADS Dental Surgeries Secure API",
        Version = "v1",
        Description = "RESTful Web API for ADS Dental Surgeries with JWT Authentication",
        Contact = new OpenApiContact
        {
            Name = "Mohamed Hussein",
            Email = "mohamed@ads.com"
        }
    });

    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ADSDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ADS Dental Surgeries Secure API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at app root
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Authentication & Authorization middleware (order is important!)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine("===================================================");
Console.WriteLine("ADS Dental Surgeries Secure API - Lab 9");
Console.WriteLine("===================================================");
Console.WriteLine("API URL: http://localhost:5000");
Console.WriteLine("Swagger UI: http://localhost:5000");
Console.WriteLine("===================================================");
Console.WriteLine("\n🔐 Default Users:");
Console.WriteLine("  1. Admin: username=admin, password=admin123");
Console.WriteLine("  2. Dentist (Tony): username=tony.smith, password=password123");
Console.WriteLine("  3. Dentist (Helen): username=helen.pearson, password=password123");
Console.WriteLine("  4. Receptionist: username=receptionist, password=password123");
Console.WriteLine("\n📝 Authentication Endpoints:");
Console.WriteLine("  POST /adsweb/api/v1/auth/login - Login");
Console.WriteLine("  POST /adsweb/api/v1/auth/register - Register");
Console.WriteLine("  GET  /adsweb/api/v1/auth/me - Get current user");
Console.WriteLine("\n🏥 Patient Endpoints (Authenticated):");
Console.WriteLine("  GET    /adsweb/api/v1/patients - List all patients");
Console.WriteLine("  GET    /adsweb/api/v1/patients/{id} - Get patient by ID");
Console.WriteLine("  POST   /adsweb/api/v1/patients - Create patient");
Console.WriteLine("  PUT    /adsweb/api/v1/patients/patient/{id} - Update patient");
Console.WriteLine("  DELETE /adsweb/api/v1/patients/patient/{id} - Delete patient");
Console.WriteLine("===================================================\n");

app.Run();
