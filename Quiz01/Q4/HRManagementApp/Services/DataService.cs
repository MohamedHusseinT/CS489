using HRManagementApp.Models;

namespace HRManagementApp.Services
{
    /// <summary>
    /// Service for managing HR data in memory
    /// </summary>
    public class DataService
    {
        private readonly List<Department> _departments;
        private readonly List<Employee> _employees;
        
        public DataService()
        {
            _departments = new List<Department>();
            _employees = new List<Employee>();
            LoadSampleData();
        }
        
        /// <summary>
        /// Load the provided sample data into memory
        /// </summary>
        private void LoadSampleData()
        {
            // Load Departments data
            var salesDept = new Department 
            { 
                DepartmentNo = "31288741190182539912", 
                Name = "Sales" 
            };
            
            var marketingDept = new Department 
            { 
                DepartmentNo = "29274582650152771644", 
                Name = "Marketing" 
            };
            
            var engineeringDept = new Department 
            { 
                DepartmentNo = "29274599650152771609", 
                Name = "Engineering" 
            };
            
            _departments.AddRange(new[] { salesDept, marketingDept, engineeringDept });
            
            // Load Employees data
            var michaelPhilips = new Employee
            {
                EmployeeNo = "000-11-1234",
                FirstName = "Michael",
                LastName = "Philips",
                DateOfBirth = new DateTime(1995, 12, 31),
                DateOfEmployment = new DateTime(2021, 10, 15),
                BiweeklySalary = 2750.50m,
                DepartmentNo = engineeringDept.DepartmentNo,
                Department = engineeringDept
            };
            
            var annaSmith = new Employee
            {
                EmployeeNo = "000-61-0987",
                FirstName = "Anna",
                LastName = "Smith",
                DateOfBirth = new DateTime(1981, 9, 17),
                DateOfEmployment = new DateTime(2022, 8, 21),
                BiweeklySalary = 2500.00m,
                DepartmentNo = marketingDept.DepartmentNo,
                Department = marketingDept
            };
            
            var johnHammonds = new Employee
            {
                EmployeeNo = "000-99-1766",
                FirstName = "John",
                LastName = "Hammonds",
                DateOfBirth = new DateTime(2001, 7, 15),
                DateOfEmployment = new DateTime(2025, 1, 23),
                BiweeklySalary = 1560.75m,
                DepartmentNo = salesDept.DepartmentNo,
                Department = salesDept
            };
            
            var barbaraJones = new Employee
            {
                EmployeeNo = "000-41-1768",
                FirstName = "Barbara",
                LastName = "Jones",
                DateOfBirth = new DateTime(2000, 11, 18),
                DateOfEmployment = new DateTime(2025, 3, 13),
                BiweeklySalary = 1650.55m,
                DepartmentNo = marketingDept.DepartmentNo,
                Department = marketingDept
            };
            
            _employees.AddRange(new[] { michaelPhilips, annaSmith, johnHammonds, barbaraJones });
            
            // Set up relationships
            foreach (var employee in _employees)
            {
                var department = _departments.First(d => d.DepartmentNo == employee.DepartmentNo);
                department.Employees.Add(employee);
            }
            
            // Set Head of Department relationships based on provided data
            // Sales: Head is Anna Smith (000-61-0987) - but Anna is in Marketing, so this might be an error in data
            // Let's assume Anna Smith is Head of Sales (cross-departmental role)
            salesDept.HeadOfDepartment = annaSmith;
            
            // Marketing: No head (null)
            // marketingDept.HeadOfDepartment = null; // Already null by default
            
            // Engineering: Head is Michael Philips (000-11-1234)
            engineeringDept.HeadOfDepartment = michaelPhilips;
        }
        
        /// <summary>
        /// Get all departments with their employees and head of department
        /// </summary>
        public List<Department> GetAllDepartments()
        {
            return _departments.ToList();
        }
        
        /// <summary>
        /// Get all employees with their department information
        /// </summary>
        public List<Employee> GetAllEmployees()
        {
            return _employees.ToList();
        }
        
        /// <summary>
        /// Get departments sorted by total annual salary (descending)
        /// </summary>
        public List<Department> GetDepartmentsSortedBySalary()
        {
            return _departments
                .OrderByDescending(d => d.TotalAnnualSalary)
                .ToList();
        }
        
        /// <summary>
        /// Get employees sorted by years of employment (descending) and last name (ascending)
        /// </summary>
        public List<Employee> GetEmployeesSortedByEmploymentAndName()
        {
            return _employees
                .OrderByDescending(e => e.YearsOfEmployment)
                .ThenBy(e => e.LastName)
                .ToList();
        }
    }
}
