using System;

namespace EmployeePensionApp.Model
{
    public class Employee
    {
        // Private fields
        private long _employeeId;
        private string _firstName;
        private string _lastName;
        private DateTime _employmentDate;
        private decimal _yearlySalary;
        private PensionPlan? _pensionPlan;

        // Default constructor
        public Employee()
        {
            _employeeId = 0;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _employmentDate = DateTime.MinValue;
            _yearlySalary = 0.00m;
            _pensionPlan = null;
        }

        // Parameterized constructor
        public Employee(long employeeId, string firstName, string lastName, DateTime employmentDate, decimal yearlySalary)
        {
            _employeeId = employeeId;
            _firstName = firstName ?? string.Empty;
            _lastName = lastName ?? string.Empty;
            _employmentDate = employmentDate;
            _yearlySalary = yearlySalary;
            _pensionPlan = null;
        }

        // Copy constructor
        public Employee(Employee? other)
        {
            if (other != null)
            {
                _employeeId = other._employeeId;
                _firstName = other._firstName;
                _lastName = other._lastName;
                _employmentDate = other._employmentDate;
                _yearlySalary = other._yearlySalary;
                _pensionPlan = other._pensionPlan != null ? new PensionPlan(other._pensionPlan) : null;
            }
        }

        // Getter and Setter for EmployeeId
        public long EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        // Getter and Setter for FirstName
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value ?? string.Empty; }
        }

        // Getter and Setter for LastName
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value ?? string.Empty; }
        }

        // Getter and Setter for EmploymentDate
        public DateTime EmploymentDate
        {
            get { return _employmentDate; }
            set { _employmentDate = value; }
        }

        // Getter and Setter for YearlySalary
        public decimal YearlySalary
        {
            get { return _yearlySalary; }
            set { _yearlySalary = value; }
        }

        // Getter and Setter for PensionPlan
        public PensionPlan? PensionPlan
        {
            get { return _pensionPlan; }
            set { _pensionPlan = value; }
        }

        // Method to check if employee is eligible for pension enrollment
        public bool IsEligibleForPensionEnrollment(DateTime referenceDate)
        {
            var yearsOfEmployment = (referenceDate - _employmentDate).TotalDays / 365.25;
            return yearsOfEmployment >= 3.0;
        }

        // Method to check if employee is enrolled in pension plan
        public bool IsEnrolledInPensionPlan()
        {
            return _pensionPlan != null;
        }

        // Override ToString for debugging
        public override string ToString()
        {
            return $"Employee{{EmployeeId={_employeeId}, FirstName='{_firstName}', LastName='{_lastName}', EmploymentDate={_employmentDate:yyyy-MM-dd}, YearlySalary={_yearlySalary:C}, HasPensionPlan={_pensionPlan != null}}}";
        }
    }
}
