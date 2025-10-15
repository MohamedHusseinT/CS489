namespace ADSDentalSurgeriesSecureAPI.DTOs
{
    /// <summary>
    /// Appointment response DTO without navigation properties
    /// </summary>
    public class AppointmentSimpleResponse
    {
        public int AppointmentId { get; set; }
        public string AppointmentNumber { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public int DentistId { get; set; }
        public int SurgeryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string FormattedDateTime { get; set; } = string.Empty;
    }
}
