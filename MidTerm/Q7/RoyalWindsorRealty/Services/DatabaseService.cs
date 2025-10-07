using System;
using System.Data.SQLite;
using System.IO;

namespace RoyalWindsorRealty.Services
{
    /// <summary>
    /// Service for managing SQLite database operations
    /// </summary>
    public class DatabaseService
    {
        private readonly string _connectionString;
        private readonly string _databasePath;
        
        public DatabaseService()
        {
            _databasePath = Path.Combine(Directory.GetCurrentDirectory(), "RoyalWindsorRealty.db");
            _connectionString = $"Data Source={_databasePath}";
        }
        
        /// <summary>
        /// Initialize database with schema and sample data
        /// </summary>
        public void InitializeDatabase()
        {
            try
            {
                // Check if database exists
                if (!File.Exists(_databasePath))
                {
                    Console.WriteLine("📊 Creating new database...");
                    CreateDatabase();
                }
                else
                {
                    Console.WriteLine("📊 Database already exists, skipping creation.");
                }
                
                Console.WriteLine($"📁 Database location: {_databasePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error initializing database: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Create database with schema and sample data
        /// </summary>
        private void CreateDatabase()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            
            // Create basic schema manually (more reliable than parsing SQL file)
            CreateBasicSchema(connection);
        }
        
        /// <summary>
        /// Create basic database schema manually
        /// </summary>
        private void CreateBasicSchema(SQLiteConnection connection)
        {
            // Create tables one by one to avoid issues
            var createAddresses = @"
                CREATE TABLE IF NOT EXISTS Addresses (
                    address_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    apartment_number TEXT NOT NULL UNIQUE,
                    street TEXT NOT NULL,
                    city TEXT NOT NULL,
                    state TEXT NOT NULL,
                    zip_code TEXT NOT NULL,
                    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";
            
            var createApartments = @"
                CREATE TABLE IF NOT EXISTS Apartments (
                    apartment_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    apartment_number TEXT NOT NULL UNIQUE,
                    property_name TEXT NOT NULL,
                    floor_no INTEGER,
                    size INTEGER NOT NULL CHECK (size > 0),
                    number_of_rooms INTEGER NOT NULL CHECK (number_of_rooms > 0),
                    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (apartment_number) REFERENCES Addresses(apartment_number)
                );
            ";
            
            var createTenants = @"
                CREATE TABLE IF NOT EXISTS Tenants (
                    tenant_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    phone_number TEXT NOT NULL,
                    email TEXT,
                    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";
            
            var createLeases = @"
                CREATE TABLE IF NOT EXISTS Leases (
                    lease_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    lease_number TEXT NOT NULL UNIQUE,
                    start_date DATE NOT NULL,
                    end_date DATE NOT NULL,
                    monthly_rental_rate DECIMAL(10,2) NOT NULL CHECK (monthly_rental_rate > 0),
                    apartment_id INTEGER NOT NULL,
                    tenant_id INTEGER NOT NULL,
                    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (apartment_id) REFERENCES Apartments(apartment_id),
                    FOREIGN KEY (tenant_id) REFERENCES Tenants(tenant_id),
                    CHECK (start_date < end_date)
                );
            ";
            
            // Execute each table creation separately
            using (var cmd = new SQLiteCommand(createAddresses, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Addresses table created");
            }
            
            using (var cmd = new SQLiteCommand(createApartments, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Apartments table created");
            }
            
            using (var cmd = new SQLiteCommand(createTenants, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Tenants table created");
            }
            
            using (var cmd = new SQLiteCommand(createLeases, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Leases table created");
            }
            
            // Insert sample data
            InsertSampleData(connection);
        }
        
        /// <summary>
        /// Insert sample data into database
        /// </summary>
        private void InsertSampleData(SQLiteConnection connection)
        {
            // Insert addresses
            var insertAddresses = @"
                INSERT OR IGNORE INTO Addresses (apartment_number, street, city, state, zip_code) VALUES
                ('K1210', '123 West Avenue', 'Phoenix', 'AZ', '85012'),
                ('B1109', '900 Johns Street', 'Cleveland', 'OH', '43098'),
                ('G815', '123 West Avenue', 'Phoenix', 'AZ', '85012');
            ";
            
            // Insert apartments
            var insertApartments = @"
                INSERT OR IGNORE INTO Apartments (apartment_number, property_name, floor_no, size, number_of_rooms) VALUES
                ('K1210', 'Bells Court', 12, 1150, 2),
                ('B1109', 'The Galleria', 11, 970, 1),
                ('G815', 'Bells Court', 8, 1150, 2);
            ";
            
            // Insert tenants
            var insertTenants = @"
                INSERT OR IGNORE INTO Tenants (first_name, last_name, phone_number, email) VALUES
                ('Robert', 'Lanskov', '(480) 123-1355', NULL),
                ('Anna', 'Smith', '(414) 998-0112', 'asmith@mail.com');
            ";
            
            // Insert leases
            var insertLeases = @"
                INSERT OR IGNORE INTO Leases (lease_number, start_date, end_date, monthly_rental_rate, apartment_id, tenant_id) VALUES
                ('D0187-175', '2021-10-01', '2022-09-30', 1750.00, 1, 1),
                ('W1736-142', '2022-08-15', '2024-02-14', 1500.00, 2, 2),
                ('DD001-142', '2022-10-01', '2023-09-30', 1975.00, 1, 1),
                ('P162-0017', '2023-10-01', '2024-09-30', 2275.00, 1, 1);
            ";
            
            // Execute each insert separately
            using (var cmd = new SQLiteCommand(insertAddresses, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Addresses data inserted");
            }
            
            using (var cmd = new SQLiteCommand(insertApartments, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Apartments data inserted");
            }
            
            using (var cmd = new SQLiteCommand(insertTenants, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Tenants data inserted");
            }
            
            using (var cmd = new SQLiteCommand(insertLeases, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Leases data inserted");
            }
            
            Console.WriteLine("✅ All sample data inserted successfully!");
        }
        
        /// <summary>
        /// Execute query and return results
        /// </summary>
        public void ExecuteQuery(string query, string title)
        {
            try
            {
                using var connection = new SQLiteConnection(_connectionString);
                connection.Open();
                
                using var cmd = new SQLiteCommand(query, connection);
                using var reader = cmd.ExecuteReader();
                
                Console.WriteLine($"\n{title}");
                Console.WriteLine(new string('=', title.Length));
                
                // Print the SQL query
                Console.WriteLine("\n🔍 SQL Query:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine(query.Trim());
                Console.WriteLine(new string('-', 50));
                
                if (reader.HasRows)
                {
                    Console.WriteLine("\n📊 Query Results:");
                    Console.WriteLine(new string('-', 50));
                    
                    // Print column headers
                    var columnCount = reader.FieldCount;
                    var headers = new string[columnCount];
                    
                    for (int i = 0; i < columnCount; i++)
                    {
                        headers[i] = reader.GetName(i);
                    }
                    
                    Console.WriteLine(string.Join(" | ", headers));
                    Console.WriteLine(new string('-', string.Join(" | ", headers).Length));
                    
                    // Print data rows
                    while (reader.Read())
                    {
                        var row = new object[columnCount];
                        for (int i = 0; i < columnCount; i++)
                        {
                            row[i] = reader.IsDBNull(i) ? "NULL" : reader.GetValue(i);
                        }
                        Console.WriteLine(string.Join(" | ", row));
                    }
                }
                else
                {
                    Console.WriteLine("\n📊 Query Results:");
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine("No results found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error executing query: {ex.Message}");
                Console.WriteLine($"Query: {query}");
            }
        }
        
        /// <summary>
        /// Get database connection string
        /// </summary>
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
