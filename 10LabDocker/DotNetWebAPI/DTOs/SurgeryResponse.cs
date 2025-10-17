namespace ADSDentalSurgeriesWebAPI.DTOs
{
    /// <summary>
    /// Surgery response DTO for API responses
    /// </summary>
    public class SurgeryResponse
    {
        public int SurgeryId { get; set; }
        public string SurgeryNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public AddressSimpleResponse? Address { get; set; }
        public List<AppointmentSimpleResponse>? Appointments { get; set; }
    }

    /// <summary>
    /// Surgery response DTO without navigation properties
    /// </summary>
    public class SurgerySimpleResponse
    {
        public int SurgeryId { get; set; }
        public string SurgeryNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
