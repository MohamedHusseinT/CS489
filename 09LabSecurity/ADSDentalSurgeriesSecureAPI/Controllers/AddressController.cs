using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ADSDentalSurgeriesSecureAPI.Services;
using ADSDentalSurgeriesSecureAPI.Models;
using ADSDentalSurgeriesSecureAPI.DTOs;
using ADSDentalSurgeriesSecureAPI.Exceptions;

namespace ADSDentalSurgeriesSecureAPI.Controllers
{
    /// <summary>
    /// REST API Controller for Address operations
    /// Role-based access: ADMIN and RECEPTIONIST roles can access
    /// </summary>
    [ApiController]
    [Route("adsweb/api/v1/addresses")]
    [Authorize(Roles = "ADMIN,RECEPTIONIST")] // Require authentication for all endpoints
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// GET: adsweb/api/v1/addresses
        /// Displays the list of all Addresses, including the Patient data, 
        /// sorted in ascending order by their city, in JSON format.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressResponse>>> GetAllAddresses()
        {
            try
            {
                var addresses = await _addressService.GetAllAddressesAsync();
                var addressResponses = addresses.Select(MapToAddressResponse).ToList();
                return Ok(addressResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving addresses.", error = ex.Message });
            }
        }

        /// <summary>
        /// GET: adsweb/api/v1/addresses/{id}
        /// Displays the data for Address whose AddressId is {id} including the Patient data.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressResponse>> GetAddress(int id)
        {
            try
            {
                var address = await _addressService.GetAddressByIdAsync(id);
                if (address == null)
                {
                    throw new AddressNotFoundException(id);
                }

                var addressResponse = MapToAddressResponse(address);
                return Ok(addressResponse);
            }
            catch (AddressNotFoundException)
            {
                return NotFound(new { message = $"Address with ID {id} was not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the address.", error = ex.Message });
            }
        }

        /// <summary>
        /// Maps Address entity to AddressResponse DTO
        /// </summary>
        private AddressResponse MapToAddressResponse(Address address)
        {
            return new AddressResponse
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                CreatedDate = address.CreatedDate,
                Patients = address.Patients?.Select(p => new PatientSimpleResponse
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
                    FullName = p.FullName
                }).ToList(),
                Surgeries = address.Surgeries?.Select(s => new SurgerySimpleResponse
                {
                    SurgeryId = s.SurgeryId,
                    SurgeryNumber = s.SurgeryNumber,
                    Name = s.Name,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    AddressId = s.AddressId,
                    CreatedDate = s.CreatedDate
                }).ToList()
            };
        }
    }
}
