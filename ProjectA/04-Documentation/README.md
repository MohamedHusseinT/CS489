# Project Documentation

## Documentation Structure

### Technical Documentation
- **API Documentation**: Swagger/OpenAPI specifications
- **Database Documentation**: Schema and migration guides
- **Deployment Guides**: Step-by-step deployment instructions
- **Development Setup**: Local development environment setup
- **Testing Documentation**: Testing strategies and guidelines

### User Documentation
- **User Manual**: End-user guide for application features
- **Admin Guide**: Administrative functions and configuration
- **Troubleshooting**: Common issues and solutions
- **FAQ**: Frequently asked questions

### Project Documentation
- **Project Charter**: Project scope, objectives, and constraints
- **Requirements Traceability**: Mapping requirements to implementation
- **Change Log**: Version history and changes
- **Lessons Learned**: Project insights and improvements

## API Documentation

### Swagger/OpenAPI Specification
```yaml
# swagger.yml
openapi: 3.0.0
info:
  title: ProjectA API
  description: Enterprise Web Application API
  version: 1.0.0
  contact:
    name: ProjectA Team
    email: team@projecta.com

servers:
  - url: https://api.projecta.com/v1
    description: Production server
  - url: https://staging-api.projecta.com/v1
    description: Staging server

paths:
  /users:
    get:
      summary: Get all users
      description: Retrieve a list of all users
      tags:
        - Users
      parameters:
        - name: page
          in: query
          schema:
            type: integer
            default: 1
        - name: pageSize
          in: query
          schema:
            type: integer
            default: 10
      responses:
        '200':
          description: Successful response
          content:
            application/json:
              schema:
                type: object
                properties:
                  data:
                    type: array
                    items:
                      $ref: '#/components/schemas/User'
                  pagination:
                    $ref: '#/components/schemas/Pagination'

    post:
      summary: Create user
      description: Create a new user
      tags:
        - Users
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/CreateUserRequest'
      responses:
        '201':
          description: User created successfully
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/User'
        '400':
          description: Bad request
        '409':
          description: User already exists

components:
  schemas:
    User:
      type: object
      properties:
        id:
          type: integer
          example: 1
        email:
          type: string
          format: email
          example: user@example.com
        firstName:
          type: string
          example: John
        lastName:
          type: string
          example: Doe
        isActive:
          type: boolean
          example: true
        createdDate:
          type: string
          format: date-time
        updatedDate:
          type: string
          format: date-time

    CreateUserRequest:
      type: object
      required:
        - email
        - password
        - firstName
        - lastName
      properties:
        email:
          type: string
          format: email
        password:
          type: string
          minLength: 8
        firstName:
          type: string
          minLength: 2
          maxLength: 50
        lastName:
          type: string
          minLength: 2
          maxLength: 50
        phone:
          type: string
        address:
          type: string
          maxLength: 200

    Pagination:
      type: object
      properties:
        page:
          type: integer
        pageSize:
          type: integer
        totalCount:
          type: integer
        totalPages:
          type: integer
```

## Database Documentation

