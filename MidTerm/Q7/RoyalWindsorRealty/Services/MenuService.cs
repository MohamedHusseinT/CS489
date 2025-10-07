using System;

namespace RoyalWindsorRealty.Services
{
    /// <summary>
    /// Service for managing interactive CLI menu for Royal Windsor Realty
    /// </summary>
    public class MenuService
    {
        private readonly DatabaseService _databaseService;
        
        public MenuService()
        {
            _databaseService = new DatabaseService();
        }
        
        /// <summary>
        /// Main menu loop for Royal Windsor Realty System
        /// </summary>
        public void RunMainMenu()
        {
            bool continueRunning = true;
            
            while (continueRunning)
            {
                DisplayMainMenu();
                var choice = GetUserChoice();
                
                switch (choice)
                {
                    case 1:
                        ExecuteQuery1();
                        break;
                    case 2:
                        ExecuteQuery2();
                        break;
                    case 3:
                        ExecuteQuery3();
                        break;
                    case 4:
                        ShowDatabaseInfo();
                        break;
                    case 5:
                        ShowBusinessRules();
                        break;
                    case 6:
                        Console.WriteLine("\n👋 Thank you for using Royal Windsor Realty Management System!");
                        continueRunning = false;
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice. Please select 1-6.");
                        break;
                }
                
                if (continueRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        
        /// <summary>
        /// Display the main menu options
        /// </summary>
        private void DisplayMainMenu()
        {
            Console.WriteLine("🏢 Royal Windsor Realty - Apartments Leasing Management System");
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("MidTerm/Q7 - CS489 Applied Software Development");
            Console.WriteLine("Student: Mohamed");
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine();
            Console.WriteLine("1. 📊 Query 1: All Apartments with Address (sorted by size desc, apartment number asc)");
            Console.WriteLine("2. 🏠 Query 2: All Apartments with Leases (including apartments without leases)");
            Console.WriteLine("3. 💰 Query 3: All Leases with Revenue Calculation");
            Console.WriteLine("4. 📈 Database Information & Statistics");
            Console.WriteLine("5. ✅ Business Rules Verification");
            Console.WriteLine("6. ❌ Exit");
            Console.WriteLine();
            Console.Write("Enter your choice (1-6): ");
        }
        
        /// <summary>
        /// Get user input and validate choice
        /// </summary>
        private int GetUserChoice()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 6)
                {
                    return choice;
                }
                Console.Write("❌ Invalid input. Please enter a number between 1-6: ");
            }
        }
        
        /// <summary>
        /// Execute Query 1: All Apartments with Address
        /// </summary>
        private void ExecuteQuery1()
        {
            var query = @"
                SELECT 
                    a.apartment_id,
                    a.apartment_number,
                    a.property_name,
                    a.floor_no,
                    a.size,
                    a.number_of_rooms,
                    addr.street,
                    addr.city,
                    addr.state,
                    addr.zip_code,
                    addr.street || ', ' || addr.city || ', ' || addr.state || ' ' || addr.zip_code AS full_address
                FROM Apartments a
                INNER JOIN Addresses addr ON a.apartment_number = addr.apartment_number
                ORDER BY a.size DESC, a.apartment_number ASC;
            ";
            
            _databaseService.ExecuteQuery(query, "QUERY 1: All Apartments with Address (Sorted by Size Desc, Apartment Number Asc)");
        }
        
        /// <summary>
        /// Execute Query 2: All Apartments with Leases
        /// </summary>
        private void ExecuteQuery2()
        {
            var query = @"
                SELECT 
                    a.apartment_id,
                    a.apartment_number,
                    a.property_name,
                    a.floor_no,
                    a.size,
                    a.number_of_rooms,
                    l.lease_id,
                    l.lease_number,
                    l.start_date,
                    l.end_date,
                    l.monthly_rental_rate,
                    t.first_name || ' ' || t.last_name AS tenant_name,
                    t.phone_number
                FROM Apartments a
                LEFT JOIN Leases l ON a.apartment_id = l.apartment_id
                LEFT JOIN Tenants t ON l.tenant_id = t.tenant_id
                ORDER BY a.apartment_number ASC, l.start_date ASC;
            ";
            
            _databaseService.ExecuteQuery(query, "QUERY 2: All Apartments with Leases (Including Apartments without Leases)");
        }
        
