using System.ComponentModel.DataAnnotations;

namespace RoyalWindsorRealty.Models
{
    /// <summary>
    /// Tenant entity representing lease holders
    /// </summary>
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        public List<Lease> Leases { get; set; } = new List<Lease>();
        
        /// <summary>
        /// Get full name of tenant
        /// </summary>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
        
        /// <summary>
        /// Get total number of leases for this tenant
        /// </summary>
        public int GetTotalLeases()
        {
            return Leases.Count;
        }
        
        /// <summary>
        /// Calculate total rent paid by this tenant across all leases
        /// </summary>
        public decimal GetTotalRentPaid()
        {
            return Leases.Sum(lease => lease.GetTotalRevenue());
        }
        
        /// <summary>
        /// Get active leases for this tenant
        /// </summary>
        public List<Lease> GetActiveLeases()
        {
            var currentDate = DateTime.Now.Date;
            return Leases.Where(lease => 
                currentDate >= lease.StartDate && currentDate <= lease.EndDate).ToList();
        }
    }
}
