using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSDentalSurgeriesWebAPI.Models
{
    /// <summary>
    /// Appointment entity representing scheduled appointments between patients and dentists
    /// </summary>
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string AppointmentNumber { get; set; } = string.Empty;

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, Completed, Cancelled, No-Show

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DentistId { get; set; }

        [Required]
        public int SurgeryId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; } = null!;

        [ForeignKey("DentistId")]
        public virtual Dentist Dentist { get; set; } = null!;

        [ForeignKey("SurgeryId")]
        public virtual Surgery Surgery { get; set; } = null!;

        public string FormattedDateTime => $"{AppointmentDate:dd-MMM-yy} {AppointmentTime:hh\\.mm}";

        public override string ToString()
        {
            return $"{AppointmentNumber} - {Patient?.FullName} with {Dentist?.FullName} on {FormattedDateTime}";
        }
    }
}
