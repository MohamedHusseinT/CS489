# ADS Dental Surgery System - Software Requirements Discovery

## Functional Requirements

### 1. User Management System
- **F1.1**: The system shall allow Office Manager to register new Dentists with unique ID, First Name, Last Name, Contact Phone Number, Email, and Specialization
- **F1.2**: The system shall allow Office Manager to enroll new Patients with Patient's First Name, Last Name, Contact Phone Number, Email, Mailing Address, and Date of Birth
- **F1.3**: The system shall provide secure sign-in functionality for Dentists
- **F1.4**: The system shall provide secure sign-in functionality for Patients
- **F1.5**: The system shall maintain role-based access control (Office Manager, Dentist, Patient)

### 2. Appointment Management System
- **F2.1**: The system shall allow Patients to request appointments by calling-in to the office
- **F2.2**: The system shall allow Patients to request appointments by submitting online forms on the ADS website
- **F2.3**: The system shall allow Office Manager to book appointments for Patients with Dentists
- **F2.4**: The system shall automatically send confirmation emails to Patients when appointments are booked
- **F2.5**: The system shall record all appointment details including date, time, Patient, and Dentist
- **F2.6**: The system shall display appointment listings to Dentists showing their scheduled appointments and Patient details
- **F2.7**: The system shall display appointment listings to Patients showing their scheduled appointments and Dentist information
- **F2.8**: The system shall allow Patients to request cancellation or rescheduling of their appointments

### 3. Surgery Location Management
- **F3.1**: The system shall maintain information about each Surgery location including name, location address, and telephone number
- **F3.2**: The system shall associate appointments with specific Surgery locations
- **F3.3**: The system shall display Surgery location details in appointment confirmations

### 4. Business Rules and Constraints
- **F4.1**: The system shall prevent Dentists from having more than 5 appointments in any given week
- **F4.2**: The system shall prevent Patients from requesting new appointments if they have outstanding, unpaid bills for previous dental services
- **F4.3**: The system shall validate that appointment dates are in the future
- **F4.4**: The system shall ensure appointment scheduling respects office hours and Surgery availability

### 5. Reporting and Notifications
- **F5.1**: The system shall generate appointment confirmations via email
- **F5.2**: The system shall send appointment reminders to Patients
- **F5.3**: The system shall generate daily/weekly schedules for Dentists
- **F5.4**: The system shall generate appointment statistics and reports for Office Manager

## Non-Functional Requirements

### Performance Requirements
- **NF1**: The system shall respond to user requests within 2 seconds for 95% of operations
- **NF2**: The system shall support up to 100 concurrent users
- **NF3**: The system shall handle up to 1000 appointments per day

### Security Requirements
- **NF4**: The system shall encrypt sensitive data (patient information, appointment details)
- **NF5**: The system shall implement secure authentication and session management
- **NF6**: The system shall comply with healthcare data privacy regulations (HIPAA equivalent)

### Reliability Requirements
- **NF7**: The system shall have 99.5% uptime
- **NF8**: The system shall have automated backup and recovery procedures
- **NF9**: The system shall maintain data integrity and consistency

### Usability Requirements
- **NF10**: The system shall provide intuitive user interface for all user types
- **NF11**: The system shall be accessible on multiple devices (desktop, tablet, mobile)
- **NF12**: The system shall provide clear error messages and user guidance

### Scalability Requirements
- **NF13**: The system shall scale to support additional Surgery locations
- **NF14**: The system shall accommodate increasing number of Dentists and Patients
- **NF15**: The system architecture shall support future feature enhancements
