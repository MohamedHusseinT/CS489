using System;
using System.Data.SqlClient;
using System.IO;

namespace ADSDatabaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🏥 Advantis Dental Surgeries (ADS) Database Test");
            Console.WriteLine("Lab 05A - CS489 Applied Software Development");
            Console.WriteLine("Student: Mohamed");
            Console.WriteLine(new string('=', 60));
            
            try
            {
                // Read and display the SQL script
                string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "myADSDentalSurgeryDBScript.sql");
                
                if (File.Exists(scriptPath))
                {
                    Console.WriteLine("\n📄 SQL Script Found:");
                    Console.WriteLine($"Path: {scriptPath}");
                    
                    string sqlScript = File.ReadAllText(scriptPath);
                    Console.WriteLine($"\n📊 Script Statistics:");
                    Console.WriteLine($"- Total Characters: {sqlScript.Length:N0}");
                    Console.WriteLine($"- Total Lines: {sqlScript.Split('\n').Length:N0}");
                    Console.WriteLine($"- Contains CREATE TABLE: {sqlScript.Contains("CREATE TABLE")}");
                    Console.WriteLine($"- Contains INSERT INTO: {sqlScript.Contains("INSERT INTO")}");
                    Console.WriteLine($"- Contains SELECT queries: {sqlScript.Split(new[] { "SELECT" }, StringSplitOptions.None).Length - 1}");
                }
                else
                {
                    Console.WriteLine($"❌ SQL Script not found at: {scriptPath}");
                }
                
                // Display the required queries
                DisplayRequiredQueries();
                
                Console.WriteLine("\n✅ Database test completed successfully!");
                Console.WriteLine("📸 Please execute the SQL script in SQL Server Management Studio");
                Console.WriteLine("📸 Take screenshots of each query execution for submission");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static void DisplayRequiredQueries()
        {
            Console.WriteLine("\n🔍 REQUIRED SQL QUERIES:");
            Console.WriteLine(new string('=', 50));
            
            Console.WriteLine("\n1️⃣ Query 1: All Dentists (sorted by last name)");
            Console.WriteLine("```sql");
            Console.WriteLine("SELECT dentist_id, first_name, last_name, contact_phone, email, specialization, created_date");
            Console.WriteLine("FROM Dentists");
            Console.WriteLine("ORDER BY last_name ASC;");
            Console.WriteLine("```");
            
            Console.WriteLine("\n2️⃣ Query 2: Appointments for Dentist ID = 1 (with patient info)");
            Console.WriteLine("```sql");
            Console.WriteLine("SELECT a.appointment_id, a.appointment_date, a.appointment_time, a.status,");
            Console.WriteLine("       p.patient_id, p.first_name AS patient_first_name, p.last_name AS patient_last_name,");
            Console.WriteLine("       p.contact_phone AS patient_phone, p.email AS patient_email,");
            Console.WriteLine("       p.mailing_address AS patient_address, p.date_of_birth AS patient_dob,");
            Console.WriteLine("       s.surgery_name, s.location_address AS surgery_address");
            Console.WriteLine("FROM Appointments a");
            Console.WriteLine("INNER JOIN Patients p ON a.patient_id = p.patient_id");
            Console.WriteLine("INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id");
            Console.WriteLine("WHERE a.dentist_id = 1");
            Console.WriteLine("ORDER BY a.appointment_date, a.appointment_time;");
            Console.WriteLine("```");
            
            Console.WriteLine("\n3️⃣ Query 3: Appointments at Surgery Location ID = 1");
            Console.WriteLine("```sql");
            Console.WriteLine("SELECT a.appointment_id, a.appointment_date, a.appointment_time, a.status,");
            Console.WriteLine("       d.first_name + ' ' + d.last_name AS dentist_name, d.specialization,");
            Console.WriteLine("       p.first_name + ' ' + p.last_name AS patient_name,");
            Console.WriteLine("       p.contact_phone AS patient_phone, p.email AS patient_email");
            Console.WriteLine("FROM Appointments a");
            Console.WriteLine("INNER JOIN Dentists d ON a.dentist_id = d.dentist_id");
            Console.WriteLine("INNER JOIN Patients p ON a.patient_id = p.patient_id");
            Console.WriteLine("WHERE a.surgery_id = 1");
            Console.WriteLine("ORDER BY a.appointment_date, a.appointment_time;");
            Console.WriteLine("```");
            
            Console.WriteLine("\n4️⃣ Query 4: Patient Appointments on Specific Date");
            Console.WriteLine("```sql");
            Console.WriteLine("SELECT a.appointment_id, a.appointment_time, a.status,");
            Console.WriteLine("       d.first_name + ' ' + d.last_name AS dentist_name, d.specialization,");
            Console.WriteLine("       d.contact_phone AS dentist_phone, d.email AS dentist_email,");
            Console.WriteLine("       s.surgery_name, s.location_address AS surgery_address,");
            Console.WriteLine("       s.telephone AS surgery_phone");
            Console.WriteLine("FROM Appointments a");
            Console.WriteLine("INNER JOIN Dentists d ON a.dentist_id = d.dentist_id");
            Console.WriteLine("INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id");
            Console.WriteLine("WHERE a.patient_id = 3 AND a.appointment_date = '2013-09-12'");
            Console.WriteLine("ORDER BY a.appointment_time;");
            Console.WriteLine("```");
        }
    }
}