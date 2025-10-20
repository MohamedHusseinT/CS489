using Microsoft.EntityFrameworkCore;
using PropertyLeasingAPI.Data;
using PropertyLeasingAPI.DTOs;
using PropertyLeasingAPI.Models;

namespace PropertyLeasingAPI.Services
{
    public interface IPropertyService
    {
        Task<List<PropertyResponse>> GetPropertiesByStateAsync(string stateCode);
    }

    public class PropertyService : IPropertyService
    {
        private readonly PropertyLeasingDbContext _context;

        public PropertyService(PropertyLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<List<PropertyResponse>> GetPropertiesByStateAsync(string stateCode)
        {
            var properties = await _context.Properties
                .Include(p => p.Leases)
                .Where(p => p.State.ToUpper() == stateCode.ToUpper())
                .OrderBy(p => p.PropertyRefName)
                .ToListAsync();

            return properties.Select(p => new PropertyResponse
            {
                PropertyId = p.PropertyId,
                PropertyRefName = p.PropertyRefName,
                City = p.City,
                State = p.State,
                Leases = p.Leases.Select(l => new LeaseSimpleResponse
                {
                    LeaseId = l.LeaseId,
                    LeaseNumber = l.LeaseNumber,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    MonthlyRentalRate = l.MonthlyRentalRate
                }).ToList()
            }).ToList();
        }
    }
}



