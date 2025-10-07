using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalWindsorRealty.Models
{
    /// <summary>
    /// Lease entity representing rental agreements
    /// </summary>
    public class Lease
    {
        [Key]
        public int LeaseId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LeaseNumber { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Monthly rental rate must be greater than 0")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MonthlyRentalRate { get; set; }
        
        [Required]
        public int ApartmentId { get; set; }
        
        [Required]
        public int TenantId { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public Apartment? Apartment { get; set; }
        public Tenant? Tenant { get; set; }
        
        /// <summary>
        /// Calculate lease duration in months
        /// </summary>
        public int GetLeaseDuration()
        {
            return (EndDate.Year - StartDate.Year) * 12 + (EndDate.Month - StartDate.Month);
        }
        
        /// <summary>
        /// Calculate total revenue for this lease
        /// </summary>
        public decimal GetTotalRevenue()
        {
            return MonthlyRentalRate * GetLeaseDuration();
        }
        
        /// <summary>
        /// Check if lease is currently active
        /// </summary>
        public bool IsActive()
        {
            var currentDate = DateTime.Now.Date;
            return currentDate >= StartDate && currentDate <= EndDate;
        }
        
        /// <summary>
        /// Check if lease is expired
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.Now.Date > EndDate;
        }
        
        /// <summary>
        /// Check if lease is future (not yet started)
        /// </summary>
        public bool IsFuture()
        {
            return DateTime.Now.Date < StartDate;
        }
        
        /// <summary>
        /// Get lease status description
        /// </summary>
        public string GetStatus()
        {
            if (IsActive()) return "Active";
            if (IsExpired()) return "Expired";
            if (IsFuture()) return "Future";
            return "Unknown";
        }
    }
}
