# Customer-Accounts Management System (CAMS)

## Overview
A full-stack web application for The Special Bank of Burlington to manage customer accounts with focus on liquidity position and prime accounts.

## Technology Stack
- **Frontend**: Blazor Server (.NET 8)
- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server LocalDB with Entity Framework Core
- **Testing**: xUnit with In-Memory Database
- **UI Framework**: Bootstrap 5

## Features Implemented

### 1. Homepage
- Welcome page with menu options
- Navigation to all features
- API endpoint information

### 2. Customer-Accounts List
- Display all customer accounts sorted by balance (descending)
- Show liquidity position (sum of all balances)
- Prime account indicators
- Responsive table design

### 3. Create Customer-Account Form
- Web form for creating new customer-account pairs
- Form validation
- Success/error messaging
- Real-time feedback

### 4. REST API Endpoints
- `GET /api/account/list` - All customer accounts in JSON
- `GET /api/account/prime/list` - Prime accounts (>$10,000) in JSON
- `GET /api/account/{id}` - Get specific account
- `POST /api/account` - Create new account
- `DELETE /api/account/{id}` - Delete account
- `GET /api/account/liquidity` - Get liquidity position

### 5. Prime Accounts View
- Dedicated page for prime accounts
- Summary statistics
- API endpoint information

## Domain Model

### Customer Entity
- `customerId` (long, Primary Key)
- `firstName` (string, required)
- `lastName` (string, required)

### Account Entity
- `accountId` (long, Primary Key)
- `accountNumber` (string, required, unique)
- `accountType` (string, required)
- `dateOpened` (DateTime, optional)
- `balance` (decimal, required)
- `customerId` (long, Foreign Key)

### Business Rules
- Customer 1:1 Account relationship
- Prime Account: Balance > $10,000
- Liquidity Position: Sum of all account balances

## Sample Data
The application comes pre-loaded with sample data:
- 3 customers (Bob Jones, Anna Smith, Carlos Jimenez)
- 3 accounts with balances: $125.95, $10,900.50, $15,000
- Total liquidity: $26,026.45
- Prime accounts: 2 (AC1002, AC1003)

## Running the Application

### Prerequisites
- .NET 8 SDK
- SQL Server LocalDB (or SQL Server)

### Steps
1. Navigate to the src directory
2. Restore packages: `dotnet restore`
3. Build solution: `dotnet build`
4. Run application: `dotnet run --project CAMS.Blazor`
5. Open browser to `https://localhost:5001`

### Database
The application uses Entity Framework Core with Code First approach. The database is automatically created on first run with sample data.

## Testing
Run unit tests with:
```bash
dotnet test
```

Test coverage includes:
- Service layer functionality
- Model validation
- Business logic
- API endpoints

## Project Structure
```
src/
├── CAMS.Blazor/          # Main Blazor Server application
│   ├── Controllers/      # API controllers
│   ├── Pages/           # Blazor pages
│   ├── Services/        # Business logic services
│   └── wwwroot/         # Static files
├── CAMS.Shared/         # Shared models and data context
│   ├── Models/          # Domain entities
│   ├── DTOs/            # Data transfer objects
│   └── Data/            # Entity Framework context
└── CAMS.Tests/          # Unit tests
    ├── Models/          # Model tests
    └── Services/        # Service tests
```

## API Documentation

### Get All Accounts
```
GET /api/account/list
```
Returns all customer accounts sorted by balance (descending).

### Get Prime Accounts
```
GET /api/account/prime/list
```
Returns only accounts with balance > $10,000.

### Create Account
```
POST /api/account
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "accountNumber": "AC1004",
  "accountType": "Checking",
  "balance": 5000.00,
  "dateOpened": "2024-01-01"
}
```

### Get Liquidity Position
```
GET /api/account/liquidity
```
Returns total balance, account count, and prime account count.

## Screenshots
Screenshots of the application are available in the `screenshots/` folder, including:
- Homepage
- Customer accounts list
- Create account form
- Prime accounts view
- API responses
- Database tables

## Submission
This project is packaged as `GSATest.zip` for submission, containing:
- Complete source code
- Documentation
- Screenshots
- Database schema
- Test results
