using RoyalWindsorRealty.Services;

namespace RoyalWindsorRealty
{
    /// <summary>
    /// Royal Windsor Realty - Apartments Leasing Management System
    /// MidTerm/Q7 - CS489 Applied Software Development
    /// Student: Mohamed
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("🏢 Royal Windsor Realty - Apartments Leasing Management System");
                Console.WriteLine("MidTerm/Q7 - CS489 Applied Software Development");
                Console.WriteLine("Student: Mohamed");
                Console.WriteLine(new string('=', 70));
                
                // Initialize database
                var databaseService = new DatabaseService();
                databaseService.InitializeDatabase();
                
                Console.WriteLine("\n✅ Database initialized successfully!");
                Console.WriteLine("🚀 Starting interactive menu system...\n");
                
                // Run interactive menu
                var menuService = new MenuService();
                menuService.RunMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
