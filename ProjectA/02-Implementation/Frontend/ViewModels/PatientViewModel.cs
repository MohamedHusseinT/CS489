using System.ComponentModel.DataAnnotations;
using ADSDentalSurgeriesWebApp.Models;

namespace ADSDentalSurgeriesWebApp.ViewModels
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Patient Number is required.")]
        [StringLength(20, ErrorMessage = "Patient Number cannot exceed 20 characters.")]
        [Display(Name = "Patient Number")]
        public string PatientNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Phone Number cannot exceed 20 characters.")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(500, ErrorMessage = "Mailing Address cannot exceed 500 characters.")]
        [Display(Name = "Mailing Address")]
        public string? MailingAddress { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public int AddressId { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        // Navigation properties
        public Address? Address { get; set; }
        public List<Appointment>? Appointments { get; set; }

        // For dropdown selection
        public List<Address>? AvailableAddresses { get; set; }
    }

    public class PatientListViewModel
    {
        public List<PatientViewModel> Patients { get; set; } = new List<PatientViewModel>();
        public string? SearchTerm { get; set; }
        public int TotalCount { get; set; }
    }
}
