using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using ADSDentalSurgeriesWebAPI.Services;
using ADSDentalSurgeriesWebAPI.Models;
using ADSDentalSurgeriesWebAPI.DTOs;
using ADSDentalSurgeriesWebAPI.Exceptions;

namespace ADSDentalSurgeriesWebAPI.Controllers
{
    /// <summary>
    /// REST API Controller for Patient operations
    /// Role-based access: DENTIST, RECEPTIONIST, and ADMIN roles can access
    /// </summary>
    [ApiController]
    [Route("adsweb/api/v1/patients")]
    [Authorize(Roles = "DENTIST,RECEPTIONIST,ADMIN")] // Require authentication for all endpoints
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// GET: adsweb/api/v1/patients
        /// Displays the list of all Patients, including their primaryAddresses, 
        /// sorted in ascending order by their lastName, in JSON format.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> GetAllPatients()
        {
            try
            {
                var patients = await _patientService.GetAllPatientsAsync();
                var patientResponses = patients.Select(MapToPatientResponse).ToList();
                return Ok(patientResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving patients.", error = ex.Message });
            }
        }

        /// <summary>
        /// GET: adsweb/api/v1/patients/{id}
        /// Displays the data for Patient whose PatientId is {id} including the primaryAddress, 
        /// in JSON format. Includes appropriate exception handling for invalid patientId.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientResponse>> GetPatient(int id)
        {
            try
            {
                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    throw new PatientNotFoundException(id);
                }

                var patientResponse = MapToPatientResponse(patient);
                return Ok(patientResponse);
            }
            catch (PatientNotFoundException)
            {
                return NotFound(new { message = $"Patient with ID {id} was not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the patient.", error = ex.Message });
            }
        }

        /// <summary>
        /// POST: adsweb/api/v1/patients
        /// Register a new Patient into the system.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PatientResponse>> CreatePatient([FromBody] PatientRequest patientRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var patient = new Patient
                {
                    PatientNumber = patientRequest.PatientNumber,
                    FirstName = patientRequest.FirstName,
                    LastName = patientRequest.LastName,
                    PhoneNumber = patientRequest.PhoneNumber,
                    Email = patientRequest.Email,
                    DateOfBirth = patientRequest.DateOfBirth,
                    MailingAddress = patientRequest.MailingAddress,
                    AddressId = patientRequest.AddressId,
                    CreatedDate = DateTime.Now
                };

                var createdPatient = await _patientService.CreatePatientAsync(patient);
                var patientResponse = MapToPatientResponse(createdPatient);
                
                return CreatedAtAction(nameof(GetPatient), new { id = createdPatient.PatientId }, patientResponse);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
            {
                return Conflict(new { message = $"Patient number '{patientRequest.PatientNumber}' already exists. Please use a unique patient number.", error = "Duplicate patient number" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the patient.", error = ex.Message });
            }
        }

        /// <summary>
        /// PUT: adsweb/api/v1/patient/{id}
        /// Retrieves and updates Patient data for the patient whose patientId is {id}.
        /// Includes appropriate exception handling for invalid patientId.
        /// </summary>
        [HttpPut("patient/{id}")]
        public async Task<ActionResult<PatientResponse>> UpdatePatient(int id, [FromBody] PatientRequest patientRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingPatient = await _patientService.GetPatientByIdAsync(id);
                if (existingPatient == null)
                {
                    throw new PatientNotFoundException(id);
                }

                var updatedPatient = new Patient
                {
                    PatientId = id,
                    PatientNumber = patientRequest.PatientNumber,
                    FirstName = patientRequest.FirstName,
                    LastName = patientRequest.LastName,
                    PhoneNumber = patientRequest.PhoneNumber,
                    Email = patientRequest.Email,
                    DateOfBirth = patientRequest.DateOfBirth,
                    MailingAddress = patientRequest.MailingAddress,
                    AddressId = patientRequest.AddressId,
                    CreatedDate = existingPatient.CreatedDate
                };

                var result = await _patientService.UpdatePatientAsync(id, updatedPatient);
                if (result == null)
                {
                    throw new PatientNotFoundException(id);
                }

                var patientResponse = MapToPatientResponse(result);
                return Ok(patientResponse);
            }
            catch (PatientNotFoundException)
            {
                return NotFound(new { message = $"Patient with ID {id} was not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the patient.", error = ex.Message });
            }
        }

        /// <summary>
        /// DELETE: adsweb/api/v1/patient/{id}
        /// Deletes the Patient data for the patient whose patientId is {id}.
        /// </summary>
        [HttpDelete("patient/{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            try
            {
                var existingPatient = await _patientService.GetPatientByIdAsync(id);
                if (existingPatient == null)
                {
                    throw new PatientNotFoundException(id);
                }

                var result = await _patientService.DeletePatientAsync(id);
                if (!result)
                {
                    throw new PatientNotFoundException(id);
                }

                return NoContent();
            }
            catch (PatientNotFoundException)
            {
                return NotFound(new { message = $"Patient with ID {id} was not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the patient.", error = ex.Message });
            }
        }

        /// <summary>
        /// GET: adsweb/api/v1/patient/search/{searchString}
        /// Queries all the Patient data for the patient(s) whose data matches the input searchString.
        /// </summary>
        [HttpGet("patient/search/{searchString}")]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> SearchPatients(string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                {
                    return BadRequest(new { message = "Search string cannot be empty." });
                }

                var patients = await _patientService.SearchPatientsAsync(searchString);
                var patientResponses = patients.Select(MapToPatientResponse).ToList();
                return Ok(patientResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching patients.", error = ex.Message });
            }
        }

        /// <summary>
        /// Maps Patient entity to PatientResponse DTO
        /// </summary>
        private PatientResponse MapToPatientResponse(Patient patient)
        {
            return new PatientResponse
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
                FullName = patient.FullName,
                Address = patient.Address != null ? new AddressSimpleResponse
                {
                    AddressId = patient.Address.AddressId,
                    Street = patient.Address.Street,
                    City = patient.Address.City,
                    State = patient.Address.State,
                    ZipCode = patient.Address.ZipCode,
                    Country = patient.Address.Country,
                    CreatedDate = patient.Address.CreatedDate
                } : null,
                Appointments = patient.Appointments?.Select(a => new AppointmentSimpleResponse
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentNumber = a.AppointmentNumber,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    Notes = a.Notes,
                    Status = a.Status,
                    PatientId = a.PatientId,
                    DentistId = a.DentistId,
                    SurgeryId = a.SurgeryId,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    FormattedDateTime = a.FormattedDateTime
                }).ToList()
            };
        }
    }
}
