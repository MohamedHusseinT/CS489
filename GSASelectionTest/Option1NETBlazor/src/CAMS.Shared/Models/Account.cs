using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAMS.Shared.Models
{
    public class Account
    {
        [Key]
        public long AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; } = string.Empty;

        public DateTime? DateOpened { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public long CustomerId { get; set; }

        // Navigation property
        public virtual Customer Customer { get; set; } = null!;

        // Helper methods
        public bool IsPrimeAccount()
        {
            return Balance > 10000;
        }

        public string GetFormattedBalance()
        {
            return Balance.ToString("C");
        }
    }
}
