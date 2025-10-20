namespace PropertyLeasingAPI.DTOs
{
    public class PropertyResponse
    {
        public int PropertyId { get; set; }
        public string PropertyRefName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public List<LeaseSimpleResponse> Leases { get; set; } = new List<LeaseSimpleResponse>();
    }

    public class LeaseSimpleResponse
    {
        public long LeaseId { get; set; }
        public long LeaseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRentalRate { get; set; }
    }

    public class LeaseResponse
    {
        public long LeaseId { get; set; }
        public long LeaseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRentalRate { get; set; }
        public PropertySimpleResponse Property { get; set; } = new PropertySimpleResponse();
        public TenantSimpleResponse Tenant { get; set; } = new TenantSimpleResponse();
    }

    public class PropertySimpleResponse
    {
        public int PropertyId { get; set; }
        public string PropertyRefName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }

    public class TenantSimpleResponse
    {
        public int TenantId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
    }

    public class RevenueResponse
    {
        public string State { get; set; } = string.Empty;
        public decimal ProjectedTotalRevenue { get; set; }
    }

    public class CreateLeaseRequest
    {
        public long LeaseReferenceNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRentalRate { get; set; }
        public int? TenantId { get; set; }
        public CreateTenantRequest? Tenant { get; set; }
    }

    public class CreateTenantRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
