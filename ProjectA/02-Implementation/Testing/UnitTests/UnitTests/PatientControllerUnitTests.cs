using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ADSDentalSurgeriesWebAPI.Controllers;
using ADSDentalSurgeriesWebAPI.Services;
using ADSDentalSurgeriesWebAPI.Models;
using ADSDentalSurgeriesWebAPI.Data;
using ADSDentalSurgeriesWebAPI.DTOs;

namespace ADSDentalSurgeriesWebAPI.Tests.UnitTests
{
    /// <summary>
    /// Unit tests for PatientController.GetAllPatients endpoint.
    /// These tests verify the controller behavior with a test database.
    /// </summary>
    public class PatientControllerUnitTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ADSDbContext _context;
        private readonly PatientService _patientService;
        private readonly PatientController _controller;

        public PatientControllerUnitTests()
        {
            // Setup in-memory database for testing
            var services = new ServiceCollection();
            services.AddDbContext<ADSDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
            
            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetRequiredService<ADSDbContext>();
            _patientService = new PatientService(_context);
            _controller = new PatientController(_patientService);
            
            // Seed test data
            SeedTestData();
        }

        /// <summary>
        /// Unit test: GetAllPatients returns successful response with patient data.
        /// Verifies that the controller correctly calls the service and returns proper response.
        /// </summary>
        [Fact]
        public async Task GetAllPatients_WithValidData_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetAllPatients();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<PatientResponse>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
            var patientResponses = Assert.IsAssignableFrom<List<PatientResponse>>(okObjectResult.Value);
            
