namespace ADSDentalSurgeriesSecureAPI.Exceptions
{
    /// <summary>
    /// Exception thrown when an address is not found
    /// </summary>
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException(int addressId) 
            : base($"Address with ID {addressId} was not found.")
        {
            AddressId = addressId;
        }

        public AddressNotFoundException(string message) : base(message)
        {
        }

        public AddressNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        public int? AddressId { get; }
    }
}
