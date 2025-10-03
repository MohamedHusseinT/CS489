using System.Text.Json.Serialization;

namespace HRManagementApp.Models
{
    /// <summary>
    /// Employee entity representing an employee in Allied Building Contractors
    /// </summary>
    public class Employee
    {
        public string EmployeeNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public decimal BiweeklySalary { get; set; }
        public string DepartmentNo { get; set; } = string.Empty;
        
        // Navigation property
        [JsonIgnore]
        public Department? Department { get; set; }
        
        // Computed properties for JSON output
        [JsonPropertyName("yearsOfEmployment")]
        public int YearsOfEmployment => GetYearsOfEmployment();
        
        [JsonPropertyName("annualSalary")]
        public decimal AnnualSalary => GetAnnualSalary();
        
        [JsonPropertyName("department")]
        public Department? EmployeeDepartment => Department;
        
        /// <summary>
        /// Calculate years of employment from date of employment to current date
        /// </summary>
        public int GetYearsOfEmployment()
        {
            var today = DateTime.Today;
            var years = today.Year - DateOfEmployment.Year;
            
            // Adjust if birthday hasn't occurred this year
            if (DateOfEmployment.Date > today.AddYears(-years))
                years--;
                
            return Math.Max(0, years);
        }
        
        /// <summary>
        /// Calculate annual salary from biweekly salary
        /// </summary>
        public decimal GetAnnualSalary()
        {
            // 26 biweekly periods in a year
            return BiweeklySalary * 26;
        }
    }
}
