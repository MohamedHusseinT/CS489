using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeriesWebApp.Data;
using ADSDentalSurgeriesWebApp.Models;
using ADSDentalSurgeriesWebApp.ViewModels;

namespace ADSDentalSurgeriesWebApp.Services
{
    public class PatientService
    {
        private readonly ADSDbContext _context;

        public PatientService(ADSDbContext context)
        {
            _context = context;
        }

        public async Task<List<PatientViewModel>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.Appointments)
                .OrderBy(p => p.LastName)
                .ToListAsync();

            return patients.Select(p => new PatientViewModel
            {
                PatientId = p.PatientId,
                PatientNumber = p.PatientNumber,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                MailingAddress = p.MailingAddress,
                AddressId = p.AddressId,
                CreatedDate = p.CreatedDate,
                Address = p.Address,
                Appointments = p.Appointments.ToList()
            }).ToList();
        }

        public async Task<PatientViewModel?> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null) return null;

            return new PatientViewModel
            {
                PatientId = patient.PatientId,
                PatientNumber = patient.PatientNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                DateOfBirth = patient.DateOfBirth,
                MailingAddress = patient.MailingAddress,
                AddressId = patient.AddressId,
                CreatedDate = patient.CreatedDate,
                Address = patient.Address,
                Appointments = patient.Appointments.ToList()
            };
        }

        public async Task<List<PatientViewModel>> SearchPatientsAsync(string searchTerm)
        {
            var patients = await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.Appointments)
                .Where(p => p.FirstName.Contains(searchTerm) ||
                           p.LastName.Contains(searchTerm) ||
                           p.PatientNumber.Contains(searchTerm) ||
                           (p.Email != null && p.Email.Contains(searchTerm)) ||
                           (p.PhoneNumber != null && p.PhoneNumber.Contains(searchTerm)))
                .OrderBy(p => p.LastName)
                .ToListAsync();

            return patients.Select(p => new PatientViewModel
            {
                PatientId = p.PatientId,
                PatientNumber = p.PatientNumber,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                MailingAddress = p.MailingAddress,
                AddressId = p.AddressId,
                CreatedDate = p.CreatedDate,
                Address = p.Address,
                Appointments = p.Appointments.ToList()
            }).ToList();
        }

        public async Task<PatientViewModel> CreatePatientAsync(PatientViewModel patientViewModel)
        {
            var patient = new Patient
            {
                PatientNumber = patientViewModel.PatientNumber,
                FirstName = patientViewModel.FirstName,
                LastName = patientViewModel.LastName,
                PhoneNumber = patientViewModel.PhoneNumber,
                Email = patientViewModel.Email,
                DateOfBirth = patientViewModel.DateOfBirth,
                MailingAddress = patientViewModel.MailingAddress,
                AddressId = patientViewModel.AddressId,
                CreatedDate = DateTime.Now
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return await GetPatientByIdAsync(patient.PatientId) ?? patientViewModel;
        }

        public async Task<PatientViewModel?> UpdatePatientAsync(int id, PatientViewModel patientViewModel)
        {
            var existingPatient = await _context.Patients.FindAsync(id);
            if (existingPatient == null) return null;

            existingPatient.PatientNumber = patientViewModel.PatientNumber;
            existingPatient.FirstName = patientViewModel.FirstName;
            existingPatient.LastName = patientViewModel.LastName;
            existingPatient.PhoneNumber = patientViewModel.PhoneNumber;
            existingPatient.Email = patientViewModel.Email;
            existingPatient.DateOfBirth = patientViewModel.DateOfBirth;
            existingPatient.MailingAddress = patientViewModel.MailingAddress;
            existingPatient.AddressId = patientViewModel.AddressId;

            await _context.SaveChangesAsync();

            return await GetPatientByIdAsync(id);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Address>> GetAvailableAddressesAsync()
        {
            return await _context.Addresses
                .OrderBy(a => a.City)
                .ThenBy(a => a.Street)
                .ToListAsync();
        }
    }
}
