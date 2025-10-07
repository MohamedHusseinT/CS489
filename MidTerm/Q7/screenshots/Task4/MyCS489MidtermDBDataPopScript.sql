-- =====================================================
-- Royal Windsor Realty Data Population Script
-- MidTerm/Q7 - CS489 Applied Software Development
-- Student: Mohamed
-- =====================================================

USE RoyalWindsorRealty;
GO

-- =====================================================
-- 1. INSERT ADDRESS DATA
-- =====================================================

-- Enable identity insert to specify address_id values explicitly
SET IDENTITY_INSERT Addresses ON;

INSERT INTO Addresses (address_id, apartment_number, street, city, state, zip_code) VALUES
(1, 'K1210', '123 West Avenue', 'Phoenix', 'AZ', '85012'),
(2, 'B1109', '900 Johns Street', 'Cleveland', 'OH', '43098'),
(3, 'G815', '123 West Avenue', 'Phoenix', 'AZ', '85012');

-- Disable identity insert
SET IDENTITY_INSERT Addresses OFF;

-- =====================================================
-- 2. INSERT APARTMENT DATA
-- =====================================================

-- Enable identity insert to specify apartment_id values explicitly
SET IDENTITY_INSERT Apartments ON;

INSERT INTO Apartments (apartment_id, apartment_number, property_name, floor_no, size, number_of_rooms, address_id) VALUES
(1, 'K1210', 'Bells Court', 12, 1150, 2, 1),
(2, 'B1109', 'The Galleria', 11, 970, 1, 2),
(3, 'G815', 'Bells Court', 8, 1150, 2, 3);

-- Disable identity insert
SET IDENTITY_INSERT Apartments OFF;

-- =====================================================
-- 3. INSERT TENANT DATA
-- =====================================================

-- Enable identity insert to specify tenant_id values explicitly
SET IDENTITY_INSERT Tenants ON;

INSERT INTO Tenants (tenant_id, first_name, last_name, phone_number, email) VALUES
(1, 'Robert', 'Lanskov', '(480) 123-1355', NULL),
(2, 'Anna', 'Smith', '(414) 998-0112', 'asmith@mail.com');

-- Disable identity insert
SET IDENTITY_INSERT Tenants OFF;

-- =====================================================
-- 4. INSERT LEASE DATA
-- =====================================================

-- Enable identity insert to specify lease_id values explicitly
SET IDENTITY_INSERT Leases ON;

INSERT INTO Leases (lease_id, lease_number, start_date, end_date, monthly_rental_rate, apartment_id, tenant_id) VALUES
(1, 'D0187-175', '2021-10-01', '2022-09-30', 1750.00, 1, 1),
(2, 'W1736-142', '2022-08-15', '2024-02-14', 1500.00, 2, 2),
(3, 'DD001-142', '2022-10-01', '2023-09-30', 1975.00, 1, 1),
(4, 'P162-0017', '2023-10-01', '2024-09-30', 2275.00, 1, 1);

-- Disable identity insert
SET IDENTITY_INSERT Leases OFF;

-- =====================================================
-- 5. VERIFY DATA INSERTION
-- =====================================================

-- Check Addresses
SELECT 'Addresses' AS TableName, COUNT(*) AS RecordCount FROM Addresses
UNION ALL
SELECT 'Apartments', COUNT(*) FROM Apartments
UNION ALL
SELECT 'Tenants', COUNT(*) FROM Tenants
UNION ALL
SELECT 'Leases', COUNT(*) FROM Leases;

-- =====================================================
-- 6. SAMPLE DATA VERIFICATION QUERIES
-- =====================================================

-- Verify Address Data
SELECT 'Address Data Verification' AS QueryType;
SELECT 
    address_id,
    apartment_number,
    street,
    city,
    state,
    zip_code
FROM Addresses
ORDER BY address_id;

-- Verify Apartment Data
SELECT 'Apartment Data Verification' AS QueryType;
SELECT 
    apartment_id,
    apartment_number,
    property_name,
    floor_no,
    size,
    number_of_rooms,
    address_id
FROM Apartments
ORDER BY apartment_id;

-- Verify Tenant Data
SELECT 'Tenant Data Verification' AS QueryType;
SELECT 
    tenant_id,
    first_name,
    last_name,
    phone_number,
    email
FROM Tenants
ORDER BY tenant_id;

-- Verify Lease Data
SELECT 'Lease Data Verification' AS QueryType;
SELECT 
    lease_id,
    lease_number,
    start_date,
    end_date,
    monthly_rental_rate,
    apartment_id,
    tenant_id
FROM Leases
ORDER BY lease_id;

-- =====================================================
-- 7. RELATIONSHIP VERIFICATION
-- =====================================================

-- Verify Apartment-Address Relationships
SELECT 'Apartment-Address Relationships' AS QueryType;
SELECT 
    a.apartment_id,
    a.apartment_number,
    a.property_name,
    a.address_id,
    addr.street,
    addr.city,
    addr.state
FROM Apartments a
INNER JOIN Addresses addr ON a.address_id = addr.address_id
ORDER BY a.apartment_id;

