using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using PAMSApp.Model;

namespace PAMSApp
{
    public class PAMSApp
    {
        private static List<Patient> _patients;

        public static void Main(string[] args)
        {
            Console.WriteLine("=== Patient Appointment Management System (PAMS) ===");
            Console.WriteLine();

            // Load patient data
            LoadPatientData();

            // Display loaded patients
            Console.WriteLine($"Loaded {_patients.Count} patients:");
            foreach (var patient in _patients)
            {
                Console.WriteLine($"- {patient.GetFullName()} (Age: {patient.GetCurrentAge()})");
            }
            Console.WriteLine();

            // Convert to JSON and write to file
            ConvertToJsonAndWriteToFile();

            Console.WriteLine("PAMS application completed successfully!");
        }

        private static void LoadPatientData()
        {
            _patients = new List<Patient>
            {
                // Patient 1: Daniel Agar
                new Patient(1, "Daniel", "Agar", "(641) 123-0009", "dagar@m.as", "1 N Street", new DateTime(1987, 1, 19)),

                // Patient 2: Ana Smith
                new Patient(2, "Ana", "Smith", "", "amsith@te.edu", "", new DateTime(1948, 12, 5)),

                // Patient 3: Marcus Garvey
                new Patient(3, "Marcus", "Garvey", "(123) 292-0018", "", "4 East Ave", new DateTime(2001, 9, 18)),

                // Patient 4: Jeff Goldbloom
                new Patient(4, "Jeff", "Goldbloom", "(999) 165-1192", "jgold@es.co.za", "", new DateTime(1995, 2, 28)),

                // Patient 5: Mary Washington
                new Patient(5, "Mary", "Washington", "", "", "30 W Burlington", new DateTime(1932, 5, 31))
            };
        }

        private static void ConvertToJsonAndWriteToFile()
        {
            try
            {
                // Sort patients by age in descending order (oldest first)
                var sortedPatients = _patients
                    .OrderByDescending(p => p.GetCurrentAge())
                    .ToList();

                Console.WriteLine("Patients sorted by age (descending - oldest first):");
                foreach (var patient in sortedPatients)
                {
                    Console.WriteLine($"- {patient.GetFullName()}: {patient.GetCurrentAge()} years old");
                }
                Console.WriteLine();

                // Create JSON data with age included
                var patientsWithAge = sortedPatients.Select(p => new
                {
                    patientId = p.PatientId,
                    firstName = p.FirstName,
                    lastName = p.LastName,
                    phoneNumber = p.PhoneNumber,
                    email = p.Email,
                    mailingAddress = p.MailingAddress,
                    dateOfBirth = p.DateOfBirth.ToString("yyyy-MM-dd"),
                    age = p.GetCurrentAge()
                }).ToList();

                // Serialize to JSON with pretty formatting
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                string jsonString = JsonSerializer.Serialize(patientsWithAge, jsonOptions);

                // Write to file
                string fileName = "patients_data.json";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"✅ Successfully wrote patient data to: {filePath}");
                Console.WriteLine($"📄 File contains {patientsWithAge.Count} patients sorted by age (descending)");
                Console.WriteLine();

                // Display a preview of the JSON content
                Console.WriteLine("📋 JSON Preview (first 500 characters):");
                Console.WriteLine(new string('=', 50));
                string preview = jsonString.Length > 500 ? jsonString.Substring(0, 500) + "..." : jsonString;
                Console.WriteLine(preview);
                Console.WriteLine(new string('=', 50));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error writing JSON file: {ex.Message}");
            }
        }

        // Method to get patients sorted by age (for external access)
        public static List<Patient> GetPatientsSortedByAge()
        {
            return _patients?.OrderByDescending(p => p.GetCurrentAge()).ToList() ?? new List<Patient>();
        }

        // Method to get patient count
        public static int GetPatientCount()
        {
            return _patients?.Count ?? 0;
        }
    }
}
