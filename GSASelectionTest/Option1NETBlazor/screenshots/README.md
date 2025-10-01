# Screenshots for GSA Selection Test

This folder contains screenshots demonstrating the functionality of the Customer-Accounts Management System (CAMS).

## Required Screenshots

### 1. Homepage
- **File**: `01_homepage.png`
- **Description**: Shows the main homepage with menu options and welcome message
- **Features**: Navigation menu, API endpoint information, responsive design

### 2. Customer-Accounts List
- **File**: `02_accounts_list.png`
- **Description**: Displays all customer accounts sorted by balance (descending)
- **Features**: 
  - Accounts sorted by balance (highest first)
  - Liquidity position display ($26,026.45)
  - Prime account indicators
  - Responsive table design

### 3. Create Account Form
- **File**: `03_create_account_form.png`
- **Description**: Web form for creating new customer-account pairs
- **Features**:
  - Form validation
  - Customer and account information fields
  - Success/error messaging
  - Real-time feedback

### 4. Prime Accounts View
- **File**: `04_prime_accounts.png`
- **Description**: Dedicated page showing only prime accounts (>$10,000)
- **Features**:
  - Filtered view of prime accounts
  - Summary statistics
  - API endpoint information

### 5. API Endpoints
- **File**: `05_api_endpoints.png`
- **Description**: REST API responses in JSON format
- **Features**:
  - `/api/account/list` - All accounts
  - `/api/account/prime/list` - Prime accounts only
  - JSON response format
  - Proper HTTP status codes

### 6. Database Tables
- **File**: `06_database_tables.png`
- **Description**: Database tables showing the data structure
- **Features**:
  - Customer table with sample data
  - Account table with sample data
  - Relationship between tables
  - Data integrity

## Additional Screenshots

### 7. Application Navigation
- **File**: `07_navigation.png`
- **Description**: Shows the navigation menu and routing
- **Features**: Sidebar navigation, active page indicators

### 8. Form Validation
- **File**: `08_form_validation.png`
- **Description**: Demonstrates form validation and error handling
- **Features**: Required field validation, error messages

### 9. Responsive Design
- **File**: `09_responsive_design.png`
- **Description**: Shows the application on different screen sizes
- **Features**: Mobile-friendly design, responsive tables

### 10. Test Results
- **File**: `10_test_results.png`
- **Description**: Unit test execution results
- **Features**: Test coverage, passing tests, test statistics

## How to Take Screenshots

1. Run the application: `dotnet run --project CAMS.Blazor`
2. Navigate to each page/feature
3. Use browser developer tools for API testing
4. Use database management tools for table screenshots
5. Save screenshots as PNG files with descriptive names

## Notes

- All screenshots should be high quality and clearly show the functionality
- Include browser address bar to show the URL
- Ensure all data is visible and readable
- Use consistent naming convention
- Include timestamps if required
