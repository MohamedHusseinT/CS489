namespace ADSDentalSurgeriesSecureAPI.Exceptions
{
    /// <summary>
    /// Exception thrown when a patient is not found
    /// </summary>
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(int patientId) 
            : base($"Patient with ID {patientId} was not found.")
        {
            PatientId = patientId;
        }

        public PatientNotFoundException(string message) : base(message)
        {
        }

        public PatientNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        public int? PatientId { get; }
    }
}
