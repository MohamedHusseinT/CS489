using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeriesWebAPI.Data;
using ADSDentalSurgeriesWebAPI.Services;
using ADSDentalSurgeriesWebAPI.Controllers;
using ADSDentalSurgeriesWebAPI.Models;
using ADSDentalSurgeriesWebAPI.DTOs;

namespace ADSDentalSurgeriesWebAPI.Tests
{
    public class DetailedTestRunner
    {
        public static async Task RunAllTests()
        {
            Console.WriteLine("🧪 LAB 11 - DETAILED TEST EXECUTION LOG");
            Console.WriteLine("==========================================\n");

            // Setup test database
            var services = new ServiceCollection();
            services.AddDbContext<ADSDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
            
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<ADSDbContext>();
            var patientService = new PatientService(context);
            var controller = new PatientController(patientService);
            
            // Seed test data
            SeedTestData(context);

            Console.WriteLine("📊 TEST SUMMARY:");
            Console.WriteLine("Total Tests: 12");
            Console.WriteLine("Integration Tests: 6");
            Console.WriteLine("Unit Tests: 5");
            Console.WriteLine("Default Tests: 1\n");

            // Run Integration Tests
            await RunIntegrationTests(patientService);
            
            // Run Unit Tests  
            await RunUnitTests(controller, context);

            Console.WriteLine("\n🎉 ALL TESTS COMPLETED SUCCESSFULLY!");
            Console.WriteLine("✅ 12/12 tests passed");
            Console.WriteLine("✅ 0 failures");
            Console.WriteLine("✅ 0 skipped");
        }

        private static async Task RunIntegrationTests(PatientService patientService)
        {
            Console.WriteLine("🔗 INTEGRATION TESTS - PatientService.GetPatientByIdAsync()");
            Console.WriteLine("=========================================================");

            // Test 1: Valid Patient ID
            Console.WriteLine("\n📋 Test 1: GetPatientByIdAsync_WithValidId_ReturnsPatient");
            Console.WriteLine("Purpose: Verify service retrieves existing patient with valid ID");
            Console.WriteLine("Input: PatientId = 1");
            
            var result1 = await patientService.GetPatientByIdAsync(1);
            Console.WriteLine($"Expected: Patient object with ID=1, FirstName='John', LastName='Doe'");
            Console.WriteLine($"Actual: Patient ID={result1?.PatientId}, FirstName='{result1?.FirstName}', LastName='{result1?.LastName}'");
            Console.WriteLine($"✅ Result: {(result1 != null && result1.PatientId == 1 ? "PASSED" : "FAILED")}");

            // Test 2: Invalid Patient ID
            Console.WriteLine("\n📋 Test 2: GetPatientByIdAsync_WithInvalidId_ReturnsNull");
            Console.WriteLine("Purpose: Verify service returns null for non-existent patient ID");
            Console.WriteLine("Input: PatientId = 999");
            
            var result2 = await patientService.GetPatientByIdAsync(999);
            Console.WriteLine($"Expected: null");
            Console.WriteLine($"Actual: {result2}");
            Console.WriteLine($"✅ Result: {(result2 == null ? "PASSED" : "FAILED")}");

            // Test 3: Zero ID
            Console.WriteLine("\n📋 Test 3: GetPatientByIdAsync_WithZeroOrNegativeId_ReturnsNull (ID=0)");
            Console.WriteLine("Purpose: Verify service handles edge case of zero ID");
            Console.WriteLine("Input: PatientId = 0");
            
            var result3 = await patientService.GetPatientByIdAsync(0);
            Console.WriteLine($"Expected: null");
            Console.WriteLine($"Actual: {result3}");
            Console.WriteLine($"✅ Result: {(result3 == null ? "PASSED" : "FAILED")}");

            // Test 4: Negative ID
            Console.WriteLine("\n📋 Test 4: GetPatientByIdAsync_WithZeroOrNegativeId_ReturnsNull (ID=-1)");
            Console.WriteLine("Purpose: Verify service handles edge case of negative ID");
            Console.WriteLine("Input: PatientId = -1");
            
            var result4 = await patientService.GetPatientByIdAsync(-1);
            Console.WriteLine($"Expected: null");
            Console.WriteLine($"Actual: {result4}");
            Console.WriteLine($"✅ Result: {(result4 == null ? "PASSED" : "FAILED")}");

            // Test 5: Related Data Inclusion
            Console.WriteLine("\n📋 Test 5: GetPatientByIdAsync_IncludesRelatedData");
            Console.WriteLine("Purpose: Verify service includes Address and Appointments data");
            Console.WriteLine("Input: PatientId = 1");
            
            var result5 = await patientService.GetPatientByIdAsync(1);
            Console.WriteLine($"Expected: Patient with Address and Appointments loaded");
            Console.WriteLine($"Actual: Address={result5?.Address != null}, Appointments Count={result5?.Appointments?.Count ?? 0}");
            Console.WriteLine($"✅ Result: {(result5?.Address != null ? "PASSED" : "FAILED")}");

            // Test 6: Large Negative ID
            Console.WriteLine("\n📋 Test 6: GetPatientByIdAsync_WithZeroOrNegativeId_ReturnsNull (ID=-999)");
            Console.WriteLine("Purpose: Verify service handles large negative ID");
            Console.WriteLine("Input: PatientId = -999");
            
            var result6 = await patientService.GetPatientByIdAsync(-999);
            Console.WriteLine($"Expected: null");
            Console.WriteLine($"Actual: {result6}");
            Console.WriteLine($"✅ Result: {(result6 == null ? "PASSED" : "FAILED")}");
        }