            Assert.Equal(2, patientResponses.Count);
            Assert.Equal("P001", patientResponses[0].PatientNumber);
            Assert.Equal("P002", patientResponses[1].PatientNumber);
        }

        /// <summary>
        /// Unit test: GetAllPatients returns empty list when no patients exist.
        /// Verifies controller behavior with empty data.
        /// </summary>
        [Fact]
        public async Task GetAllPatients_WithEmptyData_ReturnsEmptyList()
        {
            // Arrange - Clear all patients
            _context.Patients.RemoveRange(_context.Patients);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAllPatients();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<PatientResponse>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
            var patientResponses = Assert.IsAssignableFrom<List<PatientResponse>>(okObjectResult.Value);
            
            Assert.Empty(patientResponses);
        }

        /// <summary>
        /// Unit test: GetAllPatients verifies correct mapping from Patient to PatientResponse.
        /// Ensures that the controller correctly maps entity data to DTO.
        /// </summary>
        [Fact]
        public async Task GetAllPatients_VerifiesCorrectMapping()
        {
            // Act
            var result = await _controller.GetAllPatients();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<PatientResponse>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
            var patientResponses = Assert.IsAssignableFrom<List<PatientResponse>>(okObjectResult.Value);
            
            var firstPatient = patientResponses.First();
            
            // Verify mapping of basic properties
            Assert.Equal(1, firstPatient.PatientId);
            Assert.Equal("P001", firstPatient.PatientNumber);
            Assert.Equal("John", firstPatient.FirstName);
            Assert.Equal("Doe", firstPatient.LastName);
            Assert.Equal("John Doe", firstPatient.FullName);
            
            // Verify address mapping
            Assert.NotNull(firstPatient.Address);
            Assert.Equal(1, firstPatient.Address.AddressId);
            Assert.Equal("123 Main St", firstPatient.Address.Street);
            Assert.Equal("Anytown", firstPatient.Address.City);
        }

        /// <summary>
        /// Unit test: GetAllPatients verifies service dependency.
        /// Ensures that the controller actually calls the service and doesn't hard-code results.
        /// </summary>
        [Fact]
        public async Task GetAllPatients_VerifiesServiceDependency()
        {
            // Act - First call
            var firstResult = await _controller.GetAllPatients();
            
            // Add another patient
            var newPatient = new Patient
            {
                PatientId = 3,
                PatientNumber = "P003",
                FirstName = "Bob",
                LastName = "Johnson",
                PhoneNumber = "555-9999",
                Email = "bob.johnson@email.com",
                DateOfBirth = new DateTime(1978, 12, 3),
                MailingAddress = "789 Pine St, Elsewhere, TX 54321",
                AddressId = 1,
                CreatedDate = DateTime.Now
            };
            
            _context.Patients.Add(newPatient);
            await _context.SaveChangesAsync();
            
            // Act - Second call
            var secondResult = await _controller.GetAllPatients();

            // Assert
            var firstOkResult = Assert.IsType<ActionResult<IEnumerable<PatientResponse>>>(firstResult);
            var firstOkObjectResult = Assert.IsType<OkObjectResult>(firstOkResult.Result);
            var firstPatientResponses = Assert.IsAssignableFrom<List<PatientResponse>>(firstOkObjectResult.Value);
            
            var secondOkResult = Assert.IsType<ActionResult<IEnumerable<PatientResponse>>>(secondResult);
            var secondOkObjectResult = Assert.IsType<OkObjectResult>(secondOkResult.Result);
            var secondPatientResponses = Assert.IsAssignableFrom<List<PatientResponse>>(secondOkObjectResult.Value);
            
            // Verify different results (proving service dependency)
            Assert.NotEqual(firstPatientResponses.Count, secondPatientResponses.Count);
            Assert.Equal(2, firstPatientResponses.Count);
            Assert.Equal(3, secondPatientResponses.Count);
            Assert.Equal("P001", firstPatientResponses.First().PatientNumber);
            Assert.Contains(secondPatientResponses, p => p.PatientNumber == "P003");
        }

        /// <summary>
        /// Unit test: GetAllPatients handles database exceptions gracefully.
        /// Verifies that controller returns proper error response when database throws exception.
        /// </summary>
        [Fact]
        public async Task GetAllPatients_WhenDatabaseThrowsException_ReturnsInternalServerError()
        {
            // Arrange - Dispose context to simulate database error
            _context.Dispose();
            var brokenController = new PatientController(new PatientService(_context));

            // Act
            var result = await brokenController.GetAllPatients();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<PatientResponse>>>(result);
            var statusCodeResult = Assert.IsType<ObjectResult>(okResult.Result);
            
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        /// <summary>
        /// Seeds test data into the in-memory database for unit testing.
        /// </summary>
        private void SeedTestData()
        {
            // Add test addresses
            var addresses = new List<Address>
            {
                new Address
                {
                    AddressId = 1,
                    Street = "123 Main St",
                    City = "Anytown",
                    State = "CA",
                    ZipCode = "12345",
                    Country = "USA",
                    CreatedDate = DateTime.Now
                },
                new Address
                {
                    AddressId = 2,
                    Street = "456 Oak Ave",
                    City = "Somewhere",
                    State = "NY",
                    ZipCode = "67890",
                    Country = "USA",
                    CreatedDate = DateTime.Now
                }
            };

            _context.Addresses.AddRange(addresses);

            // Add test patients
            var patients = new List<Patient>
            {
                new Patient
                {
                    PatientId = 1,
                    PatientNumber = "P001",
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "555-1234",
                    Email = "john.doe@email.com",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    MailingAddress = "123 Main St, Anytown, CA 12345",
                    AddressId = 1,
                    CreatedDate = DateTime.Now
                },
                new Patient
                {
                    PatientId = 2,
                    PatientNumber = "P002",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "555-5678",
                    Email = "jane.smith@email.com",
                    DateOfBirth = new DateTime(1990, 8, 22),
                    MailingAddress = "456 Oak Ave, Somewhere, NY 67890",
                    AddressId = 2,
                    CreatedDate = DateTime.Now
                }
            };

            _context.Patients.AddRange(patients);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            _serviceProvider.Dispose();
        }
    }
}