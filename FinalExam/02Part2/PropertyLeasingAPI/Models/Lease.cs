using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyLeasingAPI.Models
{
    public class Lease
    {
        [Key]
        public long LeaseId { get; set; }

        [Required]
        public long LeaseNumber { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MonthlyRentalRate { get; set; }

        // Foreign Keys
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual Property Property { get; set; } = null!;
        public virtual Tenant Tenant { get; set; } = null!;
    }
}




