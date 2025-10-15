using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSDentalSurgeriesSecureAPI.Models
{
    /// <summary>
    /// Dentist entity representing dentists who provide services
    /// </summary>
    public class Dentist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DentistId { get; set; }

        [Required]
        [StringLength(20)]
        public string DentistNumber { get; set; } = string.Empty; // e.g., D001, D002, D003

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

        [StringLength(100)]
        public string? Specialization { get; set; }

        public DateTime? DateOfEmployment { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public string FullName => $"{FirstName} {LastName}";

        public override string ToString()
        {
            return $"{DentistNumber} - {FullName}";
        }
    }
}
