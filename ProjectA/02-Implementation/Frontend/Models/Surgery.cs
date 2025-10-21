using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSDentalSurgeriesWebApp.Models
{
    public class Surgery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SurgeryId { get; set; }

        [Required]
        [StringLength(20)]
        public string SurgeryNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public int AddressId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public override string ToString()
        {
            return $"{SurgeryNumber} - {Name}";
        }
    }
}
