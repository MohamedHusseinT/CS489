# Database Design - ER Diagram

## Entity Relationship Diagram

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                              DATABASE SCHEMA                                    │
└─────────────────────────────────────────────────────────────────────────────────┘

┌─────────────────┐
│      Users      │
├─────────────────┤
│ PK: Id (int)    │
│     Email (varchar) UNIQUE
│     PasswordHash (varchar)
│     IsActive (boolean)
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
│     LastLoginDate (datetime)
└─────────────────┘
         │
         │ 1:1
         │
         ▼
┌─────────────────┐
│    Profiles     │
├─────────────────┤
│ PK: Id (int)    │
│ FK: UserId (int) UNIQUE
│     FirstName (varchar)
│     LastName (varchar)
│     Phone (varchar)
│     Address (varchar)
│     DateOfBirth (date)
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
└─────────────────┘

┌─────────────────┐
│      Roles      │
├─────────────────┤
│ PK: Id (int)    │
│     Name (varchar) UNIQUE
│     Description (varchar)
│     IsActive (boolean)
│     CreatedDate (datetime)
│     UpdatedDate (datetime)
└─────────────────┘
         │
         │ M:N
         │
         ▼
┌─────────────────┐
│   UserRoles     │
├─────────────────┤
│ PK: Id (int)    │
│ FK: UserId (int)
│ FK: RoleId (int)
│     AssignedDate (datetime)
│     AssignedBy (int)
│     IsActive (boolean)
└─────────────────┘
         │
         │ M:N
         │
         ▼
┌─────────────────┐
│   Permissions   │
├─────────────────┤
│ PK: Id (int)    │
│     Name (varchar) UNIQUE
│     Description (varchar)
│     Resource (varchar)
│     Action (varchar)
│     IsActive (boolean)
│     CreatedDate (datetime)
└─────────────────┘
         │
         │ M:N
         │
         ▼
┌─────────────────┐
│ RolePermissions │
├─────────────────┤
│ PK: Id (int)    │
│ FK: RoleId (int)
│ FK: PermissionId (int)
│     CreatedDate (datetime)
└─────────────────┘

┌─────────────────┐
│   AuditLogs     │
├─────────────────┤
│ PK: Id (int)    │
│ FK: UserId (int)
│     Action (varchar)
│     Resource (varchar)
│     ResourceId (int)
│     OldValues (json)
│     NewValues (json)
│     IpAddress (varchar)
│     UserAgent (varchar)
│     CreatedDate (datetime)
└─────────────────┘
```

## Database Tables Description

### Core Tables

#### Users
- **Purpose**: Stores user authentication information
- **Primary Key**: Id (auto-increment)
- **Unique Constraints**: Email
- **Indexes**: Email (unique), IsActive
- **Relationships**: One-to-One with Profiles, One-to-Many with UserRoles

#### Profiles
- **Purpose**: Stores user personal information
- **Primary Key**: Id (auto-increment)
- **Foreign Key**: UserId (references Users.Id)
- **Unique Constraints**: UserId
- **Indexes**: UserId (unique), FirstName, LastName

#### Roles
- **Purpose**: Defines system roles and access levels
- **Primary Key**: Id (auto-increment)
- **Unique Constraints**: Name
- **Indexes**: Name (unique), IsActive
- **Relationships**: Many-to-Many with Users, Many-to-Many with Permissions

#### Permissions
- **Purpose**: Defines specific system capabilities
- **Primary Key**: Id (auto-increment)
- **Unique Constraints**: Name
- **Indexes**: Name (unique), Resource, Action, IsActive
- **Relationships**: Many-to-Many with Roles

### Junction Tables

#### UserRoles
- **Purpose**: Links users to their assigned roles
- **Primary Key**: Id (auto-increment)
- **Foreign Keys**: UserId, RoleId
- **Unique Constraints**: (UserId, RoleId)
- **Indexes**: UserId, RoleId, IsActive

#### RolePermissions
- **Purpose**: Links roles to their permissions
- **Primary Key**: Id (auto-increment)
- **Foreign Keys**: RoleId, PermissionId
- **Unique Constraints**: (RoleId, PermissionId)
- **Indexes**: RoleId, PermissionId

### Audit Tables

#### AuditLogs
- **Purpose**: Tracks all system changes for compliance
- **Primary Key**: Id (auto-increment)
- **Foreign Key**: UserId (nullable for system actions)
- **Indexes**: UserId, Action, Resource, CreatedDate
- **Partitioning**: By CreatedDate (monthly)

## Business Tables
[Add your specific business tables here]

### Example Business Table
```sql
CREATE TABLE BusinessEntities (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active',
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy INT,
    UpdatedDate DATETIME2,
    IsActive BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT FK_BusinessEntities_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CONSTRAINT FK_BusinessEntities_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES Users(Id),
    CONSTRAINT CK_BusinessEntities_Status CHECK (Status IN ('Active', 'Inactive', 'Pending', 'Archived'))
);

