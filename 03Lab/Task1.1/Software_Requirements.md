# Software Requirements Discovery - Advantis Dental Surgeries (ADS)

## Problem Statement Analysis
Advantis Dental Surgeries, LLC (ADS) requires a web-based software solution to manage their growing network of dental surgeries across the South West region. The system needs to handle dentist registration, patient enrollment, appointment management, and surgery information.

## Functional Requirements

### 1. User Management Requirements
1.1. The system shall allow Office Managers to register new Dentists in the network
1.2. The system shall assign a unique ID number to each registered Dentist
1.3. The system shall store Dentist information including First Name, Last Name, Contact Phone Number, Email, and Specialization
1.4. The system shall allow Office Managers to enroll new Patients requiring dental services
1.5. The system shall store Patient information including First Name, Last Name, Contact Phone Number, Email, Mailing Address, and Date of Birth

### 2. Authentication and Authorization Requirements
2.1. The system shall provide secure sign-in functionality for Dentists
2.2. The system shall provide secure sign-in functionality for Patients
2.3. The system shall provide secure sign-in functionality for Office Managers
2.4. The system shall maintain different access levels for different user types

### 3. Appointment Management Requirements
3.1. The system shall allow Patients to request appointments via phone calls
3.2. The system shall allow Patients to request appointments through online forms on the ADS website
3.3. The system shall allow Office Managers to book appointments upon receiving requests
3.4. The system shall send confirmation emails to Patients when appointments are booked
3.5. The system shall record all appointment details in the system
3.6. The system shall prevent Dentists from having more than 5 appointments in any given week
3.7. The system shall prevent Patients from requesting new appointments if they have outstanding, unpaid bills

### 4. Appointment Viewing Requirements
4.1. The system shall allow Dentists to view a listing of all their appointments
4.2. The system shall display Patient details for each appointment to Dentists
4.3. The system shall allow Patients to view their appointments
4.4. The system shall display Dentist information for each appointment to Patients

### 5. Appointment Modification Requirements
5.1. The system shall allow Patients to request appointment cancellations
5.2. The system shall allow Patients to request appointment changes
5.3. The system shall process appointment modification requests through Office Managers

### 6. Surgery Information Requirements
6.1. The system shall store information about each Surgery location
6.2. The system shall display Surgery name, location address, and telephone number
6.3. The system shall associate appointments with specific Surgery locations

### 7. Billing and Payment Requirements
7.1. The system shall track outstanding, unpaid bills for dental services
7.2. The system shall prevent new appointment requests for Patients with unpaid bills
7.3. The system shall maintain billing history for each Patient

### 8. System Integration Requirements
8.1. The system shall integrate with email services for sending appointment confirmations
8.2. The system shall provide a web-based interface accessible through browsers
8.3. The system shall maintain data consistency across all operations

### 9. Data Management Requirements
9.1. The system shall maintain data integrity for all stored information
9.2. The system shall provide secure data storage and backup capabilities
9.3. The system shall ensure data privacy and compliance with healthcare regulations

### 10. Reporting and Analytics Requirements
10.1. The system shall generate reports on appointment schedules
10.2. The system shall provide analytics on Dentist workload and Patient appointments
10.3. The system shall track system usage and performance metrics

## Non-Functional Requirements

### Performance Requirements
- The system shall respond to user requests within 3 seconds
- The system shall support concurrent access by multiple users
- The system shall handle peak loads during business hours

### Security Requirements
- The system shall implement secure authentication mechanisms
- The system shall protect sensitive patient and dentist information
- The system shall comply with healthcare data protection regulations

### Usability Requirements
- The system shall provide an intuitive user interface
- The system shall be accessible on desktop and mobile devices
- The system shall provide clear navigation and user guidance

### Reliability Requirements
- The system shall maintain 99.9% uptime
- The system shall provide data backup and recovery capabilities
- The system shall handle system failures gracefully

## Business Rules

1. Each Dentist must have a unique ID number
2. A Dentist cannot have more than 5 appointments in any given week
3. Patients with outstanding, unpaid bills cannot request new appointments
4. All appointments must be associated with a specific Surgery location
5. Appointment confirmations must be sent via email to Patients
6. Only Office Managers can book appointments
7. Patients can only view and modify their own appointments
8. Dentists can only view their own appointments
