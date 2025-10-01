using System.ComponentModel.DataAnnotations;

namespace CAMS.Shared.DTOs
{
    public class CustomerAccountDto
    {
        public long AccountId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public DateTime? DateOpened { get; set; }
        public decimal Balance { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

    public class CreateCustomerAccountDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Number is required.")]
        [StringLength(50, ErrorMessage = "Account Number cannot exceed 50 characters.")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Type is required.")]
        [StringLength(50, ErrorMessage = "Account Type cannot exceed 50 characters.")]
        public string AccountType { get; set; } = string.Empty;

        public DateTime? DateOpened { get; set; }

        [Required(ErrorMessage = "Balance is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Balance must be a positive value.")]
        public decimal Balance { get; set; }
    }

    public class LiquidityPositionDto
    {
        public decimal TotalBalance { get; set; }
        public int TotalAccounts { get; set; }
        public int PrimeAccounts { get; set; }
    }
}