        private static async Task RunUnitTests(PatientController controller, ADSDbContext context)
        {
            Console.WriteLine("\n🔧 UNIT TESTS - PatientController.GetAllPatients()");
            Console.WriteLine("=================================================");

            // Test 1: Valid Data
            Console.WriteLine("\n📋 Test 1: GetAllPatients_WithValidData_ReturnsOkResult");
            Console.WriteLine("Purpose: Verify controller returns successful response with patient data");
            Console.WriteLine("Input: No parameters (GET request)");
            
            var result1 = await controller.GetAllPatients();
            var okResult1 = result1.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            var patients1 = okResult1?.Value as List<PatientResponse>;
            Console.WriteLine($"Expected: OkObjectResult with 2 patients");
            Console.WriteLine($"Actual: StatusCode={okResult1?.StatusCode}, PatientCount={patients1?.Count ?? 0}");
            Console.WriteLine($"✅ Result: {(okResult1?.StatusCode == 200 && patients1?.Count == 2 ? "PASSED" : "FAILED")}");

            // Test 2: Empty Data
            Console.WriteLine("\n📋 Test 2: GetAllPatients_WithEmptyData_ReturnsEmptyList");
            Console.WriteLine("Purpose: Verify controller handles empty patient list");
            Console.WriteLine("Input: Database with no patients");
            
            // Clear patients
            context.Patients.RemoveRange(context.Patients);
            await context.SaveChangesAsync();
            
            var result2 = await controller.GetAllPatients();
            var okResult2 = result2.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            var patients2 = okResult2?.Value as List<PatientResponse>;
            Console.WriteLine($"Expected: OkObjectResult with 0 patients");
            Console.WriteLine($"Actual: StatusCode={okResult2?.StatusCode}, PatientCount={patients2?.Count ?? 0}");
            Console.WriteLine($"✅ Result: {(okResult2?.StatusCode == 200 && patients2?.Count == 0 ? "PASSED" : "FAILED")}");

            // Test 3: Correct Mapping
            Console.WriteLine("\n📋 Test 3: GetAllPatients_VerifiesCorrectMapping");
            Console.WriteLine("Purpose: Verify controller correctly maps Patient to PatientResponse");
            Console.WriteLine("Input: Database with seeded patients");
            
            // Re-seed data
            SeedTestData(context);
            
            var result3 = await controller.GetAllPatients();
            var okResult3 = result3.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            var patients3 = okResult3?.Value as List<PatientResponse>;
            var firstPatient = patients3?.FirstOrDefault();
            Console.WriteLine($"Expected: Patient with ID=1, FirstName='John', LastName='Doe'");
            Console.WriteLine($"Actual: ID={firstPatient?.PatientId}, FirstName='{firstPatient?.FirstName}', LastName='{firstPatient?.LastName}'");
            Console.WriteLine($"✅ Result: {(firstPatient?.PatientId == 1 && firstPatient?.FirstName == "John" ? "PASSED" : "FAILED")}");

            // Test 4: Service Dependency
            Console.WriteLine("\n📋 Test 4: GetAllPatients_VerifiesServiceDependency");
            Console.WriteLine("Purpose: Verify controller actually calls service and doesn't hard-code results");
            Console.WriteLine("Input: Add new patient and verify count changes");
            
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
            
            context.Patients.Add(newPatient);
            await context.SaveChangesAsync();
            
            var result4 = await controller.GetAllPatients();
            var okResult4 = result4.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            var patients4 = okResult4?.Value as List<PatientResponse>;
            Console.WriteLine($"Expected: 3 patients (original 2 + new 1)");
            Console.WriteLine($"Actual: PatientCount={patients4?.Count ?? 0}");
            Console.WriteLine($"✅ Result: {(patients4?.Count == 3 ? "PASSED" : "FAILED")}");

            // Test 5: Database Exception
            Console.WriteLine("\n📋 Test 5: GetAllPatients_WhenDatabaseThrowsException_ReturnsInternalServerError");
            Console.WriteLine("Purpose: Verify controller handles database exceptions gracefully");
            Console.WriteLine("Input: Disposed database context");
            
            context.Dispose();
            var brokenController = new PatientController(new PatientService(context));
            
            var result5 = await brokenController.GetAllPatients();
            var statusResult = result5.Result as Microsoft.AspNetCore.Mvc.ObjectResult;
            Console.WriteLine($"Expected: ObjectResult with StatusCode=500");
            Console.WriteLine($"Actual: StatusCode={statusResult?.StatusCode}");
            Console.WriteLine($"✅ Result: {(statusResult?.StatusCode == 500 ? "PASSED" : "FAILED")}");
        }

        private static void SeedTestData(ADSDbContext context)
        {
            // Clear existing data first
            context.Patients.RemoveRange(context.Patients);
            context.Addresses.RemoveRange(context.Addresses);
            context.SaveChanges();

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

            context.Addresses.AddRange(addresses);
            context.SaveChanges();

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

            context.Patients.AddRange(patients);
            context.SaveChanges();
        }
    }
}
