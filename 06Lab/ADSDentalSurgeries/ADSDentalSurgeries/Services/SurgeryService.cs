using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeries.Data;
using ADSDentalSurgeries.Models;

namespace ADSDentalSurgeries.Services
{
    /// <summary>
    /// Service class for Surgery CRUD operations
    /// </summary>
    public class SurgeryService
    {
        private readonly ADSDbContext _context;

        public SurgeryService(ADSDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Surgery> CreateSurgeryAsync(Surgery surgery)
        {
            _context.Surgeries.Add(surgery);
            await _context.SaveChangesAsync();
            return surgery;
        }

        // Read All
        public async Task<List<Surgery>> GetAllSurgeriesAsync()
        {
            return await _context.Surgeries
                .Include(s => s.Address)
                .Include(s => s.Appointments)
                .OrderBy(s => s.SurgeryNumber)
                .ToListAsync();
        }

        // Read by ID
        public async Task<Surgery?> GetSurgeryByIdAsync(int id)
        {
            return await _context.Surgeries
                .Include(s => s.Address)
                .Include(s => s.Appointments)
                    .ThenInclude(a => a.Patient)
                .Include(s => s.Appointments)
                    .ThenInclude(a => a.Dentist)
                .FirstOrDefaultAsync(s => s.SurgeryId == id);
        }

        // Read by Surgery Number
        public async Task<Surgery?> GetSurgeryByNumberAsync(string surgeryNumber)
        {
            return await _context.Surgeries
                .Include(s => s.Address)
                .Include(s => s.Appointments)
                .FirstOrDefaultAsync(s => s.SurgeryNumber == surgeryNumber);
        }

        // Update
        public async Task<Surgery?> UpdateSurgeryAsync(int id, Surgery updatedSurgery)
        {
            var existingSurgery = await _context.Surgeries.FindAsync(id);
            if (existingSurgery == null)
                return null;

            existingSurgery.SurgeryNumber = updatedSurgery.SurgeryNumber;
            existingSurgery.Name = updatedSurgery.Name;
            existingSurgery.PhoneNumber = updatedSurgery.PhoneNumber;
            existingSurgery.Email = updatedSurgery.Email;
            existingSurgery.AddressId = updatedSurgery.AddressId;

            await _context.SaveChangesAsync();
            return existingSurgery;
        }

        // Delete
        public async Task<bool> DeleteSurgeryAsync(int id)
        {
            var surgery = await _context.Surgeries.FindAsync(id);
            if (surgery == null)
                return false;

            _context.Surgeries.Remove(surgery);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get surgeries with appointments
        public async Task<List<Surgery>> GetSurgeriesWithAppointmentsAsync()
        {
            return await _context.Surgeries
                .Include(s => s.Address)
                .Include(s => s.Appointments)
                    .ThenInclude(a => a.Patient)
                .Include(s => s.Appointments)
                    .ThenInclude(a => a.Dentist)
                .Where(s => s.Appointments.Any())
                .OrderBy(s => s.SurgeryNumber)
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

        // Search surgeries by name or location
        public async Task<List<Surgery>> SearchSurgeriesAsync(string searchTerm)
        {
            return await _context.Surgeries
                .Include(s => s.Address)
                .Include(s => s.Appointments)
                .Where(s => s.Name.Contains(searchTerm) || 
                           s.SurgeryNumber.Contains(searchTerm) ||
                           s.Address.City.Contains(searchTerm) ||
                           s.Address.Street.Contains(searchTerm))
                .OrderBy(s => s.SurgeryNumber)
                .ToListAsync();
        }
    }
}
