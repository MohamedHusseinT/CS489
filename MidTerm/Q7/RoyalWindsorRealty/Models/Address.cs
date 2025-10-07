using System.ComponentModel.DataAnnotations;

namespace RoyalWindsorRealty.Models
{
    /// <summary>
    /// Address entity representing physical location of apartments
    /// </summary>
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ApartmentNumber { get; set; } = string.Empty;
        
        [Required]
        public string Street { get; set; } = string.Empty;
        
        [Required]
        public string City { get; set; } = string.Empty;
        
        [Required]
        public string State { get; set; } = string.Empty;
        
        [Required]
        public string ZipCode { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        public Apartment? Apartment { get; set; }
        
        /// <summary>
        /// Get full formatted address
        /// </summary>
        public string GetFullAddress()
        {
            return $"{Street}, {City}, {State} {ZipCode}";
        }
    }
}
