using System.ComponentModel.DataAnnotations;

namespace PropertyLeasingAPI.Models
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Email { get; set; }

        // Navigation property for leases
        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
    }
}



