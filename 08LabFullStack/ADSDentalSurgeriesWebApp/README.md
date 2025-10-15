# ADS Dental Surgeries - Full-Stack Web Application

## Lab 8 - CS489 Applied Software Development

### Overview
This is a comprehensive full-stack web application for managing ADS Dental Surgeries operations, built using ASP.NET Core MVC with Entity Framework Core and SQLite database.

### Features
- **Patient Management**: Complete CRUD operations for patient records
- **Address Management**: Manage addresses for patients and surgeries
- **Modern UI**: Responsive design with Bootstrap 5 and Font Awesome icons
- **Search Functionality**: Search patients and addresses by various criteria
- **Data Relationships**: Properly linked entities with foreign key relationships
- **Validation**: Client-side and server-side validation
- **Error Handling**: Comprehensive error handling and user feedback

### Technology Stack
- **Backend**: ASP.NET Core 8.0 MVC
- **Database**: SQLite with Entity Framework Core
- **Frontend**: Bootstrap 5, Font Awesome, jQuery
- **Architecture**: MVC Pattern with Service Layer
- **Validation**: Data Annotations and jQuery Validation

### Project Structure
```
ADSDentalSurgeriesWebApp/
├── Controllers/          # MVC Controllers
│   ├── HomeController.cs
│   ├── PatientController.cs
│   └── AddressController.cs
├── Models/              # Domain Models
│   ├── Address.cs
│   ├── Patient.cs
│   ├── Surgery.cs
│   ├── Dentist.cs
│   └── Appointment.cs
├── ViewModels/          # View Models for UI
│   ├── PatientViewModel.cs
│   └── AddressViewModel.cs
├── Services/            # Business Logic Layer
│   ├── PatientService.cs
│   └── AddressService.cs
├── Data/               # Data Access Layer
│   └── ADSDbContext.cs
├── Views/              # Razor Views
│   ├── Home/
│   ├── Patient/
│   ├── Address/
│   └── Shared/
└── wwwroot/           # Static Files
```

### Database Schema
The application uses the following entities:
- **Addresses**: Street, City, State, ZipCode, Country
- **Patients**: PatientNumber, FirstName, LastName, Email, Phone, Address
- **Surgeries**: SurgeryNumber, Name, Phone, Email, Address
- **Dentists**: DentistNumber, FirstName, LastName, Specialization
- **Appointments**: AppointmentNumber, Date, Time, Patient, Dentist, Surgery

### Getting Started

#### Prerequisites
- .NET 8.0 SDK
- Visual Studio Code or Visual Studio

#### Running the Application
1. Navigate to the project directory:
   ```bash
   cd ADSDentalSurgeriesWebApp
   ```

2. Restore packages:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run --urls "http://localhost:5001"
   ```

4. Open your browser and navigate to:
   ```
   http://localhost:5001
   ```

### Application Pages

#### Home Page (`/`)
- Welcome dashboard with system overview
- Navigation to different sections
- Statistics display

#### Patient Management (`/Patient`)
- **Index**: List all patients with search functionality
- **Create**: Add new patient records
- **Details**: View complete patient information
- **Edit**: Update patient information
- **Delete**: Remove patient records

#### Address Management (`/Address`)
- **Index**: List all addresses with search functionality
- **Create**: Add new address records
- **Details**: View address with associated patients and surgeries
- **Edit**: Update address information
- **Delete**: Remove address records

### Key Features

#### Search Functionality
- Search patients by name, patient number, email, or phone
- Search addresses by street, city, state, or zip code
- Real-time filtering with clear search results

#### Responsive Design
- Mobile-friendly interface
- Bootstrap 5 components
- Font Awesome icons for better UX
- Consistent styling across all pages

#### Data Validation
- Required field validation
- Email format validation
- String length constraints
- Client-side and server-side validation

#### Error Handling
- User-friendly error messages
- Success/error notifications
- Graceful handling of database errors
- 404 handling for missing resources

### Sample Data
The application comes pre-loaded with sample data:
- 5 addresses in Phoenix, AZ
- 4 patients with contact information
- 3 dental surgeries
- 3 dentists with specializations
- 6 sample appointments

### Development Notes
- Uses Entity Framework Core with SQLite for development
- Implements Repository pattern through services
- Follows MVC architecture principles
- Uses ViewModels for clean separation of concerns
- Implements proper error handling and logging

### Course Information
- **Course**: CS489 Applied Software Development
- **Assignment**: Lab 8 - Full-Stack Web Application
- **Student**: Mohamed Hussein
- **Date**: October 2025

### Screenshots
Screenshots of the application are available in the `screenshots/` folder, demonstrating:
- Home page with dashboard
- Patient management interface
- Address management interface
- Create/Edit forms
- Search functionality
- Responsive design

### Future Enhancements
- Appointment scheduling interface
- Dentist management
- Surgery management
- Reporting and analytics
- User authentication and authorization
- API integration with external systems
