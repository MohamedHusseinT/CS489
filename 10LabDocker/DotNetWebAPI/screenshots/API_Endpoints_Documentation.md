# ADS Dental Surgeries Web API - Endpoints Documentation

## Base URL
- **Development**: `https://localhost:7000` or `http://localhost:5000`
- **Swagger UI**: `https://localhost:7000` (root URL)

## Required API Endpoints

### 1. GET All Patients
- **URL**: `GET /adsweb/api/v1/patients`
- **Description**: Displays the list of all Patients, including their primaryAddresses, sorted in ascending order by their lastName, in JSON format.
- **Response**: Array of Patient objects with Address and Appointments

### 2. GET Patient by ID
- **URL**: `GET /adsweb/api/v1/patients/{id}`
- **Description**: Displays the data for Patient whose PatientId is {id} including the primaryAddress, in JSON format. Includes appropriate exception handling for invalid patientId.
- **Response**: Single Patient object with Address and Appointments
- **Error**: 404 Not Found if patient doesn't exist

### 3. POST Create Patient
- **URL**: `POST /adsweb/api/v1/patients`
- **Description**: Register a new Patient into the system.
- **Request Body**: PatientRequest JSON object
- **Response**: Created Patient object with Address
- **Status**: 201 Created

### 4. PUT Update Patient
- **URL**: `PUT /adsweb/api/v1/patient/{id}`
- **Description**: Retrieves and updates Patient data for the patient whose patientId is {id}. Includes appropriate exception handling for invalid patientId.
- **Request Body**: PatientRequest JSON object
- **Response**: Updated Patient object with Address
- **Error**: 404 Not Found if patient doesn't exist

### 5. DELETE Patient
- **URL**: `DELETE /adsweb/api/v1/patient/{id}`
- **Description**: Deletes the Patient data for the patient whose patientId is {id}.
- **Response**: 204 No Content
- **Error**: 404 Not Found if patient doesn't exist

### 6. Search Patients
- **URL**: `GET /adsweb/api/v1/patient/search/{searchString}`
- **Description**: Queries all the Patient data for the patient(s) whose data matches the input searchString.
- **Response**: Array of Patient objects matching search criteria

### 7. GET All Addresses
- **URL**: `GET /adsweb/api/v1/addresses`
- **Description**: Displays the list of all Addresses, including the Patient data, sorted in ascending order by their city, in JSON format.
- **Response**: Array of Address objects with Patients and Surgeries

## Sample Data
The API comes pre-populated with sample data from Lab 6:
- 5 Addresses (Phoenix, AZ)
- 3 Surgeries (S10, S13, S15)
- 3 Dentists (D001, D002, D003)
- 4 Patients (P100, P105, P108, P110)
- 6 Appointments

## Testing Instructions
1. Start the application: `dotnet run`
2. Open Swagger UI at: `https://localhost:7000`
3. Test each endpoint using Swagger UI or Postman
4. Take screenshots of each API response
5. Save screenshots in the `screenshots` folder

## Error Handling
- **404 Not Found**: When patient/address ID doesn't exist
- **400 Bad Request**: When request validation fails
- **500 Internal Server Error**: When unexpected errors occur

## CORS Configuration
- All origins, methods, and headers are allowed for development
- Configure appropriately for production use
