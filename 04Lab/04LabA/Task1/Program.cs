using Microsoft.EntityFrameworkCore;
using MohamedElibrary.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MohamedElibraryDbContext>(options =>
    options.UseInMemoryDatabase("Author_Publisher"));
builder.Services.AddScoped<MohamedElibraryDbContext>();

// Configure JSON serialization to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

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