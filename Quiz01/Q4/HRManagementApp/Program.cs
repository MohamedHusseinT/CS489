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
            Console.WriteLine("🏢 Allied Building Contractors HR Management System");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("CS489 Quiz01/Q4 - CLI Application");
            Console.WriteLine("Student: Mohamed");
            Console.WriteLine(new string('=', 60));
            
            // Initialize services
            var dataService = new DataService();
            var jsonService = new JsonService();
            
            try
            {
                // Feature 1: Print departments list in JSON format
                Console.WriteLine("\n📊 Loading Departments Data...");
                var departments = dataService.GetAllDepartments();
                var departmentsJson = jsonService.GenerateDepartmentsJson(departments);
                jsonService.PrintJsonOutput(departmentsJson, "DEPARTMENTS LIST (Sorted by Total Annual Salary - Descending)");
                
                // Feature 2: Print employees list in JSON format
                Console.WriteLine("\n👥 Loading Employees Data...");
                var employees = dataService.GetAllEmployees();
                var employeesJson = jsonService.GenerateEmployeesJson(employees);
                jsonService.PrintJsonOutput(employeesJson, "EMPLOYEES LIST (Sorted by Years of Employment - Descending, Last Name - Ascending)");
                
                // Display summary statistics
                DisplaySummaryStatistics(departments, employees);
                
                Console.WriteLine("\n✅ HR Management System execution completed successfully!");
                Console.WriteLine("📸 Please take screenshots of the output for submission.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Display summary statistics about the HR data
        /// </summary>
        private static void DisplaySummaryStatistics(List<HRManagementApp.Models.Department> departments, List<HRManagementApp.Models.Employee> employees)
        {
            Console.WriteLine($"\n📈 SUMMARY STATISTICS");
            Console.WriteLine($"{new string('=', 40)}");
            Console.WriteLine($"Total Departments: {departments.Count}");
            Console.WriteLine($"Total Employees: {employees.Count}");
            Console.WriteLine($"Total Annual Salary Budget: ${departments.Sum(d => d.TotalAnnualSalary):N2}");
            Console.WriteLine($"Average Employee Salary: ${employees.Average(e => e.GetAnnualSalary()):N2}");
            Console.WriteLine($"Departments with Heads: {departments.Count(d => d.HeadOfDepartment != null)}");
            Console.WriteLine($"Departments without Heads: {departments.Count(d => d.HeadOfDepartment == null)}");
        }
    }
}