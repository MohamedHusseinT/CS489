using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ADSDentalSurgeriesWebAPI.Data;
using ADSDentalSurgeriesWebAPI.Services;
using ADSDentalSurgeriesWebAPI.Models;

namespace ADSDentalSurgeriesWebAPI.Tests.IntegrationTests
{
    /// <summary>
    /// Integration tests for PatientService.GetPatientByIdAsync method.
    /// These tests verify the service works correctly with a real database.
    /// </summary>
    public class PatientServiceIntegrationTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ADSDbContext _context;
        private readonly PatientService _patientService;

        public PatientServiceIntegrationTests()
        {
            // Setup in-memory database for testing
            var services = new ServiceCollection();
            services.AddDbContext<ADSDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
            
            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetRequiredService<ADSDbContext>();
            _patientService = new PatientService(_context);
            
            // Seed test data
            SeedTestData();
        }

        /// <summary>
        /// Integration test: When patientId exists and is found.
        /// Verifies that the service correctly retrieves a patient with valid ID.
        /// </summary>
        [Fact]
        public async Task GetPatientByIdAsync_WithValidId_ReturnsPatient()
        {
            // Arrange
            int validPatientId = 1;
            var expectedPatient = await _context.Patients.FindAsync(validPatientId);

            // Act
            var result = await _patientService.GetPatientByIdAsync(validPatientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(validPatientId, result.PatientId);
            Assert.Equal("P001", result.PatientNumber);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.NotNull(result.Address);
            Assert.Equal(1, result.Address.AddressId);
        }

        /// <summary>
        /// Integration test: When patientId is invalid and not found.
        /// Verifies that the service returns null for non-existent patient ID.
        /// </summary>
        [Fact]
        public async Task GetPatientByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int invalidPatientId = 999; // Non-existent ID

            // Act
            var result = await _patientService.GetPatientByIdAsync(invalidPatientId);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Integration test: When patientId is zero or negative.
        /// Verifies that the service handles edge cases correctly.
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public async Task GetPatientByIdAsync_WithZeroOrNegativeId_ReturnsNull(int invalidId)
        {
            // Act
            var result = await _patientService.GetPatientByIdAsync(invalidId);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Integration test: Verifies that patient includes related data (Address and Appointments).
        /// This test ensures the Include statements work correctly.
        /// </summary>
        [Fact]
        public async Task GetPatientByIdAsync_IncludesRelatedData()
        {
            // Arrange
            int patientIdWithAppointments = 1;

            // Act
            var result = await _patientService.GetPatientByIdAsync(patientIdWithAppointments);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Address);
            Assert.Equal("123 Main St", result.Address.Street);
            Assert.Equal("Anytown", result.Address.City);
            
            // Verify appointments are included
            Assert.NotNull(result.Appointments);
            Assert.True(result.Appointments.Count > 0);
            
            // Verify appointment includes dentist and surgery data
            var firstAppointment = result.Appointments.First();
            Assert.NotNull(firstAppointment.Dentist);
            Assert.NotNull(firstAppointment.Surgery);
        }

        /// <summary>
        /// Seeds test data into the in-memory database for integration testing.
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

            // Add test dentists
            var dentists = new List<Dentist>
            {
                new Dentist
                {
                    DentistId = 1,
                    DentistNumber = "D001",
                    FirstName = "Dr. Smith",
                    LastName = "Johnson",
                    Specialization = "General Dentistry",
                    PhoneNumber = "555-0101",
                    Email = "dr.smith@clinic.com",
                    CreatedDate = DateTime.Now
                }
            };

            _context.Dentists.AddRange(dentists);

            // Add test surgeries
            var surgeries = new List<Surgery>
            {
                new Surgery
                {
                    SurgeryId = 1,
                    SurgeryNumber = "S001",
                    Name = "Room A",
                    PhoneNumber = "555-0201",
                    Email = "rooma@clinic.com",
                    AddressId = 1,
                    CreatedDate = DateTime.Now
                }
            };

            _context.Surgeries.AddRange(surgeries);

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

            // Add test appointments
            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    AppointmentId = 1,
                    AppointmentNumber = "APT001",
                    AppointmentDate = DateTime.Now.AddDays(7),
                    AppointmentTime = new TimeSpan(10, 0, 0),
                    Notes = "Regular checkup",
                    Status = "Scheduled",
                    PatientId = 1,
                    DentistId = 1,
                    SurgeryId = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };

            _context.Appointments.AddRange(appointments);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            _serviceProvider.Dispose();
        }
    }
}
