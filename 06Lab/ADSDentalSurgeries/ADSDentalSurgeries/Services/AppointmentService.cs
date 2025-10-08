using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeries.Data;
using ADSDentalSurgeries.Models;

namespace ADSDentalSurgeries.Services
{
    /// <summary>
    /// Service class for Appointment CRUD operations
    /// </summary>
    public class AppointmentService
    {
        private readonly ADSDbContext _context;

        public AppointmentService(ADSDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        // Read All
        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Include(a => a.Surgery)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();
        }

        // Read by ID
        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                    .ThenInclude(p => p.Address)
                .Include(a => a.Dentist)
                .Include(a => a.Surgery)
                    .ThenInclude(s => s.Address)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        // Read by Appointment Number
        public async Task<Appointment?> GetAppointmentByNumberAsync(string appointmentNumber)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Include(a => a.Surgery)
                .FirstOrDefaultAsync(a => a.AppointmentNumber == appointmentNumber);
        }

        // Update
        public async Task<Appointment?> UpdateAppointmentAsync(int id, Appointment updatedAppointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null)
                return null;

            existingAppointment.AppointmentNumber = updatedAppointment.AppointmentNumber;
            existingAppointment.AppointmentDate = updatedAppointment.AppointmentDate;
            existingAppointment.AppointmentTime = updatedAppointment.AppointmentTime;
            existingAppointment.Notes = updatedAppointment.Notes;
            existingAppointment.Status = updatedAppointment.Status;
            existingAppointment.PatientId = updatedAppointment.PatientId;
            existingAppointment.DentistId = updatedAppointment.DentistId;
            existingAppointment.SurgeryId = updatedAppointment.SurgeryId;
            existingAppointment.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        // Delete
        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get appointments for a specific dentist
        public async Task<List<Appointment>> GetAppointmentsForDentistAsync(int dentistId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Surgery)
                .Where(a => a.DentistId == dentistId)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();
        }

        // Get appointments for a specific patient
        public async Task<List<Appointment>> GetAppointmentsForPatientAsync(int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Surgery)
                .Where(a => a.PatientId == patientId)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();
        }

        // Get appointments for a specific surgery
        public async Task<List<Appointment>> GetAppointmentsForSurgeryAsync(int surgeryId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Where(a => a.SurgeryId == surgeryId)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();
        }

        // Get appointments for a specific date
        public async Task<List<Appointment>> GetAppointmentsForDateAsync(DateTime date)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Include(a => a.Surgery)
                .Where(a => a.AppointmentDate.Date == date.Date)
                .OrderBy(a => a.AppointmentTime)
                .ToListAsync();
        }

        // Get appointments by status
        public async Task<List<Appointment>> GetAppointmentsByStatusAsync(string status)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Include(a => a.Surgery)
                .Where(a => a.Status == status)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();
        }

        // Check if dentist has more than 5 appointments in a week
        public async Task<bool> IsDentistOverbookedAsync(int dentistId, DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(7);
            var appointmentCount = await _context.Appointments
                .Where(a => a.DentistId == dentistId && 
                           a.AppointmentDate >= weekStart && 
                           a.AppointmentDate < weekEnd)
                .CountAsync();
            
            return appointmentCount >= 5;
        }
    }
}
