using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSDentalSurgeriesWebAPI.Models
{
    /// <summary>
    /// Address entity representing physical addresses for surgeries and patients
    /// </summary>
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required]
        [StringLength(100)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Country { get; set; } = "USA";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<Surgery> Surgeries { get; set; } = new List<Surgery>();
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {ZipCode}";
        }
    }
}
