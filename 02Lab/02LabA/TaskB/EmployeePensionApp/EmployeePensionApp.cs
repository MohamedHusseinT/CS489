using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeePensionApp.Model;

namespace EmployeePensionApp
{
    public class EmployeePensionApp
    {
        private static List<Employee> _employees;

        public static void Main(string[] args)
        {
            // Initialize data
            LoadEmployeeData();

            // Display menu
            DisplayMenu();

            // Process user input
            ProcessUserInput();
        }

        private static void LoadEmployeeData()
        {
            _employees = new List<Employee>
            {
                // Employee 1: Daniel Agar - Has pension plan
                new Employee(1, "Daniel", "Agar", new DateTime(2023, 1, 17), 105945.50m)
                {
                    PensionPlan = new PensionPlan("EX1089", new DateTime(2025, 9, 3), 100.00m)
                },

                // Employee 2: Bernard Shaw - No pension plan
                new Employee(2, "Bernard", "Shaw", new DateTime(2022, 9, 3), 197750.00m),

                // Employee 3: Carly Agar - Has pension plan
                new Employee(3, "Carly", "Agar", new DateTime(2014, 5, 16), 842000.75m)
                {
                    PensionPlan = new PensionPlan("SM2307", new DateTime(2017, 5, 17), 1555.50m)
                },

                // Employee 4: Wesley Schneider - No pension plan
                new Employee(4, "Wesley", "Schneider", new DateTime(2023, 7, 21), 74500.00m),

                // Employee 5: Anna Wiltord - No pension plan
                new Employee(5, "Anna", "Wiltord", new DateTime(2023, 3, 15), 85750.00m),

                // Employee 6: Yosef Tesfalem - No pension plan
                new Employee(6, "Yosef", "Tesfalem", new DateTime(2024, 10, 31), 100000.00m)
            };
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("=== Employee Pension Management System ===");
            Console.WriteLine("1. Print all employees in JSON format");
            Console.WriteLine("2. Print Quarterly Upcoming Enrollees report");
            Console.WriteLine("3. Exit");
            Console.WriteLine("==========================================");
        }

        private static void ProcessUserInput()
        {
            while (true)
            {
                Console.Write("Enter your choice (1-3): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PrintAllEmployeesJson();
                        break;
                    case "2":
                        PrintQuarterlyUpcomingEnrollees();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
                Console.WriteLine();
            }
        }

        // Feature 1: Print all employees in JSON format
        // Sorted by descending yearly salary, then ascending last name
        public static void PrintAllEmployeesJson()
        {
            Console.WriteLine("=== All Employees (JSON Format) ===");
            Console.WriteLine("Sorted by: Yearly Salary (Descending), Last Name (Ascending)");
            Console.WriteLine();

            var sortedEmployees = _employees
                .OrderByDescending(e => e.YearlySalary)
                .ThenBy(e => e.LastName)
                .ToList();

            Console.WriteLine(GenerateJsonOutput(sortedEmployees));
        }

        // Feature 2: Print Quarterly Upcoming Enrollees report
        // Employees who are eligible for pension enrollment in the next quarter
        public static void PrintQuarterlyUpcomingEnrollees()
        {
            Console.WriteLine("=== Quarterly Upcoming Enrollees Report (JSON Format) ===");
            Console.WriteLine("Employees eligible for pension enrollment in the next quarter");
            Console.WriteLine("Sorted by: Employment Date (Descending)");
            Console.WriteLine();

            // Calculate next quarter dates
            var currentDate = DateTime.Now;
            var nextQuarterStart = GetNextQuarterStart(currentDate);
            var nextQuarterEnd = GetNextQuarterEnd(nextQuarterStart);

            Console.WriteLine($"Next Quarter: {nextQuarterStart:yyyy-MM-dd} to {nextQuarterEnd:yyyy-MM-dd}");
            Console.WriteLine();

            var eligibleEmployees = _employees
                .Where(e => !e.IsEnrolledInPensionPlan()) // Not enrolled in pension plan
                .Where(e => e.IsEligibleForPensionEnrollment(nextQuarterEnd)) // Eligible for enrollment
                .OrderByDescending(e => e.EmploymentDate) // Sorted by employment date (descending)
                .ToList();

            if (eligibleEmployees.Any())
            {
                Console.WriteLine(GenerateJsonOutput(eligibleEmployees));
            }
            else
            {
                Console.WriteLine("No employees are eligible for pension enrollment in the next quarter.");
            }
        }

        private static DateTime GetNextQuarterStart(DateTime currentDate)
        {
            int currentQuarter = (currentDate.Month - 1) / 3 + 1;
            int nextQuarter = currentQuarter == 4 ? 1 : currentQuarter + 1;
            int nextYear = currentQuarter == 4 ? currentDate.Year + 1 : currentDate.Year;
            
            int startMonth = (nextQuarter - 1) * 3 + 1;
            return new DateTime(nextYear, startMonth, 1);
        }

        private static DateTime GetNextQuarterEnd(DateTime quarterStart)
        {
            return quarterStart.AddMonths(3).AddDays(-1);
        }

        private static string GenerateJsonOutput(List<Employee> employees)
        {
            var json = new StringBuilder();
            json.AppendLine("[");

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                json.AppendLine("  {");
                json.AppendLine($"    \"employeeId\": {employee.EmployeeId},");
                json.AppendLine($"    \"firstName\": \"{employee.FirstName}\",");
                json.AppendLine($"    \"lastName\": \"{employee.LastName}\",");
                json.AppendLine($"    \"employmentDate\": \"{employee.EmploymentDate:yyyy-MM-dd}\",");
                json.AppendLine($"    \"yearlySalary\": {employee.YearlySalary:F2},");
                
                if (employee.PensionPlan != null)
                {
                    json.AppendLine("    \"pensionPlan\": {");
                    json.AppendLine($"      \"planReferenceNumber\": \"{employee.PensionPlan.PlanReferenceNumber}\",");
                    json.AppendLine($"      \"enrollmentDate\": \"{employee.PensionPlan.EnrollmentDate:yyyy-MM-dd}\",");
                    json.AppendLine($"      \"monthlyContribution\": {employee.PensionPlan.MonthlyContribution:F2}");
                    json.AppendLine("    }");
                }
                else
                {
                    json.AppendLine("    \"pensionPlan\": null");
                }

                json.Append(i < employees.Count - 1 ? "  }," : "  }");
            }

            json.AppendLine();
            json.AppendLine("]");
            return json.ToString();
        }
    }
}
