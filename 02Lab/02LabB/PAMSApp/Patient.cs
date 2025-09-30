using System;

namespace PAMSApp.Model
{
    public class Patient
    {
        // Private fields
        private int _patientId;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private string _email;
        private string _mailingAddress;
        private DateTime _dateOfBirth;

        // Default constructor
        public Patient()
        {
            _patientId = 0;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _phoneNumber = string.Empty;
            _email = string.Empty;
            _mailingAddress = string.Empty;
            _dateOfBirth = DateTime.MinValue;
        }

        // Parameterized constructor
        public Patient(int patientId, string firstName, string lastName, string phoneNumber, 
                      string email, string mailingAddress, DateTime dateOfBirth)
        {
            _patientId = patientId;
            _firstName = firstName ?? string.Empty;
            _lastName = lastName ?? string.Empty;
            _phoneNumber = phoneNumber ?? string.Empty;
            _email = email ?? string.Empty;
            _mailingAddress = mailingAddress ?? string.Empty;
            _dateOfBirth = dateOfBirth;
        }

        // Copy constructor
        public Patient(Patient? other)
        {
            if (other != null)
            {
                _patientId = other._patientId;
                _firstName = other._firstName;
                _lastName = other._lastName;
                _phoneNumber = other._phoneNumber;
                _email = other._email;
                _mailingAddress = other._mailingAddress;
                _dateOfBirth = other._dateOfBirth;
            }
        }

        // Getter and Setter for PatientId
        public int PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
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

        // Getter and Setter for PhoneNumber
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value ?? string.Empty; }
        }

        // Getter and Setter for Email
        public string Email
        {
            get { return _email; }
            set { _email = value ?? string.Empty; }
        }

        // Getter and Setter for MailingAddress
        public string MailingAddress
        {
            get { return _mailingAddress; }
            set { _mailingAddress = value ?? string.Empty; }
        }

        // Getter and Setter for DateOfBirth
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        // Method to calculate current age
        public int GetCurrentAge()
        {
            var today = DateTime.Today;
            var age = today.Year - _dateOfBirth.Year;
            
            // Go back to the year the person was born in if we haven't reached their birthday this year
            if (_dateOfBirth.Date > today.AddYears(-age))
                age--;
                
            return age;
        }

        // Method to get full name
        public string GetFullName()
        {
            return $"{_firstName} {_lastName}".Trim();
        }

        // Override ToString for debugging
        public override string ToString()
        {
            return $"Patient{{PatientId={_patientId}, Name='{GetFullName()}', Phone='{_phoneNumber}', Email='{_email}', Address='{_mailingAddress}', DateOfBirth={_dateOfBirth:yyyy-MM-dd}, Age={GetCurrentAge()}}}";
        }
    }
}
