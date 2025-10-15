# ADS Dental Surgeries Secure API - Lab 9

## CS489 Applied Software Development - Token-based Authentication & Role-based Authorization

### Overview
This is a comprehensive RESTful Web API for managing ADS Dental Surgeries operations with **JWT-based Authentication** and **Role-based Authorization** using ASP.NET Core, Entity Framework Core, and SQLite database.

### 🔐 Security Features
- **JWT Authentication**: Secure token-based authentication
- **Password Hashing**: BCrypt for secure password storage
- **Role-based Authorization**: Four user roles with different access levels
- **Token Expiration**: 24-hour token validity
- **Secure Endpoints**: Protected API endpoints requiring authentication

### 👥 User Roles
1. **ADMIN**: Full access to all endpoints
2. **DENTIST**: Access to patient records and appointments
3. **RECEPTIONIST**: Access to patient and address management
4. **USER**: Basic authenticated access

### 🔑 Default Users
| Username | Password | Role | Email |
|----------|----------|------|-------|
| admin | admin123 | ADMIN | admin@ads.com |
| tony.smith | password123 | DENTIST | tony.smith@ads.com |
| helen.pearson | password123 | DENTIST | helen.pearson@ads.com |
| receptionist | password123 | RECEPTIONIST | receptionist@ads.com |

### 🚀 Getting Started

#### Prerequisites
- .NET 8.0 SDK
- Postman or similar REST client (optional)

#### Running the Application
1. Navigate to the project directory:
   ```bash
   cd ADSDentalSurgeriesSecureAPI
   ```

2. Restore packages and build:
   ```bash
   dotnet restore
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run --urls "http://localhost:5000"
   ```

4. Access Swagger UI:
   ```
   http://localhost:5000
   ```

### 📚 API Endpoints

#### Authentication Endpoints (No Auth Required)
```
POST   /adsweb/api/v1/auth/login        - Login with username/email and password
POST   /adsweb/api/v1/auth/register     - Register new user account
GET    /adsweb/api/v1/auth/health       - Health check endpoint
```

#### Authenticated Endpoints (Requires JWT Token)
```
GET    /adsweb/api/v1/auth/me           - Get current user information

GET    /adsweb/api/v1/patients          - List all patients (DENTIST, RECEPTIONIST, ADMIN)
GET    /adsweb/api/v1/patients/{id}     - Get patient by ID (DENTIST, RECEPTIONIST, ADMIN)
POST   /adsweb/api/v1/patients          - Create new patient (DENTIST, RECEPTIONIST, ADMIN)
PUT    /adsweb/api/v1/patients/patient/{id} - Update patient (DENTIST, RECEPTIONIST, ADMIN)
DELETE /adsweb/api/v1/patients/patient/{id} - Delete patient (DENTIST, RECEPTIONIST, ADMIN)
GET    /adsweb/api/v1/patients/patient/search/{searchString} - Search patients

GET    /adsweb/api/v1/addresses         - List all addresses (ADMIN, RECEPTIONIST)
GET    /adsweb/api/v1/addresses/{id}    - Get address by ID (ADMIN, RECEPTIONIST)
```

### 🧪 Testing the API

#### 1. Login (Get JWT Token)
```bash
curl -X POST http://localhost:5000/adsweb/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "usernameOrEmail": "admin",
    "password": "admin123"
  }'
```

**Response:**
```json
{
  "success": true,
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-10-16T02:00:00Z",
  "user": {
    "userId": 1,
    "username": "admin",
    "email": "admin@ads.com",
    "fullName": "System Administrator",
    "roles": ["ADMIN"]
  }
}
```

#### 2. Use Token to Access Protected Endpoints
```bash
curl -X GET http://localhost:5000/adsweb/api/v1/patients \
  -H "Authorization: Bearer YOUR_JWT_TOKEN_HERE"
```

#### 3. Register New User
```bash
curl -X POST http://localhost:5000/adsweb/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "newuser",
    "email": "newuser@ads.com",
    "password": "password123",
    "firstName": "New",
    "lastName": "User"
  }'
```

### 🔒 Using Swagger UI with JWT

1. **Login**: Use the `/adsweb/api/v1/auth/login` endpoint to get a JWT token
2. **Authorize**: Click the "Authorize" button in Swagger UI
3. **Enter Token**: Type `Bearer YOUR_TOKEN_HERE` in the value field
4. **Test Endpoints**: Now you can test protected endpoints

### 📦 Technology Stack
- **Backend**: ASP.NET Core 8.0 Web API
- **Database**: SQLite with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Password Hashing**: BCrypt.Net
- **API Documentation**: Swagger/OpenAPI
- **Authorization**: Role-based access control