CREATE INDEX IX_BusinessEntities_Name ON BusinessEntities(Name);
CREATE INDEX IX_BusinessEntities_Status ON BusinessEntities(Status);
CREATE INDEX IX_BusinessEntities_CreatedDate ON BusinessEntities(CreatedDate);
```

## Database Constraints

### Primary Keys
- All tables have auto-incrementing integer primary keys
- Primary keys are clustered indexes for performance

### Foreign Keys
- All foreign key relationships are enforced
- Cascade delete is avoided to prevent data loss
- Soft delete pattern used instead

### Check Constraints
- Status fields have predefined value lists
- Date fields validate reasonable ranges
- Email fields validate format

### Unique Constraints
- Email addresses must be unique
- Role names must be unique
- Permission names must be unique
- Composite unique constraints for junction tables

## Indexing Strategy

### Primary Indexes
- **Clustered**: Primary keys (Id columns)
- **Non-clustered**: Foreign keys, unique constraints

### Performance Indexes
- **Email**: For user lookup and authentication
- **Status/IsActive**: For filtering active records
- **CreatedDate**: For date range queries
- **Composite**: (UserId, IsActive) for user role queries

### Full-Text Indexes
- **Search Fields**: Name, Description fields for business entities
- **Content Fields**: Large text fields for content search

## Data Types and Sizing

### String Fields
- **Email**: VARCHAR(255) - Standard email length
- **Names**: VARCHAR(100) - Reasonable name length
- **Descriptions**: VARCHAR(500) - Short descriptions
- **Long Text**: NVARCHAR(MAX) - Unlimited text

### Numeric Fields
- **IDs**: INT - Standard for primary keys
- **Decimals**: DECIMAL(18,2) - Currency and precise calculations
- **Booleans**: BIT - SQL Server boolean type

### Date Fields
- **Timestamps**: DATETIME2 - High precision timestamps
- **Dates**: DATE - Date-only fields
- **Default**: GETUTCDATE() - UTC timestamps

## Security Considerations

### Data Encryption
- **At Rest**: Database-level encryption
- **In Transit**: TLS/SSL connections
- **Application Level**: Password hashing (bcrypt)

### Access Control
- **Database Users**: Limited permissions per application
- **Row-Level Security**: User-specific data access
- **Audit Trail**: All changes tracked

### Backup Strategy
- **Full Backups**: Daily automated backups
- **Transaction Log**: Continuous backup for point-in-time recovery
- **Retention**: 30 days for full backups, 7 days for logs

## Migration Strategy

### Version Control
- **Entity Framework Migrations**: Database schema versioning
- **Naming Convention**: YYYYMMDD_HHMMSS_Description
- **Rollback Support**: Down migrations for all changes

### Deployment Process
1. **Development**: Local database with migrations
2. **Staging**: Automated migration deployment
3. **Production**: Manual review and deployment
4. **Rollback Plan**: Tested rollback procedures

---
*This database design will be implemented using Entity Framework Core migrations and will evolve as requirements are refined.*
