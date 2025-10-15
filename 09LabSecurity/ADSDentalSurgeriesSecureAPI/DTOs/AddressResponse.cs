namespace ADSDentalSurgeriesSecureAPI.DTOs
{
    /// <summary>
    /// Address response DTO for API responses
    /// </summary>
    public class AddressResponse
    {
        public int AddressId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? Country { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<PatientSimpleResponse>? Patients { get; set; }
        public List<SurgerySimpleResponse>? Surgeries { get; set; }
    }

    /// <summary>
    /// Address response DTO without navigation properties
    /// </summary>
    public class AddressSimpleResponse
    {
        public int AddressId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? Country { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
