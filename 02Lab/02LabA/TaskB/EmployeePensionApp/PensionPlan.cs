using System;

namespace EmployeePensionApp.Model
{
    public class PensionPlan
    {
        // Private fields
        private string _planReferenceNumber;
        private DateTime _enrollmentDate;
        private decimal _monthlyContribution;

        // Default constructor
        public PensionPlan()
        {
            _planReferenceNumber = string.Empty;
            _enrollmentDate = DateTime.MinValue;
            _monthlyContribution = 0.00m;
        }

        // Parameterized constructor
        public PensionPlan(string planReferenceNumber, DateTime enrollmentDate, decimal monthlyContribution)
        {
            _planReferenceNumber = planReferenceNumber ?? string.Empty;
            _enrollmentDate = enrollmentDate;
            _monthlyContribution = monthlyContribution;
        }

        // Copy constructor
        public PensionPlan(PensionPlan other)
        {
            if (other != null)
            {
                _planReferenceNumber = other._planReferenceNumber;
                _enrollmentDate = other._enrollmentDate;
                _monthlyContribution = other._monthlyContribution;
            }
        }

        // Getter and Setter for PlanReferenceNumber
        public string PlanReferenceNumber
        {
            get { return _planReferenceNumber; }
            set { _planReferenceNumber = value ?? string.Empty; }
        }

        // Getter and Setter for EnrollmentDate
        public DateTime EnrollmentDate
        {
            get { return _enrollmentDate; }
            set { _enrollmentDate = value; }
        }

        // Getter and Setter for MonthlyContribution
        public decimal MonthlyContribution
        {
            get { return _monthlyContribution; }
            set { _monthlyContribution = value; }
        }

        // Override ToString for debugging
        public override string ToString()
        {
            return $"PensionPlan{{PlanReferenceNumber='{_planReferenceNumber}', EnrollmentDate={_enrollmentDate:yyyy-MM-dd}, MonthlyContribution={_monthlyContribution:C}}}";
        }
    }
}