### Schema Documentation
```sql
-- Database Schema Documentation
-- ProjectA Database Schema v1.0

-- Users Table
-- Stores user authentication and basic information
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedDate DATETIME2,
    LastLoginDate DATETIME2,
    
    -- Indexes
    INDEX IX_Users_Email (Email),
    INDEX IX_Users_IsActive (IsActive),
    INDEX IX_Users_CreatedDate (CreatedDate)
);

-- Profiles Table
-- Stores user profile information
CREATE TABLE Profiles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL UNIQUE,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Address NVARCHAR(200),
    DateOfBirth DATE,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedDate DATETIME2,
    
    -- Foreign Keys
    CONSTRAINT FK_Profiles_UserId FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    
    -- Indexes
    INDEX IX_Profiles_UserId (UserId),
    INDEX IX_Profiles_FirstName (FirstName),
    INDEX IX_Profiles_LastName (LastName)
);

-- Roles Table
-- Defines system roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedDate DATETIME2,
    
    -- Indexes
    INDEX IX_Roles_Name (Name),
    INDEX IX_Roles_IsActive (IsActive)
);

-- Permissions Table
-- Defines system permissions
CREATE TABLE Permissions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    Resource NVARCHAR(100) NOT NULL,
    Action NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    
    -- Indexes
    INDEX IX_Permissions_Name (Name),
    INDEX IX_Permissions_Resource (Resource),
    INDEX IX_Permissions_Action (Action),
    INDEX IX_Permissions_IsActive (IsActive)
);

-- UserRoles Junction Table
-- Links users to roles
CREATE TABLE UserRoles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    AssignedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    AssignedBy INT,
    IsActive BIT NOT NULL DEFAULT 1,
    
    -- Foreign Keys
    CONSTRAINT FK_UserRoles_UserId FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserRoles_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserRoles_AssignedBy FOREIGN KEY (AssignedBy) REFERENCES Users(Id),
    
    -- Unique Constraints
    CONSTRAINT UQ_UserRoles_UserId_RoleId UNIQUE (UserId, RoleId),
    
    -- Indexes
    INDEX IX_UserRoles_UserId (UserId),
    INDEX IX_UserRoles_RoleId (RoleId),
    INDEX IX_UserRoles_IsActive (IsActive)
);

-- RolePermissions Junction Table
-- Links roles to permissions
CREATE TABLE RolePermissions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    
    -- Foreign Keys
    CONSTRAINT FK_RolePermissions_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE,
    CONSTRAINT FK_RolePermissions_PermissionId FOREIGN KEY (PermissionId) REFERENCES Permissions(Id) ON DELETE CASCADE,
    
    -- Unique Constraints
    CONSTRAINT UQ_RolePermissions_RoleId_PermissionId UNIQUE (RoleId, PermissionId),
    
    -- Indexes
    INDEX IX_RolePermissions_RoleId (RoleId),
    INDEX IX_RolePermissions_PermissionId (PermissionId)
);

-- AuditLogs Table
-- Tracks system changes
CREATE TABLE AuditLogs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    Action NVARCHAR(100) NOT NULL,
    Resource NVARCHAR(100) NOT NULL,
    ResourceId INT,
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX),
    IpAddress NVARCHAR(45),
    UserAgent NVARCHAR(500),
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    
    -- Foreign Keys
    CONSTRAINT FK_AuditLogs_UserId FOREIGN KEY (UserId) REFERENCES Users(Id),
    
    -- Indexes
    INDEX IX_AuditLogs_UserId (UserId),
    INDEX IX_AuditLogs_Action (Action),
    INDEX IX_AuditLogs_Resource (Resource),
    INDEX IX_AuditLogs_CreatedDate (CreatedDate)
);
```

## Development Setup Guide

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- PostgreSQL 13+
- Docker Desktop
- Git

### Local Development Setup
```bash
# 1. Clone repository
git clone https://github.com/your-org/projecta.git
cd projecta

# 2. Setup backend
cd 02-Implementation/Backend
dotnet restore
dotnet ef database update
dotnet run

# 3. Setup frontend
cd ../Frontend
npm install
npm run dev

# 4. Setup database
docker-compose up -d postgres redis

# 5. Run tests
dotnet test
npm test
```

### Environment Configuration
```json
// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProjectA_Dev;User Id=sa;Password=YourPassword123!;TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-here",
    "Issuer": "ProjectA",
    "Audience": "ProjectA-Users",
    "ExpirationHours": 24
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Testing Documentation

### Unit Testing Guidelines
- **Coverage Target**: Minimum 80% code coverage
- **Test Structure**: Arrange-Act-Assert pattern
- **Naming Convention**: MethodName_Scenario_ExpectedResult
- **Mocking**: Use Moq for external dependencies
- **Test Data**: Use builders and factories

### Integration Testing Guidelines
- **Database**: Use in-memory or test containers
- **API Testing**: Test complete request/response cycles
- **Authentication**: Test JWT token validation
- **Authorization**: Test role-based access control

### End-to-End Testing
- **User Flows**: Test critical user journeys
- **Cross-Browser**: Test on major browsers
- **Mobile**: Test responsive design
- **Performance**: Test load and stress scenarios

---
*Documentation will be continuously updated as the project evolves and new features are added.*
