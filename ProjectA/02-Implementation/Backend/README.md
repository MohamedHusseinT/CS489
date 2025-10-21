# ADS Dental Surgeries Web API

## Lab Assignment 7 - RESTful Web API Implementation

This project implements a RESTful Web API solution for the ADS Dental Surgeries Appointments management system using ASP.NET Core Web API.

## Project Structure

```
ADSDentalSurgeriesWebAPI/
├── Controllers/          # API Controllers
│   ├── PatientController.cs
│   └── AddressController.cs
├── Data/                # Entity Framework DbContext
│   └── ADSDbContext.cs
├── DTOs/                # Data Transfer Objects
│   ├── AddressResponse.cs
│   ├── PatientRequest.cs
│   ├── PatientResponse.cs
│   ├── SurgeryResponse.cs
│   └── AppointmentSimpleResponse.cs
├── Exceptions/          # Custom Exception Classes
│   ├── PatientNotFoundException.cs
│   └── AddressNotFoundException.cs
├── Models/              # Domain Models
│   ├── Address.cs
│   ├── Patient.cs
│   ├── Surgery.cs
│   ├── Dentist.cs
│   └── Appointment.cs
├── Services/            # Business Logic Services
│   ├── PatientService.cs
│   └── AddressService.cs
├── screenshots/         # API Testing Screenshots
│   ├── API_Endpoints_Documentation.md
│   └── ADS_Dental_Surgeries_API.postman_collection.json
└── Program.cs           # Application Configuration
```

## Required API Endpoints

### 1. GET All Patients
- **URL**: `GET /adsweb/api/v1/patients`
- **Description**: List all patients with addresses, sorted by lastName
- **Response**: Array of Patient objects

### 2. GET Patient by ID
- **URL**: `GET /adsweb/api/v1/patients/{id}`
- **Description**: Get specific patient with address
- **Response**: Single Patient object
- **Error**: 404 if not found

### 3. POST Create Patient
- **URL**: `POST /adsweb/api/v1/patients`
- **Description**: Register new patient
- **Request**: PatientRequest JSON
- **Response**: Created Patient object
- **Status**: 201 Created

### 4. PUT Update Patient
- **URL**: `PUT /adsweb/api/v1/patient/{id}`
- **Description**: Update existing patient
- **Request**: PatientRequest JSON
- **Response**: Updated Patient object
- **Error**: 404 if not found

### 5. DELETE Patient
- **URL**: `DELETE /adsweb/api/v1/patient/{id}`
- **Description**: Delete patient
- **Response**: 204 No Content
- **Error**: 404 if not found

### 6. Search Patients
- **URL**: `GET /adsweb/api/v1/patient/search/{searchString}`
- **Description**: Search patients by name, number, email, or phone
- **Response**: Array of matching Patient objects

### 7. GET All Addresses
- **URL**: `GET /adsweb/api/v1/addresses`
- **Description**: List all addresses with patients, sorted by city
- **Response**: Array of Address objects

## Technology Stack

- **Framework**: ASP.NET Core 8.0 Web API
- **Database**: SQLite (Entity Framework Core)
- **ORM**: Entity Framework Core
- **Documentation**: Swagger/OpenAPI
- **CORS**: Enabled for development

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio Code or Visual Studio 2022

### Running the Application

1. **Clone/Download** the project
2. **Navigate** to the project directory:
   ```bash
   cd ADSDentalSurgeriesWebAPI
   ```
3. **Restore** packages:
   ```bash
   dotnet restore
   ```
4. **Build** the project:
   ```bash
   dotnet build
   ```
5. **Run** the application:
   ```bash
   dotnet run
   ```
6. **Open** Swagger UI at: `https://localhost:7000`

### Database
- SQLite database file: `ads_dental_surgeries_webapi.db`
- Database is automatically created and seeded with sample data
- Sample data includes 5 addresses, 3 surgeries, 3 dentists, 4 patients, and 6 appointments

## Testing the API

### Using Swagger UI
1. Start the application
2. Navigate to `https://localhost:7000`
3. Use the interactive Swagger UI to test endpoints

### Using Postman
1. Import the collection: `screenshots/ADS_Dental_Surgeries_API.postman_collection.json`
2. Set base URL to: `https://localhost:7000`
3. Test each endpoint

### Sample Request Bodies

#### Create Patient
```json
{
  "patientNumber": "P999",
  "firstName": "Test",
  "lastName": "Patient",
  "phoneNumber": "(602) 555-9999",
  "email": "test.patient@email.com",
  "dateOfBirth": "1990-01-01T00:00:00Z",
  "mailingAddress": "123 Test Street",
  "addressId": 1
}
```

#### Update Patient
```json
{
  "patientNumber": "P100",
  "firstName": "Gillian",
  "lastName": "White",
  "phoneNumber": "(602) 555-0301",
  "email": "gillian.white.updated@email.com",
  "dateOfBirth": "1985-05-15T00:00:00Z",
  "mailingAddress": "123 Main Street, Updated",
  "addressId": 1
}
```

## Error Handling

- **404 Not Found**: When patient/address ID doesn't exist
- **400 Bad Request**: When request validation fails
- **500 Internal Server Error**: When unexpected errors occur

## Sample Data

The application comes pre-populated with sample data:

### Patients
- P100: Gillian White
- P105: Jill Bell  
- P108: Ian MacKay
- P110: John Walker

### Addresses
- 5 addresses in Phoenix, AZ
- Street addresses: Main Street, Oak Avenue, Pine Road, Elm Street, Maple Drive

### Surgeries
- S10: Phoenix Central Dental
- S13: Phoenix North Dental
- S15: Phoenix South Dental

### Dentists
- D001: Tony Smith (General Dentistry)
- D002: Helen Pearson (Orthodontics)
- D003: Robin Plevin (Oral Surgery)

## Screenshots

Take screenshots of each API endpoint response and save them in the `screenshots` folder for submission.

## Submission

1. Commit and push to GitHub
2. Submit the GitHub URL to Sakai
3. Include screenshots of API responses

## Lab Assignment Requirements Met

✅ **Spring Boot Alternative**: Implemented using ASP.NET Core Web API  
✅ **RESTful Endpoints**: All 7 required endpoints implemented  
✅ **JSON Format**: All responses in JSON format  
✅ **Exception Handling**: Proper error handling for invalid IDs  
✅ **Sorting**: Patients by lastName, Addresses by city  
✅ **Search Functionality**: Patient search by multiple fields  
✅ **CRUD Operations**: Create, Read, Update, Delete for Patients  
✅ **Sample Data**: Pre-populated with Lab 6 data  
✅ **Documentation**: Swagger UI and Postman collection  
✅ **Screenshots**: Folder prepared for API testing screenshots  

## Author

**Student**: Mohamed  
**Course**: CS489 Applied Software Development  
**Lab**: Assignment 7 - RESTful Web API  
**Date**: October 2025