### 🏗️ Project Structure
```
ADSDentalSurgeriesSecureAPI/
├── Controllers/          # API Controllers
│   ├── AuthController.cs       # Authentication endpoints
│   ├── PatientController.cs    # Patient CRUD (protected)
│   └── AddressController.cs    # Address CRUD (protected)
├── Models/              # Domain Models
│   ├── User.cs           # User entity
│   ├── Role.cs           # Role entity
│   ├── UserRole.cs       # User-Role mapping
│   ├── Patient.cs        # Patient entity
│   ├── Address.cs        # Address entity
│   └── ...
├── DTOs/                # Data Transfer Objects
│   ├── LoginRequest.cs
│   ├── RegisterRequest.cs
│   ├── AuthResponse.cs
│   └── ...
├── Services/            # Business Logic
│   ├── JwtService.cs     # JWT token generation/validation
│   ├── AuthService.cs    # Authentication logic
│   ├── PatientService.cs
│   └── AddressService.cs
├── Data/               # Data Access
│   └── ADSDbContext.cs  # Entity Framework DbContext
└── Program.cs          # Application configuration

```

### 🔐 Security Implementation

#### JWT Configuration (appsettings.json)
```json
{
  "Jwt": {
    "SecretKey": "ADS_Dental_Surgeries_Super_Secret_Key_For_JWT_Authentication_2025_Lab09_Security",
    "Issuer": "ADSDentalSurgeriesAPI",
    "Audience": "ADSDentalSurgeriesClient",
    "ExpirationHours": "24"
  }
}
```

#### Authorization Policies
- **AdminOnly**: Requires ADMIN role
- **DentistOnly**: Requires DENTIST or ADMIN role
- **ReceptionistOnly**: Requires RECEPTIONIST or ADMIN role
- **AuthenticatedUser**: Requires any authenticated user

### 📊 Database Schema
The application includes the following entities:
- **Users**: Application users with authentication
- **Roles**: User roles (ADMIN, DENTIST, RECEPTIONIST, USER)
- **UserRoles**: Many-to-many relationship between Users and Roles
- **Patients**: Dental patients information
- **Addresses**: Address records
- **Dentists**: Dentist information
- **Surgeries**: Dental surgery locations
- **Appointments**: Patient appointments

### 🎯 Lab Requirements Met
✅ Token-based User Authentication using JWT  
✅ Role-based Authorization with multiple roles  
✅ Secure password hashing with BCrypt  
✅ Protected API endpoints  
✅ User registration and login  
✅ Backward compatibility (public authentication endpoints)  
✅ Swagger UI with JWT support  
✅ Comprehensive error handling  
✅ Seed data for testing  

### 📝 API Response Formats

#### Success Response
```json
{
  "success": true,
  "message": "Operation successful",
  "data": { ... }
}
```

#### Error Response
```json
{
  "success": false,
  "message": "Error description",
  "error": "Detailed error message"
}
```

#### 401 Unauthorized
When accessing protected endpoints without a valid token:
```json
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Unauthorized",
  "status": 401
}
```

### 🔧 Development Notes
- The API supports CORS for cross-origin requests
- Database is automatically created and seeded on startup
- All passwords are hashed using BCrypt before storage
- JWT tokens are validated on every protected endpoint request
- Token expiration is set to 24 hours by default

### 📸 Screenshots
Screenshots of the API in action are available in the `screenshots/` folder, demonstrating:
- Swagger UI with JWT authentication
- Login and registration endpoints
- Protected endpoints with authorization
- Role-based access control
- Error handling for unauthorized access

### 🎓 Course Information
- **Course**: CS489 Applied Software Development
- **Assignment**: Lab 9 - Token-based Authentication & Role-based Authorization
- **Student**: Mohamed Hussein
- **Date**: October 2025

### 🚦 Testing Scenarios

#### Test 1: Public Endpoints (No Auth)
- ✅ Login with valid credentials
- ✅ Login with invalid credentials
- ✅ Register new user
- ✅ Health check

#### Test 2: Protected Endpoints (With Auth)
- ✅ Access with valid token
- ❌ Access without token (401 Unauthorized)
- ❌ Access with expired token (401 Unauthorized)
- ❌ Access with invalid token (401 Unauthorized)

#### Test 3: Role-based Access
- ✅ ADMIN can access all endpoints
- ✅ DENTIST can access patient endpoints
- ✅ RECEPTIONIST can access patient and address endpoints
- ❌ USER role has limited access

### 🔄 Comparison with Lab 7
| Feature | Lab 7 (No Security) | Lab 9 (With Security) |
|---------|-------------------|---------------------|
| Authentication | None | JWT-based |
| Authorization | None | Role-based |
| Password Storage | N/A | BCrypt hashed |
| Token Management | N/A | JWT with expiration |
| Public Endpoints | All | Only auth endpoints |
| Protected Endpoints | None | Patient, Address APIs |
| User Management | None | Full user CRUD |

### 📚 Additional Resources
- [JWT.io](https://jwt.io/) - JWT debugger and documentation
- [BCrypt](https://github.com/BcryptNet/bcrypt.net) - Password hashing library
- [ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

---

**Note**: This is a lab assignment implementation. In production, additional security measures should be implemented, such as:
- HTTPS enforcement
- Rate limiting
- Refresh tokens
- Password complexity requirements
- Account lockout policies
- Audit logging
- Security headers

