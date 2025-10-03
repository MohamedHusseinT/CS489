using HRManagementApp.Models;
using HRManagementApp.DTOs;

namespace HRManagementApp.Services
{
    /// <summary>
    /// Service for mapping entities to DTOs to avoid circular references
    /// </summary>
    public class MappingService
    {
        /// <summary>
        /// Convert Department entity to DepartmentDto for JSON output
        /// </summary>
        public DepartmentDto MapToDepartmentDto(Department department)
        {
            return new DepartmentDto
            {
                DepartmentNo = department.DepartmentNo,
                Name = department.Name,
                Employees = department.Employees.Select(MapToEmployeeSummaryDto).ToList(),
                HeadOfDepartment = department.HeadOfDepartment != null ? MapToEmployeeSummaryDto(department.HeadOfDepartment) : null,
                TotalAnnualSalary = department.TotalAnnualSalary
            };
        }
        
        /// <summary>
        /// Convert Employee entity to EmployeeSummaryDto (no department reference)
        /// </summary>
        public EmployeeSummaryDto MapToEmployeeSummaryDto(Employee employee)
        {
            return new EmployeeSummaryDto
            {
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DateOfEmployment = employee.DateOfEmployment,
                BiweeklySalary = employee.BiweeklySalary,
                YearsOfEmployment = employee.YearsOfEmployment,
                AnnualSalary = employee.AnnualSalary
            };
        }
        
        /// <summary>
        /// Convert Employee entity to EmployeeDto for JSON output
        /// </summary>
        public EmployeeDto MapToEmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DateOfEmployment = employee.DateOfEmployment,
                BiweeklySalary = employee.BiweeklySalary,
                YearsOfEmployment = employee.YearsOfEmployment,
                AnnualSalary = employee.AnnualSalary,
                Department = employee.Department != null ? MapToDepartmentSummaryDto(employee.Department) : new DepartmentSummaryDto()
            };
        }
        
        /// <summary>
        /// Convert Department entity to DepartmentSummaryDto (no employees reference)
        /// </summary>
        public DepartmentSummaryDto MapToDepartmentSummaryDto(Department department)
        {
            return new DepartmentSummaryDto
            {
                DepartmentNo = department.DepartmentNo,
                Name = department.Name
            };
        }
        
        /// <summary>
        /// Convert list of departments to department DTOs
        /// </summary>
        public List<DepartmentDto> MapToDepartmentDtos(List<Department> departments)
        {
            return departments.Select(MapToDepartmentDto).ToList();
        }
        
        /// <summary>
        /// Convert list of employees to employee DTOs
        /// </summary>
        public List<EmployeeDto> MapToEmployeeDtos(List<Employee> employees)
        {
            return employees.Select(MapToEmployeeDto).ToList();
        }
    }
}
