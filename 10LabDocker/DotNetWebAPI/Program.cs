using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeriesWebAPI.Data;
using ADSDentalSurgeriesWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<ADSDbContext>(options =>
    options.UseSqlite("Data Source=ads_dental_surgeries_webapi.db"));

// Add services
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<AddressService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "ADS Dental Surgeries Web API", 
        Version = "v1",
        Description = "RESTful Web API for ADS Dental Surgeries Appointment Management System"
    });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger in all environments for Docker container
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ADS Dental Surgeries Web API v1");
    c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
});

// Disable HTTPS redirection in Docker environment
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ADSDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
