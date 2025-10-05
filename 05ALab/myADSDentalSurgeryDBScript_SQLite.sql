-- =====================================================
-- Advantis Dental Surgeries (ADS) Database Script - SQLite Version
-- Lab 05A - CS489 Applied Software Development
-- Student: Mohamed
-- =====================================================

-- SQLite version for macOS users
-- This script creates the same database structure as SQL Server version

-- =====================================================
-- 1. CREATE TABLES
-- =====================================================

-- Dentists Table
CREATE TABLE Dentists (
    dentist_id INTEGER PRIMARY KEY AUTOINCREMENT,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    contact_phone TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    specialization TEXT NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Patients Table
CREATE TABLE Patients (
    patient_id INTEGER PRIMARY KEY AUTOINCREMENT,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    contact_phone TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    mailing_address TEXT NOT NULL,
    date_of_birth DATE NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Surgeries Table
CREATE TABLE Surgeries (
    surgery_id INTEGER PRIMARY KEY AUTOINCREMENT,
    surgery_name TEXT NOT NULL,
    location_address TEXT NOT NULL,
    telephone TEXT NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Appointments Table
CREATE TABLE Appointments (
    appointment_id INTEGER PRIMARY KEY AUTOINCREMENT,
    dentist_id INTEGER NOT NULL,
    patient_id INTEGER NOT NULL,
    surgery_id INTEGER NOT NULL,
    appointment_date DATE NOT NULL,
    appointment_time TIME NOT NULL,
    status TEXT DEFAULT 'Scheduled' CHECK (status IN ('Scheduled', 'Completed', 'Cancelled', 'No-Show')),
    notes TEXT,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (dentist_id) REFERENCES Dentists(dentist_id),
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id),
    FOREIGN KEY (surgery_id) REFERENCES Surgeries(surgery_id)
);

-- Bills Table (for tracking outstanding payments)
CREATE TABLE Bills (
    bill_id INTEGER PRIMARY KEY AUTOINCREMENT,
    patient_id INTEGER NOT NULL,
    appointment_id INTEGER,
    amount DECIMAL(10,2) NOT NULL,
    bill_date DATE NOT NULL,
    due_date DATE NOT NULL,
    status TEXT DEFAULT 'Outstanding' CHECK (status IN ('Outstanding', 'Paid', 'Overdue')),
    description TEXT,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id),
    FOREIGN KEY (appointment_id) REFERENCES Appointments(appointment_id)
);

-- =====================================================
-- 2. CREATE INDEXES FOR PERFORMANCE
-- =====================================================

-- Indexes for Dentists
CREATE INDEX idx_dentists_lastname ON Dentists(last_name);
CREATE INDEX idx_dentists_email ON Dentists(email);

-- Indexes for Patients
CREATE INDEX idx_patients_lastname ON Patients(last_name);
CREATE INDEX idx_patients_email ON Patients(email);

-- Indexes for Appointments
CREATE INDEX idx_appointments_dentist ON Appointments(dentist_id);
CREATE INDEX idx_appointments_patient ON Appointments(patient_id);
CREATE INDEX idx_appointments_surgery ON Appointments(surgery_id);
CREATE INDEX idx_appointments_date ON Appointments(appointment_date);
CREATE INDEX idx_appointments_datetime ON Appointments(appointment_date, appointment_time);

-- Indexes for Bills
CREATE INDEX idx_bills_patient ON Bills(patient_id);
CREATE INDEX idx_bills_status ON Bills(status);

-- =====================================================
-- 3. INSERT SAMPLE DATA
-- =====================================================

-- Insert Dentists
INSERT INTO Dentists (first_name, last_name, contact_phone, email, specialization) VALUES
('Tony', 'Smith', '(555) 123-4567', 'tony.smith@ads.com', 'General Dentistry'),
('Helen', 'Pearson', '(555) 234-5678', 'helen.pearson@ads.com', 'Orthodontics'),
('Robin', 'Plevin', '(555) 345-6789', 'robin.plevin@ads.com', 'Oral Surgery');

-- Insert Patients
INSERT INTO Patients (first_name, last_name, contact_phone, email, mailing_address, date_of_birth) VALUES
('Gillian', 'White', '(555) 111-2222', 'gillian.white@email.com', '123 Main St, Burlington, VT 05401', '1985-03-15'),
('Jill', 'Bell', '(555) 222-3333', 'jill.bell@email.com', '456 Oak Ave, Burlington, VT 05401', '1990-07-22'),
('Ian', 'MacKay', '(555) 333-4444', 'ian.mackay@email.com', '789 Pine St, Burlington, VT 05401', '1982-11-08'),
('John', 'Walker', '(555) 444-5555', 'john.walker@email.com', '321 Elm St, Burlington, VT 05401', '1978-05-30');

-- Insert Surgeries
INSERT INTO Surgeries (surgery_name, location_address, telephone) VALUES
('ADS Downtown Surgery', '100 Church St, Burlington, VT 05401', '(555) 100-1001'),
('ADS North End Surgery', '200 North Ave, Burlington, VT 05401', '(555) 200-2002'),
('ADS South Side Surgery', '300 South St, Burlington, VT 05401', '(555) 300-3003');

-- Insert Appointments (based on sample data)
INSERT INTO Appointments (dentist_id, patient_id, surgery_id, appointment_date, appointment_time, status) VALUES
(1, 1, 2, '2013-09-12', '10:00:00', 'Completed'),  -- Tony Smith, Gillian White, S15 (mapped to surgery_id 2)
(1, 2, 2, '2013-09-12', '12:00:00', 'Completed'),  -- Tony Smith, Jill Bell, S15
(2, 3, 1, '2013-09-12', '10:00:00', 'Completed'),  -- Helen Pearson, Ian MacKay, S10 (mapped to surgery_id 1)
(2, 3, 1, '2013-09-14', '14:00:00', 'Completed'),  -- Helen Pearson, Ian MacKay, S10
(3, 2, 2, '2013-09-14', '16:30:00', 'Completed'),  -- Robin Plevin, Jill Bell, S15
(3, 4, 3, '2013-09-15', '18:00:00', 'Completed');  -- Robin Plevin, John Walker, S13 (mapped to surgery_id 3)

-- Insert Sample Bills
INSERT INTO Bills (patient_id, appointment_id, amount, bill_date, due_date, status, description) VALUES
(1, 1, 150.00, '2013-09-12', '2013-10-12', 'Paid', 'Routine cleaning and checkup'),
(2, 2, 200.00, '2013-09-12', '2013-10-12', 'Outstanding', 'Dental filling'),
(3, 3, 300.00, '2013-09-12', '2013-10-12', 'Paid', 'Orthodontic consultation'),
(3, 4, 500.00, '2013-09-14', '2013-10-14', 'Outstanding', 'Braces adjustment'),
(2, 5, 400.00, '2013-09-14', '2013-10-14', 'Outstanding', 'Oral surgery consultation'),
(4, 6, 250.00, '2013-09-15', '2013-10-15', 'Paid', 'Tooth extraction');

-- =====================================================
-- 4. REQUIRED SQL QUERIES
-- =====================================================

-- Query 1: Display the list of ALL Dentists registered in the system, 
-- sorted in ascending order of their lastNames
SELECT 
    dentist_id,
    first_name,
    last_name,
    contact_phone,
    email,
    specialization,
    created_date
FROM Dentists
ORDER BY last_name ASC;

-- Query 2: Display the list of ALL Appointments for a given Dentist by their dentist_Id number. 
-- Include in the result, the Patient information.
-- Example: Get appointments for dentist_id = 1 (Tony Smith)
SELECT 
    a.appointment_id,
    a.appointment_date,
    a.appointment_time,
    a.status,
    p.patient_id,
    p.first_name AS patient_first_name,
    p.last_name AS patient_last_name,
    p.contact_phone AS patient_phone,
    p.email AS patient_email,
    p.mailing_address AS patient_address,
    p.date_of_birth AS patient_dob,
    s.surgery_name,
    s.location_address AS surgery_address
FROM Appointments a
INNER JOIN Patients p ON a.patient_id = p.patient_id
INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id
WHERE a.dentist_id = 1  -- Replace with desired dentist_id
ORDER BY a.appointment_date, a.appointment_time;

-- Query 3: Display the list of ALL Appointments that have been scheduled at a Surgery Location
-- Example: Get appointments for surgery_id = 1 (ADS Downtown Surgery)
SELECT 
    a.appointment_id,
    a.appointment_date,
    a.appointment_time,
    a.status,
    d.first_name || ' ' || d.last_name AS dentist_name,
    d.specialization,
    p.first_name || ' ' || p.last_name AS patient_name,
    p.contact_phone AS patient_phone,
    p.email AS patient_email
FROM Appointments a
INNER JOIN Dentists d ON a.dentist_id = d.dentist_id
INNER JOIN Patients p ON a.patient_id = p.patient_id
WHERE a.surgery_id = 1  -- Replace with desired surgery_id
ORDER BY a.appointment_date, a.appointment_time;

-- Query 4: Display the list of the Appointments booked for a given Patient on a given Date
-- Example: Get appointments for patient_id = 3 (Ian MacKay) on 2013-09-12
SELECT 
    a.appointment_id,
    a.appointment_time,
    a.status,
    d.first_name || ' ' || d.last_name AS dentist_name,
    d.specialization,
    d.contact_phone AS dentist_phone,
    d.email AS dentist_email,
    s.surgery_name,
    s.location_address AS surgery_address,
    s.telephone AS surgery_phone
FROM Appointments a
INNER JOIN Dentists d ON a.dentist_id = d.dentist_id
INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id
WHERE a.patient_id = 3  -- Replace with desired patient_id
AND a.appointment_date = '2013-09-12'  -- Replace with desired date
ORDER BY a.appointment_time;

-- =====================================================
-- 5. ADDITIONAL USEFUL QUERIES
-- =====================================================

-- Query to get all appointments with full details
SELECT 
    a.appointment_id,
    a.appointment_date,
    a.appointment_time,
    a.status,
    d.first_name || ' ' || d.last_name AS dentist_name,
    d.specialization,
    p.first_name || ' ' || p.last_name AS patient_name,
    p.contact_phone AS patient_phone,
    p.email AS patient_email,
    s.surgery_name,
    s.location_address AS surgery_address,
    s.telephone AS surgery_phone
FROM Appointments a
INNER JOIN Dentists d ON a.dentist_id = d.dentist_id
INNER JOIN Patients p ON a.patient_id = p.patient_id
INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id
ORDER BY a.appointment_date, a.appointment_time;

-- Query to find patients with outstanding bills
SELECT 
    p.patient_id,
    p.first_name || ' ' || p.last_name AS patient_name,
    p.email,
    COUNT(b.bill_id) AS outstanding_bills_count,
    SUM(b.amount) AS total_outstanding_amount
FROM Patients p
LEFT JOIN Bills b ON p.patient_id = b.patient_id AND b.status = 'Outstanding'
GROUP BY p.patient_id, p.first_name, p.last_name, p.email
HAVING outstanding_bills_count > 0
ORDER BY total_outstanding_amount DESC;

-- Query to get dentist weekly appointment summary
SELECT 
    d.first_name || ' ' || d.last_name AS dentist_name,
    a.appointment_date,
    COUNT(*) AS appointments_count
FROM Dentists d
INNER JOIN Appointments a ON d.dentist_id = a.dentist_id
WHERE a.appointment_date >= date('now', '-7 days')
GROUP BY d.dentist_id, d.first_name, d.last_name, a.appointment_date
ORDER BY d.last_name, a.appointment_date;

-- Query to get surgery utilization summary
SELECT 
    s.surgery_name,
    s.location_address,
    COUNT(a.appointment_id) AS total_appointments,
    COUNT(CASE WHEN a.status = 'Completed' THEN 1 END) AS completed_appointments,
    COUNT(CASE WHEN a.status = 'Scheduled' THEN 1 END) AS scheduled_appointments,
    COUNT(CASE WHEN a.status = 'Cancelled' THEN 1 END) AS cancelled_appointments
FROM Surgeries s
LEFT JOIN Appointments a ON s.surgery_id = a.surgery_id
GROUP BY s.surgery_id, s.surgery_name, s.location_address
ORDER BY total_appointments DESC;

-- =====================================================
-- END OF SCRIPT
-- =====================================================

-- Display success message
SELECT 'ADS Dental Surgery Database (SQLite) created successfully!' AS message;
SELECT 'Database: ADSDentalSurgery.db' AS database_file;
SELECT 'Tables: Dentists, Patients, Surgeries, Appointments, Bills' AS tables_created;
SELECT 'Sample data inserted successfully!' AS data_status;

