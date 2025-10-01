using System.ComponentModel.DataAnnotations;

namespace CAMS.Shared.Models
{
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        // Navigation property
        public virtual Account? Account { get; set; }

        // Helper method
        public string GetFullName()
        {
            return $"{FirstName.Trim()} {LastName.Trim()}".Trim();
        }
    }
}