-- Verify Lease-Apartment-Tenant Relationships
SELECT 'Lease-Apartment-Tenant Relationships' AS QueryType;
SELECT 
    l.lease_id,
    l.lease_number,
    l.start_date,
    l.end_date,
    l.monthly_rental_rate,
    a.apartment_number,
    a.property_name,
    t.first_name + ' ' + t.last_name AS tenant_name
FROM Leases l
INNER JOIN Apartments a ON l.apartment_id = a.apartment_id
INNER JOIN Tenants t ON l.tenant_id = t.tenant_id
ORDER BY l.lease_id;

-- =====================================================
-- 8. REVENUE CALCULATION VERIFICATION
-- =====================================================

-- Calculate Revenue for Each Lease (CORRECTED CALCULATION)
-- This query demonstrates the corrected lease duration calculation
-- that properly counts months from start to end date
SELECT 'Revenue Calculation Verification (CORRECTED)' AS QueryType;
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
    t.first_name + ' ' + t.last_name AS tenant_name
FROM Leases l
INNER JOIN Apartments a ON l.apartment_id = a.apartment_id
INNER JOIN Tenants t ON l.tenant_id = t.tenant_id
ORDER BY l.lease_id;

-- =====================================================
-- 9. BUSINESS RULES VERIFICATION
-- =====================================================

-- Check that every lease has an apartment
SELECT 'Business Rule: Every lease has apartment' AS RuleCheck;
SELECT 
    l.lease_id,
    l.lease_number,
    CASE 
        WHEN a.apartment_id IS NOT NULL THEN 'PASS'
        ELSE 'FAIL'
    END AS rule_status
FROM Leases l
LEFT JOIN Apartments a ON l.apartment_id = a.apartment_id;

-- Check that every lease has a tenant
SELECT 'Business Rule: Every lease has tenant' AS RuleCheck;
SELECT 
    l.lease_id,
    l.lease_number,
    CASE 
        WHEN t.tenant_id IS NOT NULL THEN 'PASS'
        ELSE 'FAIL'
    END AS rule_status
FROM Leases l
LEFT JOIN Tenants t ON l.tenant_id = t.tenant_id;

-- Check that every tenant has at least one lease
SELECT 'Business Rule: Every tenant has at least one lease' AS RuleCheck;
SELECT 
    t.tenant_id,
    t.first_name + ' ' + t.last_name AS tenant_name,
    COUNT(l.lease_id) AS lease_count,
    CASE 
        WHEN COUNT(l.lease_id) >= 1 THEN 'PASS'
        ELSE 'FAIL'
    END AS rule_status
FROM Tenants t
LEFT JOIN Leases l ON t.tenant_id = l.tenant_id
GROUP BY t.tenant_id, t.first_name, t.last_name;

-- =====================================================
-- 10. REQUIRED QUERIES FOR MIDTERM/Q7
-- =====================================================

-- REQUIRED QUERY 1: All Apartments with Address (sorted by size desc, apartment number asc)
-- This query displays all apartments with their full address information,
-- sorted by apartment size in descending order, then by apartment number in ascending order
SELECT 'REQUIRED QUERY 1: All Apartments with Address' AS QueryType;
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

-- REQUIRED QUERY 2: All Apartments with Leases (including apartments without leases)
-- This query displays all apartments and their lease information,
-- including apartments that have no leases (LEFT JOIN)
SELECT 'REQUIRED QUERY 2: All Apartments with Leases (Including Apartments without Leases)' AS QueryType;
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

-- REQUIRED QUERY 3: All Leases with Revenue Calculation
-- This query displays all leases with calculated revenue earned
-- Revenue = Monthly Rate × Lease Duration (in months)
-- Uses CORRECTED lease duration calculation for accurate month counting
SELECT 'REQUIRED QUERY 3: All Leases with Revenue Calculation' AS QueryType;
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
-- END OF DATA POPULATION SCRIPT
-- =====================================================

PRINT 'Royal Windsor Realty data population completed successfully!';
PRINT 'Data Summary:';
PRINT '- 3 Addresses inserted (address_id: 1, 2, 3)';
PRINT '- 3 Apartments inserted (apartment_id: 1, 2, 3)';
PRINT '- 2 Tenants inserted (tenant_id: 1, 2)';
PRINT '- 4 Leases inserted (lease_id: 1, 2, 3, 4)';
PRINT 'All business rules verified and relationships established.';
PRINT '';
PRINT 'IDENTITY INSERT USAGE:';
PRINT '- Used SET IDENTITY_INSERT ON/OFF to maintain consistent ID values';
PRINT '- All primary key values explicitly specified for data consistency';
PRINT '';
PRINT 'REQUIRED QUERIES EXECUTED:';
PRINT '1. All Apartments with Address (sorted by size desc, apartment number asc)';
PRINT '2. All Apartments with Leases (including apartments without leases)';
PRINT '3. All Leases with Revenue Calculation (CORRECTED lease duration)';
PRINT '';
PRINT 'NOTE: Query 3 uses corrected lease duration calculation for accurate month counting.';
PRINT 'Example: Lease from 2021-10-01 to 2022-09-30 = 12 months (not 11 months).';
