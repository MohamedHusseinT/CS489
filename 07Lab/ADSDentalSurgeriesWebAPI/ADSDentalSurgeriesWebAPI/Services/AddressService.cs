using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeriesWebAPI.Data;
using ADSDentalSurgeriesWebAPI.Models;
using ADSDentalSurgeriesWebAPI.Exceptions;

namespace ADSDentalSurgeriesWebAPI.Services
{
    /// <summary>
    /// Service class for Address CRUD operations
    /// </summary>
    public class AddressService
    {
        private readonly ADSDbContext _context;

        public AddressService(ADSDbContext context)
        {
            _context = context;
        }

        // Read All - sorted by City ascending
        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _context.Addresses
                .Include(a => a.Patients)
                .Include(a => a.Surgeries)
                .OrderBy(a => a.City)
                .ToListAsync();
        }

        // Read by ID
        public async Task<Address?> GetAddressByIdAsync(int id)
        {
            return await _context.Addresses
                .Include(a => a.Patients)
                .Include(a => a.Surgeries)
                .FirstOrDefaultAsync(a => a.AddressId == id);
        }

        // Create
        public async Task<Address> CreateAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        // Update
        public async Task<Address?> UpdateAddressAsync(int id, Address updatedAddress)
        {
            var existingAddress = await _context.Addresses.FindAsync(id);
            if (existingAddress == null)
                return null;

            existingAddress.Street = updatedAddress.Street;
            existingAddress.City = updatedAddress.City;
            existingAddress.State = updatedAddress.State;
            existingAddress.ZipCode = updatedAddress.ZipCode;
            existingAddress.Country = updatedAddress.Country;

            await _context.SaveChangesAsync();
            return existingAddress;
        }

        // Delete
        public async Task<bool> DeleteAddressAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return false;

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        // Search addresses by city, state, or street
        public async Task<List<Address>> SearchAddressesAsync(string searchString)
        {
            return await _context.Addresses
                .Include(a => a.Patients)
                .Include(a => a.Surgeries)
                .Where(a => a.City.Contains(searchString) ||
                           a.State.Contains(searchString) ||
                           a.Street.Contains(searchString) ||
                           a.ZipCode.Contains(searchString))
                .OrderBy(a => a.City)
                .ToListAsync();
        }
    }
}
