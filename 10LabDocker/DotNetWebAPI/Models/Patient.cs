using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSDentalSurgeriesWebAPI.Models
{
    /// <summary>
    /// Patient entity representing patients who book appointments
    /// </summary>
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        [Required]
        [StringLength(20)]
        public string PatientNumber { get; set; } = string.Empty; // e.g., P100, P105, P108, P110

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

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public string FullName => $"{FirstName} {LastName}";

        public override string ToString()
        {
            return $"{PatientNumber} - {FullName}";
        }
    }
}
