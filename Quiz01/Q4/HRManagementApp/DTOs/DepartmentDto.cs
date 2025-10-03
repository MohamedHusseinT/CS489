using System.Text.Json.Serialization;

namespace HRManagementApp.DTOs
{
    /// <summary>
    /// Data Transfer Object for Department listing with employees and head of department
    /// </summary>
    public class DepartmentDto
    {
        [JsonPropertyName("departmentNo")]
        public string DepartmentNo { get; set; } = string.Empty;
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("employees")]
        public List<EmployeeSummaryDto> Employees { get; set; } = new List<EmployeeSummaryDto>();
        
        [JsonPropertyName("headOfDepartment")]
        public EmployeeSummaryDto? HeadOfDepartment { get; set; }
        
        [JsonPropertyName("totalAnnualSalary")]
        public decimal TotalAnnualSalary { get; set; }
    }
    
    /// <summary>
    /// Summary DTO for Employee data within Department context (no department reference)
    /// </summary>
    public class EmployeeSummaryDto
    {
        [JsonPropertyName("employeeNo")]
        public string EmployeeNo { get; set; } = string.Empty;
        
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;
        
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;
        
        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        
        [JsonPropertyName("dateOfEmployment")]
        public DateTime DateOfEmployment { get; set; }
        
        [JsonPropertyName("biweeklySalary")]
        public decimal BiweeklySalary { get; set; }
        
        [JsonPropertyName("yearsOfEmployment")]
        public int YearsOfEmployment { get; set; }
        
        [JsonPropertyName("annualSalary")]
        public decimal AnnualSalary { get; set; }
    }
}
