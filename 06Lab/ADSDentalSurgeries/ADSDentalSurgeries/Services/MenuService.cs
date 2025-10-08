using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeries.Data;
using ADSDentalSurgeries.Models;
using ADSDentalSurgeries.Services;

namespace ADSDentalSurgeries.Services
{
    /// <summary>
    /// Service class for CLI menu operations
    /// </summary>
    public class MenuService
    {
        private readonly PatientService _patientService;
        private readonly DentistService _dentistService;
        private readonly AppointmentService _appointmentService;
        private readonly SurgeryService _surgeryService;

        public MenuService(PatientService patientService, DentistService dentistService, 
                          AppointmentService appointmentService, SurgeryService surgeryService)
        {
            _patientService = patientService;
            _dentistService = dentistService;
            _appointmentService = appointmentService;
            _surgeryService = surgeryService;
        }

        public async Task RunMainMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("🏥 ADS Dental Surgeries - Appointment Management System");
                Console.WriteLine("Lab 6 - CS489 Applied Software Development");
                Console.WriteLine("Student: Mohamed");
                Console.WriteLine(new string('=', 70));
                Console.WriteLine();

                Console.WriteLine("Please select an option:");
                Console.WriteLine();
                Console.WriteLine("1. 👥 Patient Management");
                Console.WriteLine("2. 🦷 Dentist Management");
                Console.WriteLine("3. 🏥 Surgery Management");
                Console.WriteLine("4. 📅 Appointment Management");
                Console.WriteLine("5. 📊 Reports & Queries");
                Console.WriteLine("6. 🔍 Search Functions");
                Console.WriteLine("7. ❌ Exit");
                Console.WriteLine();

                Console.Write("Enter your choice (1-7): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await RunPatientMenuAsync();
                        break;
                    case "2":
                        await RunDentistMenuAsync();
                        break;
                    case "3":
                        await RunSurgeryMenuAsync();
                        break;
                    case "4":
                        await RunAppointmentMenuAsync();
                        break;
                    case "5":
                        await RunReportsMenuAsync();
                        break;
                    case "6":
                        await RunSearchMenuAsync();
                        break;
                    case "7":
                        Console.WriteLine("Thank you for using ADS Dental Surgeries System!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        await WaitForUserInput();
                        break;
                }
            }
        }

        private async Task RunPatientMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("👥 PATIENT MANAGEMENT");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine();

                Console.WriteLine("1. 📋 List All Patients");
                Console.WriteLine("2. 🔍 View Patient Details");
                Console.WriteLine("3. ➕ Add New Patient");
                Console.WriteLine("4. ✏️  Update Patient");
                Console.WriteLine("5. 🗑️  Delete Patient");
                Console.WriteLine("6. 📅 View Patient Appointments");
                Console.WriteLine("7. ⬅️  Back to Main Menu");
                Console.WriteLine();

