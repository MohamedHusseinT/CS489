-- =====================================================
-- Royal Windsor Realty Database Script
-- MidTerm/Q7 - CS489 Applied Software Development
-- Student: Mohamed
-- =====================================================

-- Create Database
CREATE DATABASE RoyalWindsorRealty;
GO

USE RoyalWindsorRealty;
GO

-- =====================================================
-- 1. CREATE TABLES
-- =====================================================

-- Addresses Table
CREATE TABLE Addresses (
    address_id INT IDENTITY(1,1) PRIMARY KEY,
    apartment_number NVARCHAR(20) NOT NULL UNIQUE,
    street NVARCHAR(100) NOT NULL,
    city NVARCHAR(50) NOT NULL,
    state NVARCHAR(10) NOT NULL,
    zip_code NVARCHAR(10) NOT NULL,
    created_date DATETIME2 DEFAULT GETDATE()
);

-- Apartments Table
CREATE TABLE Apartments (
    apartment_id INT IDENTITY(1,1) PRIMARY KEY,
    apartment_number NVARCHAR(20) NOT NULL UNIQUE,
    property_name NVARCHAR(100) NOT NULL,
    floor_no INT,
    size INT NOT NULL CHECK (size > 0),
    number_of_rooms INT NOT NULL CHECK (number_of_rooms > 0),
    address_id INT NOT NULL,
    created_date DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (address_id) REFERENCES Addresses(address_id)
);

-- Tenants Table
CREATE TABLE Tenants (
    tenant_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    phone_number NVARCHAR(20) NOT NULL,
    email NVARCHAR(100),
    created_date DATETIME2 DEFAULT GETDATE()
);

-- Leases Table
CREATE TABLE Leases (
    lease_id INT IDENTITY(1,1) PRIMARY KEY,
    lease_number NVARCHAR(50) NOT NULL UNIQUE,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    monthly_rental_rate DECIMAL(10,2) NOT NULL CHECK (monthly_rental_rate > 0),
    apartment_id INT NOT NULL,
    tenant_id INT NOT NULL,
    created_date DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (apartment_id) REFERENCES Apartments(apartment_id),
    FOREIGN KEY (tenant_id) REFERENCES Tenants(tenant_id),
    CONSTRAINT CHK_Lease_Dates CHECK (start_date < end_date)
);

-- =====================================================
-- 2. CREATE INDEXES FOR PERFORMANCE
-- =====================================================

-- Indexes for Addresses
CREATE INDEX IX_Addresses_ApartmentNumber ON Addresses(apartment_number);
CREATE INDEX IX_Addresses_City ON Addresses(city);
CREATE INDEX IX_Addresses_State ON Addresses(state);

-- Indexes for Apartments
CREATE INDEX IX_Apartments_PropertyName ON Apartments(property_name);
CREATE INDEX IX_Apartments_Size ON Apartments(size);
CREATE INDEX IX_Apartments_FloorNo ON Apartments(floor_no);
CREATE INDEX IX_Apartments_AddressId ON Apartments(address_id);

-- Indexes for Tenants
CREATE INDEX IX_Tenants_LastName ON Tenants(last_name);
CREATE INDEX IX_Tenants_PhoneNumber ON Tenants(phone_number);
CREATE INDEX IX_Tenants_Email ON Tenants(email);

-- Indexes for Leases
CREATE INDEX IX_Leases_LeaseNumber ON Leases(lease_number);
CREATE INDEX IX_Leases_StartDate ON Leases(start_date);
CREATE INDEX IX_Leases_EndDate ON Leases(end_date);
CREATE INDEX IX_Leases_ApartmentId ON Leases(apartment_id);
CREATE INDEX IX_Leases_TenantId ON Leases(tenant_id);
CREATE INDEX IX_Leases_MonthlyRate ON Leases(monthly_rental_rate);

-- =====================================================
-- 3. CREATE VIEWS FOR COMMON QUERIES
-- =====================================================

-- View for Apartment Details with Address
CREATE VIEW vw_ApartmentDetails AS
SELECT 
    a.apartment_id,
    a.apartment_number,
    a.property_name,
    a.floor_no,
    a.size,
    a.number_of_rooms,
    addr.address_id,
    addr.street,
    addr.city,
    addr.state,
    addr.zip_code,
    CONCAT(addr.street, ', ', addr.city, ', ', addr.state, ' ', addr.zip_code) AS full_address
FROM Apartments a
INNER JOIN Addresses addr ON a.address_id = addr.address_id;

-- View for Lease Details with Apartment and Tenant Info
CREATE VIEW vw_LeaseDetails AS
SELECT 
    l.lease_id,
    l.lease_number,
    l.start_date,
    l.end_date,
    l.monthly_rental_rate,
    CASE 
        WHEN YEAR(l.end_date) = YEAR(l.start_date) 
        THEN (MONTH(l.end_date) - MONTH(l.start_date)) + 1
        ELSE (YEAR(l.end_date) - YEAR(l.start_date)) * 12 + 
             (MONTH(l.end_date) - MONTH(l.start_date)) + 1
    END AS lease_duration_months,
    (l.monthly_rental_rate * 
        CASE 
            WHEN YEAR(l.end_date) = YEAR(l.start_date) 
            THEN (MONTH(l.end_date) - MONTH(l.start_date)) + 1
            ELSE (YEAR(l.end_date) - YEAR(l.start_date)) * 12 + 
                 (MONTH(l.end_date) - MONTH(l.start_date)) + 1
        END
    ) AS revenue_earned,
    a.apartment_id,
    a.apartment_number,
    a.property_name,
    a.size AS apartment_size,
    t.tenant_id,
    t.first_name + ' ' + t.last_name AS tenant_name,
    t.phone_number,
    t.email
