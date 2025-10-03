using System.Text.Json.Serialization;

namespace HRManagementApp.DTOs
{
    /// <summary>
    /// Data Transfer Object for Employee listing with department information
    /// </summary>
    public class EmployeeDto
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
        
        [JsonPropertyName("department")]
        public DepartmentSummaryDto Department { get; set; } = new DepartmentSummaryDto();
    }
    
    /// <summary>
    /// Summary DTO for Department data within Employee context (no employees reference)
    /// </summary>
    public class DepartmentSummaryDto
    {
        [JsonPropertyName("departmentNo")]
        public string DepartmentNo { get; set; } = string.Empty;
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
