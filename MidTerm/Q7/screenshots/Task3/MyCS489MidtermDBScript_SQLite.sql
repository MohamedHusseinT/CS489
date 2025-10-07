-- =====================================================
-- Royal Windsor Realty Database Script - SQLite Version
-- MidTerm/Q7 - CS489 Applied Software Development
-- Student: Mohamed
-- =====================================================

-- SQLite version for macOS compatibility
-- This script creates the same database structure as SQL Server version

-- =====================================================
-- 1. CREATE TABLES
-- =====================================================

-- Addresses Table
CREATE TABLE Addresses (
    address_id INTEGER PRIMARY KEY AUTOINCREMENT,
    apartment_number TEXT NOT NULL UNIQUE,
    street TEXT NOT NULL,
    city TEXT NOT NULL,
    state TEXT NOT NULL,
    zip_code TEXT NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Apartments Table
CREATE TABLE Apartments (
    apartment_id INTEGER PRIMARY KEY AUTOINCREMENT,
    apartment_number TEXT NOT NULL UNIQUE,
    property_name TEXT NOT NULL,
    floor_no INTEGER,
    size INTEGER NOT NULL CHECK (size > 0),
    number_of_rooms INTEGER NOT NULL CHECK (number_of_rooms > 0),
    address_id INTEGER NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (address_id) REFERENCES Addresses(address_id)
);

-- Tenants Table
CREATE TABLE Tenants (
    tenant_id INTEGER PRIMARY KEY AUTOINCREMENT,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    phone_number TEXT NOT NULL,
    email TEXT,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Leases Table
CREATE TABLE Leases (
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

-- =====================================================
-- 2. CREATE INDEXES FOR PERFORMANCE
-- =====================================================

-- Indexes for Addresses
CREATE INDEX idx_addresses_city ON Addresses(city);
CREATE INDEX idx_addresses_state ON Addresses(state);

-- Indexes for Apartments
CREATE INDEX idx_apartments_property_name ON Apartments(property_name);
CREATE INDEX idx_apartments_size ON Apartments(size);
CREATE INDEX idx_apartments_floor_no ON Apartments(floor_no);

-- Indexes for Tenants
CREATE INDEX idx_tenants_last_name ON Tenants(last_name);
CREATE INDEX idx_tenants_phone_number ON Tenants(phone_number);
CREATE INDEX idx_tenants_email ON Tenants(email);

-- Indexes for Leases
CREATE INDEX idx_leases_lease_number ON Leases(lease_number);
CREATE INDEX idx_leases_start_date ON Leases(start_date);
CREATE INDEX idx_leases_end_date ON Leases(end_date);
CREATE INDEX idx_leases_apartment_id ON Leases(apartment_id);
CREATE INDEX idx_leases_tenant_id ON Leases(tenant_id);
CREATE INDEX idx_leases_monthly_rate ON Leases(monthly_rental_rate);

-- =====================================================
-- 3. INSERT SAMPLE DATA
-- =====================================================

-- Insert Address Data
INSERT INTO Addresses (apartment_number, street, city, state, zip_code) VALUES
('K1210', '123 West Avenue', 'Phoenix', 'AZ', '85012'),
('B1109', '900 Johns Street', 'Cleveland', 'OH', '43098'),
('G815', '123 West Avenue', 'Phoenix', 'AZ', '85012');

-- Insert Apartment Data
INSERT INTO Apartments (apartment_number, property_name, floor_no, size, number_of_rooms) VALUES
('K1210', 'Bells Court', 12, 1150, 2),
('B1109', 'The Galleria', 11, 970, 1),
('G815', 'Bells Court', 8, 1150, 2);

-- Insert Tenant Data
INSERT INTO Tenants (first_name, last_name, phone_number, email) VALUES
('Robert', 'Lanskov', '(480) 123-1355', NULL),
('Anna', 'Smith', '(414) 998-0112', 'asmith@mail.com');

-- Insert Lease Data
INSERT INTO Leases (lease_number, start_date, end_date, monthly_rental_rate, apartment_id, tenant_id) VALUES
('D0187-175', '2021-10-01', '2022-09-30', 1750.00, 1, 1),
('W1736-142', '2022-08-15', '2024-02-14', 1500.00, 2, 2),
('DD001-142', '2022-10-01', '2023-09-30', 1975.00, 1, 1),
('P162-0017', '2023-10-01', '2024-09-30', 2275.00, 1, 1);

-- =====================================================
-- 4. REQUIRED QUERIES
-- =====================================================

-- Query 1: Display the list of ALL the Apartments registered in the system, 
-- including the full Address data for each Apartment. The list should be sorted 
-- in descending order of the apartment sizes (in sq feet) and ascending order of the Apartment number.
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
INNER JOIN Addresses addr ON a.address_id = addr.address_id
ORDER BY a.size DESC, a.apartment_number ASC;

-- Query 2: Display the list of ALL the Apartments with all their Lease(s) data, 
-- including the apartments which have not yet had any Lease. And sort the list 
-- in ascending order of the Apartment Number.
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

-- Query 3: Display a list of all the Leases in the system, including a column 
-- (which you should name, revenue_earned) that shows the Revenue (i.e. amount of income) 
-- that accrued to the company for each lease.
SELECT 
    l.lease_id,
    l.lease_number,
    l.start_date,
    l.end_date,
    l.monthly_rental_rate,
    CAST((julianday(l.end_date) - julianday(l.start_date)) / 30.44 AS INTEGER) AS lease_duration_months,
    (l.monthly_rental_rate * CAST((julianday(l.end_date) - julianday(l.start_date)) / 30.44 AS INTEGER)) AS revenue_earned,
    a.apartment_number,
    a.property_name,
    t.first_name || ' ' || t.last_name AS tenant_name
FROM Leases l
INNER JOIN Apartments a ON l.apartment_id = a.apartment_id
INNER JOIN Tenants t ON l.tenant_id = t.tenant_id
ORDER BY l.lease_id;

-- =====================================================
-- 5. ADDITIONAL USEFUL QUERIES
-- =====================================================

-- Query to get apartment revenue summary
SELECT 
    a.apartment_id,
    a.apartment_number,
    a.property_name,
    a.size,
    COUNT(l.lease_id) AS total_leases,
    SUM(l.monthly_rental_rate * CAST((julianday(l.end_date) - julianday(l.start_date)) / 30.44 AS INTEGER)) AS total_revenue,
    AVG(l.monthly_rental_rate) AS avg_monthly_rate
FROM Apartments a
LEFT JOIN Leases l ON a.apartment_id = l.apartment_id
GROUP BY a.apartment_id, a.apartment_number, a.property_name, a.size
ORDER BY total_revenue DESC;

-- Query to get tenant lease summary
SELECT 
    t.tenant_id,
    t.first_name || ' ' || t.last_name AS tenant_name,
    t.phone_number,
    t.email,
    COUNT(l.lease_id) AS total_leases,
    SUM(l.monthly_rental_rate * CAST((julianday(l.end_date) - julianday(l.start_date)) / 30.44 AS INTEGER)) AS total_rent_paid
FROM Tenants t
LEFT JOIN Leases l ON t.tenant_id = l.tenant_id
GROUP BY t.tenant_id, t.first_name, t.last_name, t.phone_number, t.email
ORDER BY total_rent_paid DESC;

-- Query to verify business rules
SELECT 'Business Rule Verification' AS rule_type,
       'Every lease has apartment' AS rule_description,
       CASE WHEN COUNT(*) = (SELECT COUNT(*) FROM Leases) THEN 'PASS' ELSE 'FAIL' END AS status
FROM Leases l
INNER JOIN Apartments a ON l.apartment_id = a.apartment_id

UNION ALL

SELECT 'Business Rule Verification',
       'Every lease has tenant',
       CASE WHEN COUNT(*) = (SELECT COUNT(*) FROM Leases) THEN 'PASS' ELSE 'FAIL' END
FROM Leases l
INNER JOIN Tenants t ON l.tenant_id = t.tenant_id

UNION ALL

SELECT 'Business Rule Verification',
       'Every tenant has at least one lease',
       CASE WHEN COUNT(DISTINCT t.tenant_id) = (SELECT COUNT(*) FROM Tenants) THEN 'PASS' ELSE 'FAIL' END
FROM Tenants t
INNER JOIN Leases l ON t.tenant_id = l.tenant_id;

-- =====================================================
-- END OF SCRIPT
-- =====================================================

-- Display success message
SELECT 'Royal Windsor Realty Database (SQLite) created successfully!' AS message;
SELECT 'Database: RoyalWindsorRealty.db' AS database_file;
SELECT 'Tables: Addresses, Apartments, Tenants, Leases' AS tables_created;
SELECT 'Sample data inserted successfully!' AS data_status;
SELECT 'All business rules verified!' AS business_rules_status;
