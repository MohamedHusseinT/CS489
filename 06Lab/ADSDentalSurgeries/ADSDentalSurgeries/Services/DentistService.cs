using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeries.Data;
using ADSDentalSurgeries.Models;

namespace ADSDentalSurgeries.Services
{
    /// <summary>
    /// Service class for Dentist CRUD operations
    /// </summary>
    public class DentistService
    {
        private readonly ADSDbContext _context;

        public DentistService(ADSDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Dentist> CreateDentistAsync(Dentist dentist)
        {
            _context.Dentists.Add(dentist);
            await _context.SaveChangesAsync();
            return dentist;
        }

        // Read All
        public async Task<List<Dentist>> GetAllDentistsAsync()
        {
            return await _context.Dentists
                .Include(d => d.Appointments)
                .OrderBy(d => d.DentistNumber)
                .ToListAsync();
        }

        // Read by ID
        public async Task<Dentist?> GetDentistByIdAsync(int id)
        {
            return await _context.Dentists
                .Include(d => d.Appointments)
                    .ThenInclude(a => a.Patient)
                .Include(d => d.Appointments)
                    .ThenInclude(a => a.Surgery)
                .FirstOrDefaultAsync(d => d.DentistId == id);
        }

        // Read by Dentist Number
        public async Task<Dentist?> GetDentistByNumberAsync(string dentistNumber)
        {
            return await _context.Dentists
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.DentistNumber == dentistNumber);
        }

        // Update
        public async Task<Dentist?> UpdateDentistAsync(int id, Dentist updatedDentist)
        {
            var existingDentist = await _context.Dentists.FindAsync(id);
            if (existingDentist == null)
                return null;

            existingDentist.DentistNumber = updatedDentist.DentistNumber;
            existingDentist.FirstName = updatedDentist.FirstName;
            existingDentist.LastName = updatedDentist.LastName;
            existingDentist.PhoneNumber = updatedDentist.PhoneNumber;
            existingDentist.Email = updatedDentist.Email;
            existingDentist.Specialization = updatedDentist.Specialization;
            existingDentist.DateOfEmployment = updatedDentist.DateOfEmployment;
            existingDentist.IsActive = updatedDentist.IsActive;

            await _context.SaveChangesAsync();
            return existingDentist;
        }

        // Delete
        public async Task<bool> DeleteDentistAsync(int id)
        {
            var dentist = await _context.Dentists.FindAsync(id);
            if (dentist == null)
                return false;

            _context.Dentists.Remove(dentist);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get dentists with appointments
        public async Task<List<Dentist>> GetDentistsWithAppointmentsAsync()
        {
            return await _context.Dentists
                .Include(d => d.Appointments)
                    .ThenInclude(a => a.Patient)
                .Include(d => d.Appointments)
                    .ThenInclude(a => a.Surgery)
                .Where(d => d.Appointments.Any())
                .OrderBy(d => d.DentistNumber)
                .ToListAsync();
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

        // Search dentists by name or specialization
        public async Task<List<Dentist>> SearchDentistsAsync(string searchTerm)
        {
            return await _context.Dentists
                .Include(d => d.Appointments)
                .Where(d => d.FirstName.Contains(searchTerm) || 
                           d.LastName.Contains(searchTerm) ||
                           d.DentistNumber.Contains(searchTerm) ||
                           (d.Specialization != null && d.Specialization.Contains(searchTerm)))
                .OrderBy(d => d.DentistNumber)
                .ToListAsync();
        }
    }
}
