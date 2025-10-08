using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeries.Data;
using ADSDentalSurgeries.Models;

namespace ADSDentalSurgeries.Services
{
    /// <summary>
    /// Service class for Patient CRUD operations
    /// </summary>
    public class PatientService
    {
        private readonly ADSDbContext _context;

        public PatientService(ADSDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        // Read All
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.Appointments)
                .OrderBy(p => p.PatientNumber)
                .ToListAsync();
        }

        // Read by ID
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Dentist)
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Surgery)
                .FirstOrDefaultAsync(p => p.PatientId == id);
        }

        // Read by Patient Number
        public async Task<Patient?> GetPatientByNumberAsync(string patientNumber)
        {
            return await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.PatientNumber == patientNumber);
        }

        // Update
        public async Task<Patient?> UpdatePatientAsync(int id, Patient updatedPatient)
        {
            var existingPatient = await _context.Patients.FindAsync(id);
            if (existingPatient == null)
                return null;

            existingPatient.PatientNumber = updatedPatient.PatientNumber;
            existingPatient.FirstName = updatedPatient.FirstName;
            existingPatient.LastName = updatedPatient.LastName;
            existingPatient.PhoneNumber = updatedPatient.PhoneNumber;
            existingPatient.Email = updatedPatient.Email;
            existingPatient.DateOfBirth = updatedPatient.DateOfBirth;
            existingPatient.MailingAddress = updatedPatient.MailingAddress;
            existingPatient.AddressId = updatedPatient.AddressId;

            await _context.SaveChangesAsync();
            return existingPatient;
        }

        // Delete
        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get patients with appointments
        public async Task<List<Patient>> GetPatientsWithAppointmentsAsync()
        {
            return await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Dentist)
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Surgery)
                .Where(p => p.Appointments.Any())
                .OrderBy(p => p.PatientNumber)
                .ToListAsync();
        }

        // Search patients by name
        public async Task<List<Patient>> SearchPatientsByNameAsync(string searchTerm)
        {
            return await _context.Patients
                .Include(p => p.Address)
                .Where(p => p.FirstName.Contains(searchTerm) || 
                           p.LastName.Contains(searchTerm) ||
                           p.PatientNumber.Contains(searchTerm))
                .OrderBy(p => p.PatientNumber)
                .ToListAsync();
        }
    }
}
