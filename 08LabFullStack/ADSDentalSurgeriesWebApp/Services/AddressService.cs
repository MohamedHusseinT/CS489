using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeriesWebApp.Data;
using ADSDentalSurgeriesWebApp.Models;
using ADSDentalSurgeriesWebApp.ViewModels;

namespace ADSDentalSurgeriesWebApp.Services
{
    public class AddressService
    {
        private readonly ADSDbContext _context;

        public AddressService(ADSDbContext context)
        {
            _context = context;
        }

        public async Task<List<AddressViewModel>> GetAllAddressesAsync()
        {
            var addresses = await _context.Addresses
                .Include(a => a.Patients)
                .Include(a => a.Surgeries)
                .OrderBy(a => a.City)
                .ThenBy(a => a.Street)
                .ToListAsync();

            return addresses.Select(a => new AddressViewModel
            {
                AddressId = a.AddressId,
                Street = a.Street,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode,
                Country = a.Country,
                CreatedDate = a.CreatedDate,
                Patients = a.Patients.ToList(),
                Surgeries = a.Surgeries.ToList()
            }).ToList();
        }

        public async Task<AddressViewModel?> GetAddressByIdAsync(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.Patients)
                .Include(a => a.Surgeries)
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null) return null;

            return new AddressViewModel
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                CreatedDate = address.CreatedDate,
                Patients = address.Patients.ToList(),
                Surgeries = address.Surgeries.ToList()
            };
        }

        public async Task<List<AddressViewModel>> SearchAddressesAsync(string searchTerm)
        {
            var addresses = await _context.Addresses
                .Include(a => a.Patients)
                .Include(a => a.Surgeries)
                .Where(a => a.Street.Contains(searchTerm) ||
                           a.City.Contains(searchTerm) ||
                           a.State.Contains(searchTerm) ||
                           a.ZipCode.Contains(searchTerm))
                .OrderBy(a => a.City)
                .ThenBy(a => a.Street)
                .ToListAsync();

            return addresses.Select(a => new AddressViewModel
            {
                AddressId = a.AddressId,
                Street = a.Street,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode,
                Country = a.Country,
                CreatedDate = a.CreatedDate,
                Patients = a.Patients.ToList(),
                Surgeries = a.Surgeries.ToList()
            }).ToList();
        }

        public async Task<AddressViewModel> CreateAddressAsync(AddressViewModel addressViewModel)
        {
            var address = new Address
            {
                Street = addressViewModel.Street,
                City = addressViewModel.City,
                State = addressViewModel.State,
                ZipCode = addressViewModel.ZipCode,
                Country = addressViewModel.Country ?? "USA",
                CreatedDate = DateTime.Now
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return await GetAddressByIdAsync(address.AddressId) ?? addressViewModel;
        }

        public async Task<AddressViewModel?> UpdateAddressAsync(int id, AddressViewModel addressViewModel)
        {
            var existingAddress = await _context.Addresses.FindAsync(id);
            if (existingAddress == null) return null;

            existingAddress.Street = addressViewModel.Street;
            existingAddress.City = addressViewModel.City;
            existingAddress.State = addressViewModel.State;
            existingAddress.ZipCode = addressViewModel.ZipCode;
            existingAddress.Country = addressViewModel.Country ?? "USA";

            await _context.SaveChangesAsync();

            return await GetAddressByIdAsync(id);
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return false;

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
