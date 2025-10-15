namespace ADSDentalSurgeriesSecureAPI.DTOs
{
    /// <summary>
    /// Patient response DTO for API responses
    /// </summary>
    public class PatientResponse
    {
        public int PatientId { get; set; }
        public string PatientNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? MailingAddress { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FullName { get; set; } = string.Empty;
        public AddressSimpleResponse? Address { get; set; }
        public List<AppointmentSimpleResponse>? Appointments { get; set; }
    }

    /// <summary>
    /// Patient response DTO without navigation properties
    /// </summary>
    public class PatientSimpleResponse
    {
        public int PatientId { get; set; }
        public string PatientNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? MailingAddress { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
