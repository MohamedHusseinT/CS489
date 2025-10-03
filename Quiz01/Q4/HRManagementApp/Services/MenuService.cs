using HRManagementApp.Models;
using HRManagementApp.DTOs;

namespace HRManagementApp.Services
{
    /// <summary>
    /// Service for managing interactive CLI menu for HR staff and directors
    /// </summary>
    public class MenuService
    {
        private readonly DataService _dataService;
        private readonly JsonService _jsonService;
        private readonly MappingService _mappingService;
        
        public MenuService()
        {
            _dataService = new DataService();
            _jsonService = new JsonService();
            _mappingService = new MappingService();
        }
        
        /// <summary>
        /// Main menu loop for HR Management System
        /// </summary>
        public void RunMainMenu()
        {
            bool continueRunning = true;
            
            while (continueRunning)
            {
                DisplayMainMenu();
                var choice = GetUserChoice();
                
                switch (choice)
                {
                    case 1:
                        ShowDepartmentsWithEmployees();
                        break;
                    case 2:
                        ShowEmployeesWithDepartments();
                        break;
                    case 3:
                        ShowDepartmentHeadsOnly();
                        break;
                    case 4:
                        ShowTotalSalaryBudget();
                        break;
                    case 5:
                        ShowSummaryStatistics();
                        break;
                    case 6:
                        Console.WriteLine("\n👋 Thank you for using Allied Building Contractors HR Management System!");
                        continueRunning = false;
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice. Please select 1-6.");
                        break;
                }
                
                if (continueRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        
        /// <summary>
        /// Display the main menu options
        /// </summary>
        private void DisplayMainMenu()
        {
            Console.WriteLine("🏢 Allied Building Contractors HR Management System");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("CS489 Quiz01/Q4 - CLI Application");
            Console.WriteLine("Student: Mohamed");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine();
            Console.WriteLine("1. 📊 View All Departments (with employees and heads)");
            Console.WriteLine("2. 👥 View All Employees (with department info)");
            Console.WriteLine("3. 👑 View Department Heads Only");
            Console.WriteLine("4. 💰 View Total Annual Salary Budget");
            Console.WriteLine("5. 📈 View Summary Statistics");
            Console.WriteLine("6. ❌ Exit");
            Console.WriteLine();
            Console.Write("Enter your choice (1-6): ");
        }
        
        /// <summary>
        /// Get user input and validate choice
        /// </summary>
        private int GetUserChoice()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 6)
                {
                    return choice;
                }
                Console.Write("❌ Invalid input. Please enter a number between 1-6: ");
            }
        }
        
        /// <summary>
        /// Option 1: Show departments with employees and heads (HR Staff need)
        /// </summary>
        private void ShowDepartmentsWithEmployees()
        {
            Console.WriteLine("\n📊 DEPARTMENTS WITH EMPLOYEES AND HEADS");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("This view shows all departments with their employees and department heads.");
            Console.WriteLine("Sorted by total annual salary (descending).");
            Console.WriteLine();
            
            var departments = _dataService.GetAllDepartments();
            var jsonOutput = _jsonService.GenerateDepartmentsJson(departments);
            _jsonService.PrintJsonOutput(jsonOutput, "DEPARTMENTS LIST (Sorted by Total Annual Salary - Descending)");
        }
        
        /// <summary>
        /// Option 2: Show employees with departments (HR Staff need)
        /// </summary>
        private void ShowEmployeesWithDepartments()
        {
            Console.WriteLine("\n👥 EMPLOYEES WITH DEPARTMENT INFORMATION");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("This view shows all employees with their department information.");
            Console.WriteLine("Sorted by years of employment (descending) and last name (ascending).");
            Console.WriteLine();
            
            var employees = _dataService.GetAllEmployees();
            var jsonOutput = _jsonService.GenerateEmployeesJson(employees);
            _jsonService.PrintJsonOutput(jsonOutput, "EMPLOYEES LIST (Sorted by Years of Employment - Descending, Last Name - Ascending)");
        }
        