FROM Leases l
INNER JOIN Apartments a ON l.apartment_id = a.apartment_id
INNER JOIN Tenants t ON l.tenant_id = t.tenant_id;

-- View for Apartment Revenue Summary
CREATE VIEW vw_ApartmentRevenue AS
SELECT 
    a.apartment_id,
    a.apartment_number,
    a.property_name,
    a.size,
    COUNT(l.lease_id) AS total_leases,
    SUM(l.monthly_rental_rate * 
        CASE 
            WHEN YEAR(l.end_date) = YEAR(l.start_date) 
            THEN (MONTH(l.end_date) - MONTH(l.start_date)) + 1
            ELSE (YEAR(l.end_date) - YEAR(l.start_date)) * 12 + 
                 (MONTH(l.end_date) - MONTH(l.start_date)) + 1
        END
    ) AS total_revenue,
    AVG(l.monthly_rental_rate) AS avg_monthly_rate
FROM Apartments a
LEFT JOIN Leases l ON a.apartment_id = l.apartment_id
GROUP BY a.apartment_id, a.apartment_number, a.property_name, a.size;

-- =====================================================
-- 4. CREATE STORED PROCEDURES
-- =====================================================

-- Procedure to calculate lease revenue
CREATE PROCEDURE sp_CalculateLeaseRevenue
    @LeaseId INT,
    @Revenue DECIMAL(10,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT @Revenue = monthly_rental_rate * 
        CASE 
            WHEN YEAR(end_date) = YEAR(start_date) 
            THEN (MONTH(end_date) - MONTH(start_date)) + 1
            ELSE (YEAR(end_date) - YEAR(start_date)) * 12 + 
                 (MONTH(end_date) - MONTH(start_date)) + 1
        END
    FROM Leases
    WHERE lease_id = @LeaseId;
END;

-- Procedure to get apartment with all leases
CREATE PROCEDURE sp_GetApartmentWithLeases
    @ApartmentId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.apartment_id,
        a.apartment_number,
        a.property_name,
        a.floor_no,
        a.size,
        a.number_of_rooms,
        addr.address_id,
        addr.street,
        addr.city,
        addr.state,
        addr.zip_code,
        l.lease_id,
        l.lease_number,
        l.start_date,
        l.end_date,
        l.monthly_rental_rate,
        t.first_name + ' ' + t.last_name AS tenant_name
    FROM Apartments a
    INNER JOIN Addresses addr ON a.address_id = addr.address_id
    LEFT JOIN Leases l ON a.apartment_id = l.apartment_id
    LEFT JOIN Tenants t ON l.tenant_id = t.tenant_id
    WHERE a.apartment_id = @ApartmentId
    ORDER BY l.start_date;
END;

-- =====================================================
-- 5. REQUIRED QUERIES (for reference)
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
    addr.address_id,
    addr.street,
    addr.city,
    addr.state,
    addr.zip_code,
    CONCAT(addr.street, ', ', addr.city, ', ', addr.state, ' ', addr.zip_code) AS full_address
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
    t.first_name + ' ' + t.last_name AS tenant_name,
    t.phone_number
FROM Apartments a
LEFT JOIN Leases l ON a.apartment_id = l.apartment_id
LEFT JOIN Tenants t ON l.tenant_id = t.tenant_id
ORDER BY a.apartment_number ASC, l.start_date ASC;

-- Query 3: Display a list of all the Leases in the system, including a column 
-- (which you should name, revenue_earned) that shows the Revenue (i.e. amount of income) 
-- that accrued to the company for each lease.
-- NOTE: Uses corrected lease duration calculation for accurate month counting
SELECT 
    l.lease_id,
    l.lease_number,
    l.start_date,
    l.end_date,
    l.monthly_rental_rate,
    CASE 
        WHEN YEAR(l.end_date) = YEAR(l.start_date) 
        THEN (MONTH(l.end_date) - MONTH(l.start_date)) + 1
        ELSE (YEAR(l.end_date) - YEAR(l.start_date)) * 12 + 
             (MONTH(l.end_date) - MONTH(l.start_date)) + 1
    END AS lease_duration_months,
    (l.monthly_rental_rate * 
        CASE 
            WHEN YEAR(l.end_date) = YEAR(l.start_date) 
            THEN (MONTH(l.end_date) - MONTH(l.start_date)) + 1
            ELSE (YEAR(l.end_date) - YEAR(l.start_date)) * 12 + 
                 (MONTH(l.end_date) - MONTH(l.start_date)) + 1
        END
    ) AS revenue_earned,
    a.apartment_number,
    a.property_name,
    t.first_name + ' ' + t.last_name AS tenant_name
FROM Leases l
INNER JOIN Apartments a ON l.apartment_id = a.apartment_id
INNER JOIN Tenants t ON l.tenant_id = t.tenant_id
ORDER BY l.lease_id;

-- =====================================================
-- END OF SCRIPT
-- =====================================================

PRINT 'Royal Windsor Realty Database created successfully!';
PRINT 'Database: RoyalWindsorRealty';
PRINT 'Tables: Addresses, Apartments, Tenants, Leases';
PRINT 'Views: vw_ApartmentDetails, vw_LeaseDetails, vw_ApartmentRevenue';
PRINT 'Stored Procedures: sp_CalculateLeaseRevenue, sp_GetApartmentWithLeases';
PRINT 'Sample data population script: MyCS489MidtermDBDataPopScript.sql';
