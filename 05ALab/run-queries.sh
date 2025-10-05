#!/bin/bash

# ADS Dental Surgery Database - Query Execution Script
# Lab 05A - CS489 Applied Software Development
# Student: Mohamed

echo "🏥 Advantis Dental Surgeries (ADS) Database - Query Results"
echo "============================================================"
echo "Database: ADSDentalSurgery.db (SQLite)"
echo ""

# Query 1: All Dentists (sorted by last name)
echo "1️⃣ QUERY 1: All Dentists (sorted by last name)"
echo "=============================================="
sqlite3 ADSDentalSurgery.db "
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
"
echo ""

# Query 2: Appointments for Dentist ID = 1 (Tony Smith)
echo "2️⃣ QUERY 2: Appointments for Dentist ID = 1 (Tony Smith)"
echo "========================================================"
sqlite3 ADSDentalSurgery.db "
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
WHERE a.dentist_id = 1
ORDER BY a.appointment_date, a.appointment_time;
"
echo ""

# Query 3: Appointments at Surgery Location ID = 1
echo "3️⃣ QUERY 3: Appointments at Surgery Location ID = 1 (ADS Downtown Surgery)"
echo "=========================================================================="
sqlite3 ADSDentalSurgery.db "
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
WHERE a.surgery_id = 1
ORDER BY a.appointment_date, a.appointment_time;
"
echo ""

# Query 4: Patient Appointments on Specific Date
echo "4️⃣ QUERY 4: Patient Appointments on Specific Date (Patient ID = 3, Date = 2013-09-12)"
echo "====================================================================================="
sqlite3 ADSDentalSurgery.db "
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
WHERE a.patient_id = 3 AND a.appointment_date = '2013-09-12'
ORDER BY a.appointment_time;
"
echo ""

echo "✅ All queries executed successfully!"
echo "📸 Take screenshots of these results for submission"
echo "📁 Database file: ADSDentalSurgery.db"

