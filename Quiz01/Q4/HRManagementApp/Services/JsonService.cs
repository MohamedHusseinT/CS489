using System.Text.Json;
using HRManagementApp.Models;
using HRManagementApp.DTOs;

namespace HRManagementApp.Services
{
    /// <summary>
    /// Service for JSON processing and formatting using DTOs
    /// </summary>
    public class JsonService
    {
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly MappingService _mappingService;
        
        public JsonService()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _mappingService = new MappingService();
        }
        
        /// <summary>
        /// Generate JSON output for departments list with employees and head of department
        /// Sorted by total annual salary in descending order
        /// </summary>
        public string GenerateDepartmentsJson(List<Department> departments)
        {
            var sortedDepartments = departments
                .OrderByDescending(d => d.TotalAnnualSalary)
                .ToList();
            
            var departmentDtos = _mappingService.MapToDepartmentDtos(sortedDepartments);
            return JsonSerializer.Serialize(departmentDtos, _jsonOptions);
        }
        
        /// <summary>
        /// Generate JSON output for employees list with department data and years of employment
        /// Sorted by years of employment (descending) and last name (ascending)
        /// </summary>
        public string GenerateEmployeesJson(List<Employee> employees)
        {
            var sortedEmployees = employees
                .OrderByDescending(e => e.YearsOfEmployment)
                .ThenBy(e => e.LastName)
                .ToList();
            
            var employeeDtos = _mappingService.MapToEmployeeDtos(sortedEmployees);
            return JsonSerializer.Serialize(employeeDtos, _jsonOptions);
        }
        
        /// <summary>
        /// Print formatted JSON output to console
        /// </summary>
        public void PrintJsonOutput(string jsonOutput, string title)
        {
            Console.WriteLine($"\n{new string('=', 60)}");
            Console.WriteLine($" {title}");
            Console.WriteLine($"{new string('=', 60)}");
            Console.WriteLine(jsonOutput);
            Console.WriteLine($"{new string('=', 60)}\n");
        }
    }
}