                Console.Write("Enter your choice (1-7): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllPatientsAsync();
                        break;
                    case "2":
                        await ViewPatientDetailsAsync();
                        break;
                    case "3":
                        await AddNewPatientAsync();
                        break;
                    case "4":
                        await UpdatePatientAsync();
                        break;
                    case "5":
                        await DeletePatientAsync();
                        break;
                    case "6":
                        await ViewPatientAppointmentsAsync();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        await WaitForUserInput();
                        break;
                }
            }
        }

        private async Task RunDentistMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("🦷 DENTIST MANAGEMENT");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine();

                Console.WriteLine("1. 📋 List All Dentists");
                Console.WriteLine("2. 🔍 View Dentist Details");
                Console.WriteLine("3. ➕ Add New Dentist");
                Console.WriteLine("4. ✏️  Update Dentist");
                Console.WriteLine("5. 🗑️  Delete Dentist");
                Console.WriteLine("6. 📅 View Dentist Appointments");
                Console.WriteLine("7. ⬅️  Back to Main Menu");
                Console.WriteLine();

                Console.Write("Enter your choice (1-7): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllDentistsAsync();
                        break;
                    case "2":
                        await ViewDentistDetailsAsync();
                        break;
                    case "3":
                        await AddNewDentistAsync();
                        break;
                    case "4":
                        await UpdateDentistAsync();
                        break;
                    case "5":
                        await DeleteDentistAsync();
                        break;
                    case "6":
                        await ViewDentistAppointmentsAsync();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        await WaitForUserInput();
                        break;
                }
            }
        }

        private async Task RunSurgeryMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("🏥 SURGERY MANAGEMENT");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine();

                Console.WriteLine("1. 📋 List All Surgeries");
                Console.WriteLine("2. 🔍 View Surgery Details");
                Console.WriteLine("3. ➕ Add New Surgery");
                Console.WriteLine("4. ✏️  Update Surgery");
                Console.WriteLine("5. 🗑️  Delete Surgery");
                Console.WriteLine("6. 📅 View Surgery Appointments");
                Console.WriteLine("7. ⬅️  Back to Main Menu");
                Console.WriteLine();

                Console.Write("Enter your choice (1-7): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllSurgeriesAsync();
                        break;
                    case "2":
                        await ViewSurgeryDetailsAsync();
                        break;
                    case "3":
                        await AddNewSurgeryAsync();
                        break;
                    case "4":
                        await UpdateSurgeryAsync();
                        break;
                    case "5":
                        await DeleteSurgeryAsync();
                        break;
                    case "6":
                        await ViewSurgeryAppointmentsAsync();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        await WaitForUserInput();
                        break;
                }
            }
        }

        private async Task RunAppointmentMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("📅 APPOINTMENT MANAGEMENT");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine();

                Console.WriteLine("1. 📋 List All Appointments");
                Console.WriteLine("2. 🔍 View Appointment Details");
                Console.WriteLine("3. ➕ Add New Appointment");
                Console.WriteLine("4. ✏️  Update Appointment");
                Console.WriteLine("5. 🗑️  Delete Appointment");
                Console.WriteLine("6. 📅 View Appointments by Date");
                Console.WriteLine("7. 📊 View Appointments by Status");
                Console.WriteLine("8. ⬅️  Back to Main Menu");
                Console.WriteLine();

                Console.Write("Enter your choice (1-8): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllAppointmentsAsync();
                        break;
                    case "2":
                        await ViewAppointmentDetailsAsync();
                        break;
                    case "3":
                        await AddNewAppointmentAsync();
                        break;
                    case "4":
                        await UpdateAppointmentAsync();
                        break;
                    case "5":
                        await DeleteAppointmentAsync();
                        break;
                    case "6":
                        await ViewAppointmentsByDateAsync();
                        break;
                    case "7":
                        await ViewAppointmentsByStatusAsync();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        await WaitForUserInput();
                        break;
                }
            }
        }

        private async Task RunReportsMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("📊 REPORTS & QUERIES");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine();

                Console.WriteLine("1. 📋 All Patients with Appointments");
                Console.WriteLine("2. 🦷 All Dentists with Appointments");
                Console.WriteLine("3. 🏥 All Surgeries with Appointments");
                Console.WriteLine("4. 📅 Appointments for Specific Dentist");
                Console.WriteLine("5. 📅 Appointments for Specific Patient");
                Console.WriteLine("6. 📅 Appointments for Specific Surgery");
                Console.WriteLine("7. 📊 System Statistics");
                Console.WriteLine("8. ⬅️  Back to Main Menu");
                Console.WriteLine();

                Console.Write("Enter your choice (1-8): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ShowPatientsWithAppointmentsAsync();
                        break;
                    case "2":
                        await ShowDentistsWithAppointmentsAsync();
                        break;
                    case "3":
                        await ShowSurgeriesWithAppointmentsAsync();
                        break;
                    case "4":
                        await ShowAppointmentsForDentistAsync();
                        break;
                    case "5":
                        await ShowAppointmentsForPatientAsync();
                        break;
                    case "6":
                        await ShowAppointmentsForSurgeryAsync();
                        break;
                    case "7":
                        await ShowSystemStatisticsAsync();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        await WaitForUserInput();
                        break;
                }
            }
        }

        private async Task RunSearchMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("🔍 SEARCH FUNCTIONS");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine();

                Console.WriteLine("1. 🔍 Search Patients");
                Console.WriteLine("2. 🔍 Search Dentists");
                Console.WriteLine("3. 🔍 Search Surgeries");
                Console.WriteLine("4. ⬅️  Back to Main Menu");
                Console.WriteLine();

                Console.Write("Enter your choice (1-4): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await SearchPatientsAsync();
                        break;
                    case "2":
                        await SearchDentistsAsync();
                        break;
                    case "3":
                        await SearchSurgeriesAsync();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        await WaitForUserInput();
                        break;
                }
            }
        }

        // Patient operations
        private async Task ListAllPatientsAsync()
        {
            Console.Clear();
            Console.WriteLine("📋 ALL PATIENTS");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine();

            var patients = await _patientService.GetAllPatientsAsync();
            
            if (!patients.Any())
            {
                Console.WriteLine("No patients found.");
            }
            else
            {
                Console.WriteLine($"Found {patients.Count} patients:");
                Console.WriteLine();
                
                foreach (var patient in patients)
                {
                    Console.WriteLine($"ID: {patient.PatientId}");
                    Console.WriteLine($"Patient Number: {patient.PatientNumber}");
                    Console.WriteLine($"Name: {patient.FullName}");
                    Console.WriteLine($"Phone: {patient.PhoneNumber ?? "N/A"}");
                    Console.WriteLine($"Email: {patient.Email ?? "N/A"}");
                    Console.WriteLine($"Address: {patient.Address?.ToString() ?? "N/A"}");
                    Console.WriteLine($"Appointments: {patient.Appointments?.Count ?? 0}");
                    Console.WriteLine(new string('-', 50));
                }
            }

            await WaitForUserInput();
        }

        private async Task ViewPatientDetailsAsync()
        {
            Console.Clear();
            Console.WriteLine("🔍 VIEW PATIENT DETAILS");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine();

            Console.Write("Enter Patient ID: ");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                var patient = await _patientService.GetPatientByIdAsync(patientId);
                
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                }
                else
                {
                    Console.WriteLine($"Patient ID: {patient.PatientId}");
                    Console.WriteLine($"Patient Number: {patient.PatientNumber}");
                    Console.WriteLine($"Name: {patient.FullName}");
                    Console.WriteLine($"Phone: {patient.PhoneNumber ?? "N/A"}");
                    Console.WriteLine($"Email: {patient.Email ?? "N/A"}");
                    Console.WriteLine($"Date of Birth: {patient.DateOfBirth?.ToString("dd-MMM-yyyy") ?? "N/A"}");
                    Console.WriteLine($"Mailing Address: {patient.MailingAddress ?? "N/A"}");
                    Console.WriteLine($"Address: {patient.Address?.ToString() ?? "N/A"}");
                    Console.WriteLine($"Created: {patient.CreatedDate:dd-MMM-yyyy HH:mm}");
                    Console.WriteLine();

                    if (patient.Appointments?.Any() == true)
                    {
                        Console.WriteLine("Appointments:");
                        foreach (var appointment in patient.Appointments.OrderBy(a => a.AppointmentDate))
                        {
                            Console.WriteLine($"  - {appointment.FormattedDateTime} with {appointment.Dentist?.FullName} at {appointment.Surgery?.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No appointments found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Patient ID.");
            }

            await WaitForUserInput();
        }

        // Similar methods for other operations...
        // For brevity, I'll implement a few key methods and the WaitForUserInput method

        private async Task WaitForUserInput()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Placeholder methods for other operations
        private async Task AddNewPatientAsync() { Console.WriteLine("Add New Patient - Not implemented yet"); await WaitForUserInput(); }
        private async Task UpdatePatientAsync() { Console.WriteLine("Update Patient - Not implemented yet"); await WaitForUserInput(); }
        private async Task DeletePatientAsync() { Console.WriteLine("Delete Patient - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewPatientAppointmentsAsync() { Console.WriteLine("View Patient Appointments - Not implemented yet"); await WaitForUserInput(); }

        private async Task ListAllDentistsAsync() { Console.WriteLine("List All Dentists - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewDentistDetailsAsync() { Console.WriteLine("View Dentist Details - Not implemented yet"); await WaitForUserInput(); }
        private async Task AddNewDentistAsync() { Console.WriteLine("Add New Dentist - Not implemented yet"); await WaitForUserInput(); }
        private async Task UpdateDentistAsync() { Console.WriteLine("Update Dentist - Not implemented yet"); await WaitForUserInput(); }
        private async Task DeleteDentistAsync() { Console.WriteLine("Delete Dentist - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewDentistAppointmentsAsync() { Console.WriteLine("View Dentist Appointments - Not implemented yet"); await WaitForUserInput(); }

        private async Task ListAllSurgeriesAsync() { Console.WriteLine("List All Surgeries - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewSurgeryDetailsAsync() { Console.WriteLine("View Surgery Details - Not implemented yet"); await WaitForUserInput(); }
        private async Task AddNewSurgeryAsync() { Console.WriteLine("Add New Surgery - Not implemented yet"); await WaitForUserInput(); }
        private async Task UpdateSurgeryAsync() { Console.WriteLine("Update Surgery - Not implemented yet"); await WaitForUserInput(); }
        private async Task DeleteSurgeryAsync() { Console.WriteLine("Delete Surgery - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewSurgeryAppointmentsAsync() { Console.WriteLine("View Surgery Appointments - Not implemented yet"); await WaitForUserInput(); }

        private async Task ListAllAppointmentsAsync() { Console.WriteLine("List All Appointments - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewAppointmentDetailsAsync() { Console.WriteLine("View Appointment Details - Not implemented yet"); await WaitForUserInput(); }
        private async Task AddNewAppointmentAsync() { Console.WriteLine("Add New Appointment - Not implemented yet"); await WaitForUserInput(); }
        private async Task UpdateAppointmentAsync() { Console.WriteLine("Update Appointment - Not implemented yet"); await WaitForUserInput(); }
        private async Task DeleteAppointmentAsync() { Console.WriteLine("Delete Appointment - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewAppointmentsByDateAsync() { Console.WriteLine("View Appointments by Date - Not implemented yet"); await WaitForUserInput(); }
        private async Task ViewAppointmentsByStatusAsync() { Console.WriteLine("View Appointments by Status - Not implemented yet"); await WaitForUserInput(); }

        private async Task ShowPatientsWithAppointmentsAsync() { Console.WriteLine("Show Patients with Appointments - Not implemented yet"); await WaitForUserInput(); }
        private async Task ShowDentistsWithAppointmentsAsync() { Console.WriteLine("Show Dentists with Appointments - Not implemented yet"); await WaitForUserInput(); }
        private async Task ShowSurgeriesWithAppointmentsAsync() { Console.WriteLine("Show Surgeries with Appointments - Not implemented yet"); await WaitForUserInput(); }
        private async Task ShowAppointmentsForDentistAsync() { Console.WriteLine("Show Appointments for Dentist - Not implemented yet"); await WaitForUserInput(); }
        private async Task ShowAppointmentsForPatientAsync() { Console.WriteLine("Show Appointments for Patient - Not implemented yet"); await WaitForUserInput(); }
        private async Task ShowAppointmentsForSurgeryAsync() { Console.WriteLine("Show Appointments for Surgery - Not implemented yet"); await WaitForUserInput(); }
        private async Task ShowSystemStatisticsAsync() { Console.WriteLine("Show System Statistics - Not implemented yet"); await WaitForUserInput(); }

        private async Task SearchPatientsAsync() { Console.WriteLine("Search Patients - Not implemented yet"); await WaitForUserInput(); }
        private async Task SearchDentistsAsync() { Console.WriteLine("Search Dentists - Not implemented yet"); await WaitForUserInput(); }
        private async Task SearchSurgeriesAsync() { Console.WriteLine("Search Surgeries - Not implemented yet"); await WaitForUserInput(); }
    }
}
