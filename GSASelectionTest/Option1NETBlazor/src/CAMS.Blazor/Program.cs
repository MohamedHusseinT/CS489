using Microsoft.EntityFrameworkCore;
using CAMS.Shared.Data;
using CAMS.Blazor.Services;
using CAMS.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// SignalR is automatically configured by AddInteractiveServerComponents()

    // Add Entity Framework
    builder.Services.AddDbContext<CamsDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=cams.db"));

// Add services
builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();

// Add controllers for API endpoints
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Map API controllers
app.MapControllers();

// Serve additional static files for Blazor
app.UseDefaultFiles();

app.UseAntiforgery();

// Map Blazor Hub for SignalR BEFORE mapping components
app.MapBlazorHub();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CamsDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