        /// <summary>
        /// Option 3: Show department heads only (Directors need)
        /// </summary>
        private void ShowDepartmentHeadsOnly()
        {
            Console.WriteLine("\n👑 DEPARTMENT HEADS ONLY");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("This view shows only the heads of each department for company directors.");
            Console.WriteLine();
            
            var departments = _dataService.GetAllDepartments();
            var headsOnly = departments.Where(d => d.HeadOfDepartment != null).ToList();
            
            if (headsOnly.Count == 0)
            {
                Console.WriteLine("❌ No department heads found.");
                return;
            }
            
            var headsDtos = headsOnly.Select(d => new
            {
                DepartmentName = d.Name,
                DepartmentNo = d.DepartmentNo,
                HeadOfDepartment = _mappingService.MapToEmployeeSummaryDto(d.HeadOfDepartment!)
            }).ToList();
            
            var jsonOutput = System.Text.Json.JsonSerializer.Serialize(headsDtos, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });
            
            _jsonService.PrintJsonOutput(jsonOutput, "DEPARTMENT HEADS LIST");
        }
        
        /// <summary>
        /// Option 4: Show total annual salary budget (All users need)
        /// </summary>
        private void ShowTotalSalaryBudget()
        {
            Console.WriteLine("\n💰 TOTAL ANNUAL SALARY BUDGET");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("This view shows the total amount paid in salaries for the entire workforce.");
            Console.WriteLine();
            
            var departments = _dataService.GetAllDepartments();
            var totalBudget = departments.Sum(d => d.TotalAnnualSalary);
            var employeeCount = departments.Sum(d => d.Employees.Count);
            var averageSalary = employeeCount > 0 ? totalBudget / employeeCount : 0;
            
            var budgetInfo = new
            {
                TotalAnnualSalaryBudget = totalBudget,
                TotalEmployees = employeeCount,
                AverageEmployeeSalary = averageSalary,
                Currency = "USD",
                BudgetBreakdown = departments.Select(d => new
                {
                    DepartmentName = d.Name,
                    DepartmentNo = d.DepartmentNo,
                    EmployeeCount = d.Employees.Count,
                    DepartmentAnnualSalary = d.TotalAnnualSalary,
                    PercentageOfTotal = totalBudget > 0 ? (d.TotalAnnualSalary / totalBudget) * 100 : 0
                }).OrderByDescending(d => d.DepartmentAnnualSalary).ToList()
            };
            
            var jsonOutput = System.Text.Json.JsonSerializer.Serialize(budgetInfo, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });
            
            _jsonService.PrintJsonOutput(jsonOutput, "TOTAL ANNUAL SALARY BUDGET REPORT");
        }
        
        /// <summary>
        /// Option 5: Show summary statistics
        /// </summary>
        private void ShowSummaryStatistics()
        {
            Console.WriteLine("\n📈 SUMMARY STATISTICS");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("This view shows comprehensive statistics about the HR data.");
            Console.WriteLine();
            
            var departments = _dataService.GetAllDepartments();
            var employees = _dataService.GetAllEmployees();
            
            var statistics = new
            {
                TotalDepartments = departments.Count,
                TotalEmployees = employees.Count,
                TotalAnnualSalaryBudget = departments.Sum(d => d.TotalAnnualSalary),
                AverageEmployeeSalary = employees.Count > 0 ? employees.Average(e => e.GetAnnualSalary()) : 0,
                DepartmentsWithHeads = departments.Count(d => d.HeadOfDepartment != null),
                DepartmentsWithoutHeads = departments.Count(d => d.HeadOfDepartment == null),
                AverageYearsOfEmployment = employees.Count > 0 ? employees.Average(e => e.YearsOfEmployment) : 0,
                DepartmentBreakdown = departments.Select(d => new
                {
                    DepartmentName = d.Name,
                    EmployeeCount = d.Employees.Count,
                    HasHead = d.HeadOfDepartment != null,
                    HeadName = d.HeadOfDepartment != null ? $"{d.HeadOfDepartment.FirstName} {d.HeadOfDepartment.LastName}" : "No Head Assigned"
                }).ToList()
            };
            
            var jsonOutput = System.Text.Json.JsonSerializer.Serialize(statistics, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });
            
            _jsonService.PrintJsonOutput(jsonOutput, "HR MANAGEMENT SYSTEM STATISTICS");
        }
    }
}
