using System.ComponentModel.DataAnnotations;

namespace ADSDentalSurgeriesWebAPI.DTOs
{
    /// <summary>
    /// Patient request DTO for API requests
    /// </summary>
    public class PatientRequest
    {
        [Required]
        [StringLength(20)]
        public string PatientNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(500)]
        public string? MailingAddress { get; set; }

        [Required]
        public int AddressId { get; set; }
    }
}
