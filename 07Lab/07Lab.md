For this Lab Assignment 7, you are required to create a Spring Boot, Spring Web MVC-based Web application project and implement a RESTful Web API solution, for the ADS Dental Surgeries Appointments management system. Note that you can copy-over and use the backend, data access layer, domain model and service layer code from Lab 6.

In your solution, you are required to implement the following RESTful Web API endpoint URIs:

1. HTTP GET request: http://localhost:8080/adsweb/api/v1/patients - Displays the list of all Patients, including their primaryAddresses, sorted in ascending order by their lastName, in JSON format.

2. HTTP GET request: http://localhost:8080/adsweb/api/v1/patients/1 - Displays the data for Patient whose PatientId is 1 including the primaryAddress, in JSON format. Also, make sure to implement appropriate exception handling, for where patientId is invalid and not found.

3. HTTP POST request: http://localhost:8080/adsweb/api/v1/patients - Register a new Patient into the system. Note: You supply the correct/appropriate Patient data in JSON format

4. HTTP PUT request: http://localhost:8080/adsweb/api/v1/patient/1 - Retrieves and updates Patient data for the patient whose patientId is 1 (or any other valid patientId). Also, make sure to implement appropriate exception handling, for where patientId is invalid and not found.

5. HTTP DELETE request: http://localhost:8080/adsweb/api/v1/patient/1 - Deletes the Patient data for the patient whose patientId is 1 (or any other valid patientId). 

6. http://localhost:8080/adsweb/api/v1/patient/search/{searchString} - Queries all the Patient data for the patient(s) whose data matches the input searchString.

7. HTTP GET request: http://localhost:8080/adsweb/api/v1/addresses - Displays the list of all Addresses, including the Patient data, sorted in ascending order by their city, in JSON format.

Use my example from the CityLibraryWebAPI project from lesson7-rest-webapi.zip, which is attached for you here in zip file), as a guide.

When complete: Take screenshots of your Web API output screens using the Postman tool (or some other webclient tool) and add them to a subfolder inside your project named screenshots.

For submission, simply commit and push your solution to your github and submit the url here to this Lab Assignment item on Sakai.

ALTERNATIVELY: You work on implementing the Web API endpoints for your chosen Course Project.

Enjoy!!!


