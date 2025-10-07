using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalWindsorRealty.Models
{
    /// <summary>
    /// Apartment entity representing rental units
    /// </summary>
    public class Apartment
    {
        [Key]
        public int ApartmentId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ApartmentNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string PropertyName { get; set; } = string.Empty;
        
        public int? FloorNo { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Size must be greater than 0")]
        public int Size { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of rooms must be greater than 0")]
        public int NumberOfRooms { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public Address? Address { get; set; }
        public List<Lease> Leases { get; set; } = new List<Lease>();
        
        /// <summary>
        /// Calculate total revenue from all leases for this apartment
        /// </summary>
        public decimal GetTotalRevenue()
        {
            return Leases.Sum(lease => lease.GetTotalRevenue());
        }
        
        /// <summary>
        /// Get active leases (current date within lease period)
        /// </summary>
        public List<Lease> GetActiveLeases()
        {
            var currentDate = DateTime.Now.Date;
            return Leases.Where(lease => 
                currentDate >= lease.StartDate && currentDate <= lease.EndDate).ToList();
        }
        
        /// <summary>
        /// Get lease count for this apartment
        /// </summary>
        public int GetLeaseCount()
        {
            return Leases.Count;
        }
    }
}
