using Microsoft.EntityFrameworkCore;
using PropertyLeasingAPI.Data;
using PropertyLeasingAPI.DTOs;
using PropertyLeasingAPI.Models;

namespace PropertyLeasingAPI.Services
{
    public interface ILeaseService
    {
        Task<List<LeaseResponse>> GetAllLeasesAsync();
        Task<RevenueResponse> GetProjectedRevenueByStateAsync(string stateCode);
        Task<LeaseResponse> CreateLeaseAsync(int propertyId, CreateLeaseRequest request);
    }

    public class LeaseService : ILeaseService
    {
        private readonly PropertyLeasingDbContext _context;

        public LeaseService(PropertyLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaseResponse>> GetAllLeasesAsync()
        {
            var leases = await _context.Leases
                .Include(l => l.Property)
                .Include(l => l.Tenant)
                .OrderByDescending(l => l.LeaseNumber)
                .ToListAsync();

            return leases.Select(l => new LeaseResponse
            {
                LeaseId = l.LeaseId,
                LeaseNumber = l.LeaseNumber,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                MonthlyRentalRate = l.MonthlyRentalRate,
                Property = new PropertySimpleResponse
                {
                    PropertyId = l.Property.PropertyId,
                    PropertyRefName = l.Property.PropertyRefName,
                    City = l.Property.City,
                    State = l.Property.State
                },
                Tenant = new TenantSimpleResponse
                {
                    TenantId = l.Tenant.TenantId,
                    FirstName = l.Tenant.FirstName,
                    LastName = l.Tenant.LastName,
                    PhoneNumber = l.Tenant.PhoneNumber,
                    Email = l.Tenant.Email
                }
            }).ToList();
        }

        public async Task<RevenueResponse> GetProjectedRevenueByStateAsync(string stateCode)
        {
            var leases = await _context.Leases
                .Include(l => l.Property)
                .Where(l => l.Property.State.ToUpper() == stateCode.ToUpper())
                .ToListAsync();

            decimal totalRevenue = 0;

            foreach (var lease in leases)
            {
                var months = ((lease.EndDate.Year - lease.StartDate.Year) * 12) + lease.EndDate.Month - lease.StartDate.Month;
                totalRevenue += lease.MonthlyRentalRate * months;
            }

            return new RevenueResponse
            {
                State = stateCode.ToUpper(),
                ProjectedTotalRevenue = totalRevenue
            };
        }

        public async Task<LeaseResponse> CreateLeaseAsync(int propertyId, CreateLeaseRequest request)
        {
            // Check if property exists
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null)
            {
                throw new ArgumentException($"Property with ID {propertyId} not found.");
            }

            // Validate tenant input - either TenantId or Tenant object must be provided
            if (!request.TenantId.HasValue && request.Tenant == null)
            {
                throw new ArgumentException("Either TenantId or Tenant information must be provided.");
            }

            if (request.TenantId.HasValue && request.Tenant != null)
            {
                throw new ArgumentException("Provide either TenantId OR Tenant information, not both.");
            }

            int tenantId;

            // Handle existing tenant scenario
            if (request.TenantId.HasValue)
            {
                var existingTenant = await _context.Tenants.FindAsync(request.TenantId.Value);
                if (existingTenant == null)
                {
                    throw new ArgumentException($"Tenant with ID {request.TenantId.Value} not found.");
                }
                tenantId = request.TenantId.Value;
            }
            // Handle new tenant scenario
            else
            {
                if (string.IsNullOrWhiteSpace(request.Tenant!.FirstName) || 
                    string.IsNullOrWhiteSpace(request.Tenant.LastName) || 
                    string.IsNullOrWhiteSpace(request.Tenant.PhoneNumber))
                {
                    throw new ArgumentException("Tenant FirstName, LastName, and PhoneNumber are required.");
                }

                var newTenant = new Tenant
                {
                    FirstName = request.Tenant.FirstName,
                    LastName = request.Tenant.LastName,
                    PhoneNumber = request.Tenant.PhoneNumber,
                    Email = request.Tenant.Email
                };

                _context.Tenants.Add(newTenant);
                await _context.SaveChangesAsync();
                tenantId = newTenant.TenantId;
            }

            // Check if lease number already exists
            var existingLease = await _context.Leases.FirstOrDefaultAsync(l => l.LeaseNumber == request.LeaseReferenceNumber);
            if (existingLease != null)
            {
                throw new ArgumentException($"Lease with number {request.LeaseReferenceNumber} already exists.");
            }

            var newLease = new Lease
            {
                LeaseNumber = request.LeaseReferenceNumber,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MonthlyRentalRate = request.MonthlyRentalRate,
                PropertyId = propertyId,
                TenantId = tenantId
            };

            _context.Leases.Add(newLease);
            await _context.SaveChangesAsync();

            // Return the created lease with full details
            var createdLease = await _context.Leases
                .Include(l => l.Property)
                .Include(l => l.Tenant)
                .FirstAsync(l => l.LeaseId == newLease.LeaseId);

            return new LeaseResponse
            {
                LeaseId = createdLease.LeaseId,
                LeaseNumber = createdLease.LeaseNumber,
                StartDate = createdLease.StartDate,
                EndDate = createdLease.EndDate,
                MonthlyRentalRate = createdLease.MonthlyRentalRate,
                Property = new PropertySimpleResponse
                {
                    PropertyId = createdLease.Property.PropertyId,
                    PropertyRefName = createdLease.Property.PropertyRefName,
                    City = createdLease.Property.City,
                    State = createdLease.Property.State
                },
                Tenant = new TenantSimpleResponse
                {
                    TenantId = createdLease.Tenant.TenantId,
                    FirstName = createdLease.Tenant.FirstName,
                    LastName = createdLease.Tenant.LastName,
                    PhoneNumber = createdLease.Tenant.PhoneNumber,
                    Email = createdLease.Tenant.Email
                }
            };
        }
    }
}
