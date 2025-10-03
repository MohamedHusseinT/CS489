using System.Text.Json.Serialization;

namespace HRManagementApp.Models
{
    /// <summary>
    /// Department entity representing a department in Allied Building Contractors
    /// </summary>
    public class Department
    {
        public string DepartmentNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        
        // Navigation properties
        [JsonIgnore]
        public List<Employee> Employees { get; set; } = new List<Employee>();
        
        [JsonIgnore]
        public Employee? HeadOfDepartment { get; set; }
        
        // Computed properties for JSON output
        [JsonPropertyName("totalAnnualSalary")]
        public decimal TotalAnnualSalary => Employees.Sum(e => e.GetAnnualSalary());
        
        [JsonPropertyName("employees")]
        public List<Employee> DepartmentEmployees => Employees.ToList();
        
        [JsonPropertyName("headOfDepartment")]
        public Employee? DepartmentHead => HeadOfDepartment;
    }
}
