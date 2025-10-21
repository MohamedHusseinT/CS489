using System.ComponentModel.DataAnnotations;
using ADSDentalSurgeriesWebApp.Models;

namespace ADSDentalSurgeriesWebApp.ViewModels
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100, ErrorMessage = "Street cannot exceed 100 characters.")]
        [Display(Name = "Street Address")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]
        [StringLength(20, ErrorMessage = "State cannot exceed 20 characters.")]
        [Display(Name = "State")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip Code is required.")]
        [StringLength(10, ErrorMessage = "Zip Code cannot exceed 10 characters.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Country cannot exceed 20 characters.")]
        [Display(Name = "Country")]
        public string? Country { get; set; } = "USA";

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Full Address")]
        public string FullAddress => $"{Street}, {City}, {State} {ZipCode}";

        // Navigation properties
        public List<Patient>? Patients { get; set; }
        public List<Surgery>? Surgeries { get; set; }

        [Display(Name = "Patient Count")]
        public int PatientCount => Patients?.Count ?? 0;

        [Display(Name = "Surgery Count")]
        public int SurgeryCount => Surgeries?.Count ?? 0;
    }

    public class AddressListViewModel
    {
        public List<AddressViewModel> Addresses { get; set; } = new List<AddressViewModel>();
        public string? SearchTerm { get; set; }
        public int TotalCount { get; set; }
    }
}
