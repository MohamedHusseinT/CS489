using Microsoft.AspNetCore.Mvc;
using PropertyLeasingAPI.DTOs;
using PropertyLeasingAPI.Services;

namespace PropertyLeasingAPI.Controllers
{
    [ApiController]
    [Route("propmgmt/api/leases")]
    public class LeaseController : ControllerBase
    {
        private readonly ILeaseService _leaseService;

        public LeaseController(ILeaseService leaseService)
        {
            _leaseService = leaseService;
        }

        /// <summary>
        /// Get all leases sorted by lease reference number in descending order
        /// </summary>
        /// <returns>List of all leases with property and tenant information</returns>
        [HttpGet]
        public async Task<ActionResult<List<LeaseResponse>>> GetAllLeases()
        {
            try
            {
                var leases = await _leaseService.GetAllLeasesAsync();
                return Ok(leases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get projected total revenue for leases in a given state
        /// </summary>
        /// <param name="stateCode">State code (e.g., CO, TX)</param>
        /// <returns>Projected total revenue for the state</returns>
        [HttpGet("revenue/{stateCode}")]
        public async Task<ActionResult<RevenueResponse>> GetProjectedRevenueByState(string stateCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stateCode))
                {
                    return BadRequest("State code is required.");
                }

                var revenue = await _leaseService.GetProjectedRevenueByStateAsync(stateCode);
                return Ok(revenue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new lease for a property
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="request">Lease details</param>
        /// <returns>Created lease with property and tenant information</returns>
        [HttpPost("new/{propertyId}")]
        public async Task<ActionResult<LeaseResponse>> CreateLease(int propertyId, [FromBody] CreateLeaseRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Lease data is required.");
                }

                if (request.LeaseReferenceNumber <= 0)
                {
                    return BadRequest("Valid lease reference number is required.");
                }

                if (request.StartDate >= request.EndDate)
                {
                    return BadRequest("End date must be after start date.");
                }

                if (request.MonthlyRentalRate <= 0)
                {
                    return BadRequest("Monthly rental rate must be greater than zero.");
                }

                // Validate tenant input
                if (!request.TenantId.HasValue && request.Tenant == null)
                {
                    return BadRequest("Either TenantId or Tenant information must be provided.");
                }

                if (request.TenantId.HasValue && request.Tenant != null)
                {
                    return BadRequest("Provide either TenantId OR Tenant information, not both.");
                }

                if (request.TenantId.HasValue && request.TenantId <= 0)
                {
                    return BadRequest("Valid tenant ID is required.");
                }

                var createdLease = await _leaseService.CreateLeaseAsync(propertyId, request);
                return CreatedAtAction(nameof(GetAllLeases), new { id = createdLease.LeaseId }, createdLease);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
