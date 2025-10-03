using HRManagementApp.Services;

namespace HRManagementApp
{
    /// <summary>
    /// Allied Building Contractors HR Management CLI Application
    /// Quiz01/Q4 - CS489 Applied Software Development
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize and run the interactive menu system
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