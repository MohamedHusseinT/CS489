using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ADSDentalSurgeries.Data;
using ADSDentalSurgeries.Services;

namespace ADSDentalSurgeries
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("🏥 ADS Dental Surgeries - Appointment Management System");
                Console.WriteLine("Lab 6 - CS489 Applied Software Development");
                Console.WriteLine("Student: Mohamed");
                Console.WriteLine(new string('=', 70));
                Console.WriteLine();

                // Setup dependency injection
                var services = new ServiceCollection();
                ConfigureServices(services);
                var serviceProvider = services.BuildServiceProvider();

                // Initialize database
                await InitializeDatabaseAsync(serviceProvider);

                // Run the application
                var menuService = serviceProvider.GetRequiredService<MenuService>();
                await menuService.RunMainMenuAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework
            services.AddDbContext<ADSDbContext>(options =>
                options.UseSqlite("Data Source=ads_dental_surgeries.db"));

            // Add services
            services.AddScoped<PatientService>();
            services.AddScoped<DentistService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<SurgeryService>();
            services.AddScoped<MenuService>();
        }

        private static async Task InitializeDatabaseAsync(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ADSDbContext>();

            Console.WriteLine("🔧 Initializing database...");
            
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            
            Console.WriteLine("✅ Database initialized successfully!");
            Console.WriteLine("📁 Database location: ads_dental_surgeries.db");
            Console.WriteLine();

            // Display sample data summary
            var patientCount = await context.Patients.CountAsync();
            var dentistCount = await context.Dentists.CountAsync();
            var surgeryCount = await context.Surgeries.CountAsync();
            var appointmentCount = await context.Appointments.CountAsync();

            Console.WriteLine("📊 Sample Data Summary:");
            Console.WriteLine($"   - {patientCount} Patients");
            Console.WriteLine($"   - {dentistCount} Dentists");
            Console.WriteLine($"   - {surgeryCount} Surgeries");
            Console.WriteLine($"   - {appointmentCount} Appointments");
            Console.WriteLine();

            Console.WriteLine("🚀 Starting interactive menu system...");
            Console.WriteLine();
        }
    }
}