        /// <summary>
        /// Execute Query 3: All Leases with Revenue
        /// </summary>
        private void ExecuteQuery3()
        {
            var query = @"
                SELECT 
                    l.lease_id,
                    l.lease_number,
                    l.start_date,
                    l.end_date,
                    l.monthly_rental_rate,
                    CASE 
                        WHEN strftime('%Y', l.end_date) = strftime('%Y', l.start_date) 
                        THEN (strftime('%m', l.end_date) - strftime('%m', l.start_date)) + 1
                        ELSE (strftime('%Y', l.end_date) - strftime('%Y', l.start_date)) * 12 + 
                             (strftime('%m', l.end_date) - strftime('%m', l.start_date)) + 1
                    END AS lease_duration_months,
                    (l.monthly_rental_rate * 
                        CASE 
                            WHEN strftime('%Y', l.end_date) = strftime('%Y', l.start_date) 
                            THEN (strftime('%m', l.end_date) - strftime('%m', l.start_date)) + 1
                            ELSE (strftime('%Y', l.end_date) - strftime('%Y', l.start_date)) * 12 + 
                                 (strftime('%m', l.end_date) - strftime('%m', l.start_date)) + 1
                        END
                    ) AS revenue_earned,
                    a.apartment_number,
                    a.property_name,
                    t.first_name || ' ' || t.last_name AS tenant_name
                FROM Leases l
                INNER JOIN Apartments a ON l.apartment_id = a.apartment_id
                INNER JOIN Tenants t ON l.tenant_id = t.tenant_id
                ORDER BY l.lease_id;
            ";
            
            _databaseService.ExecuteQuery(query, "QUERY 3: All Leases with Revenue Calculation");
        }
        
        /// <summary>
        /// Show database information and statistics
        /// </summary>
        private void ShowDatabaseInfo()
        {
            Console.WriteLine("\n📈 DATABASE INFORMATION & STATISTICS");
            Console.WriteLine(new string('=', 50));
            
            // Count records in each table
            var countQuery = @"
                SELECT 'Addresses' AS table_name, COUNT(*) AS record_count FROM Addresses
                UNION ALL
                SELECT 'Apartments', COUNT(*) FROM Apartments
                UNION ALL
                SELECT 'Tenants', COUNT(*) FROM Tenants
                UNION ALL
                SELECT 'Leases', COUNT(*) FROM Leases;
            ";
            
            _databaseService.ExecuteQuery(countQuery, "Record Counts by Table");
            
            // Show apartment revenue summary
            var revenueQuery = @"
                SELECT 
                    a.apartment_id,
                    a.apartment_number,
                    a.property_name,
                    a.size,
                    COUNT(l.lease_id) AS total_leases,
                    SUM(l.monthly_rental_rate * 
                        CASE 
                            WHEN strftime('%Y', l.end_date) = strftime('%Y', l.start_date) 
                            THEN (strftime('%m', l.end_date) - strftime('%m', l.start_date)) + 1
                            ELSE (strftime('%Y', l.end_date) - strftime('%Y', l.start_date)) * 12 + 
                                 (strftime('%m', l.end_date) - strftime('%m', l.start_date)) + 1
                        END
                    ) AS total_revenue,
                    AVG(l.monthly_rental_rate) AS avg_monthly_rate
                FROM Apartments a
                LEFT JOIN Leases l ON a.apartment_id = l.apartment_id
                GROUP BY a.apartment_id, a.apartment_number, a.property_name, a.size
                ORDER BY total_revenue DESC;
            ";
            
            _databaseService.ExecuteQuery(revenueQuery, "Apartment Revenue Summary");
        }
        
        /// <summary>
        /// Show business rules verification
        /// </summary>
        private void ShowBusinessRules()
        {
            Console.WriteLine("\n✅ BUSINESS RULES VERIFICATION");
            Console.WriteLine(new string('=', 50));
            
            var rulesQuery = @"
                SELECT 'Every lease has apartment' AS rule_description,
                       CASE WHEN COUNT(*) = (SELECT COUNT(*) FROM Leases) THEN 'PASS' ELSE 'FAIL' END AS status
                FROM Leases l
                INNER JOIN Apartments a ON l.apartment_id = a.apartment_id
                
                UNION ALL
                
                SELECT 'Every lease has tenant',
                       CASE WHEN COUNT(*) = (SELECT COUNT(*) FROM Leases) THEN 'PASS' ELSE 'FAIL' END
                FROM Leases l
                INNER JOIN Tenants t ON l.tenant_id = t.tenant_id
                
                UNION ALL
                
                SELECT 'Every tenant has at least one lease',
                       CASE WHEN COUNT(DISTINCT t.tenant_id) = (SELECT COUNT(*) FROM Tenants) THEN 'PASS' ELSE 'FAIL' END
                FROM Tenants t
                INNER JOIN Leases l ON t.tenant_id = l.tenant_id;
            ";
            
            _databaseService.ExecuteQuery(rulesQuery, "Business Rules Verification");
        }
    }
}
