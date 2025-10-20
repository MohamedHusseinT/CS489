using System.ComponentModel.DataAnnotations;

namespace PropertyLeasingAPI.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(200)]
        public string PropertyRefName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string State { get; set; } = string.Empty;

        // Navigation property for leases
        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
    }
}



