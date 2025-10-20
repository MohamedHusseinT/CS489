using Microsoft.AspNetCore.Mvc;
using PropertyLeasingAPI.DTOs;
using PropertyLeasingAPI.Services;

namespace PropertyLeasingAPI.Controllers
{
    [ApiController]
    [Route("propmgmt/api/properties")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Get all properties for a given state, sorted by property reference name
        /// </summary>
        /// <param name="stateCode">State code (e.g., CO, TX)</param>
        /// <returns>List of properties with their leases</returns>
        [HttpGet("{stateCode}")]
        public async Task<ActionResult<List<PropertyResponse>>> GetPropertiesByState(string stateCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stateCode))
                {
                    return BadRequest("State code is required.");
                }

                var properties = await _propertyService.GetPropertiesByStateAsync(stateCode);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}



