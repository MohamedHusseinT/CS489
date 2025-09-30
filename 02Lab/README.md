# Employee Pension Management System - Lab 2A

This project implements a Command-Line Interface (CLI) application for managing Employee Pensions planning system using .NET Core.

## Project Structure

```
02Lab/
├── 02LabA/
│   ├── TaskA/
│   │   ├── EmployeePensionApp/
│   │   │   ├── Employee.cs
│   │   │   ├── PensionPlan.cs
│   │   │   ├── EmployeePensionApp.cs
│   │   │   ├── Program.cs
│   │   │   └── EmployeePensionApp.csproj
│   │   └── screenshots/
│   └── TaskB/
│       ├── EmployeePensionApp/
│       │   ├── Employee.cs
│       │   ├── PensionPlan.cs
│       │   ├── EmployeePensionApp.cs
│       │   ├── Program.cs
│       │   └── EmployeePensionApp.csproj
│       └── screenshots/
└── 02LabB/
```

## Features

### 1. Print All Employees (JSON Format)
- Displays all employees with their pension plan information (if applicable)
- Sorted by:
  - Yearly Salary (Descending)
  - Last Name (Ascending)

### 2. Quarterly Upcoming Enrollees Report
- Shows employees eligible for pension enrollment in the next quarter
- Criteria:
  - Not currently enrolled in a pension plan
  - Employed for at least 3 years by the end of next quarter
- Sorted by Employment Date (Descending)

## Sample Data

The application includes the following employee data:

| Employee | Employment Date | Yearly Salary | Pension Plan |
|----------|----------------|---------------|--------------|
| Daniel Agar | 2023-01-17 | $105,945.50 | EX1089 (Enrolled) |
| Bernard Shaw | 2022-09-03 | $197,750.00 | Not Enrolled |
| Carly Agar | 2014-05-16 | $842,000.75 | SM2307 (Enrolled) |
| Wesley Schneider | 2023-07-21 | $74,500.00 | Not Enrolled |
| Anna Wiltord | 2023-03-15 | $85,750.00 | Not Enrolled |
| Yosef Tesfalem | 2024-10-31 | $100,000.00 | Not Enrolled |

## How to Run

### Prerequisites
- .NET 8.0 SDK or later

### Running the Application

1. Navigate to the project directory:
   ```bash
   cd 02Lab/02LabA/TaskA/EmployeePensionApp
   # or
   cd 02Lab/02LabA/TaskB/EmployeePensionApp
   ```

2. Build the application:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Follow the menu prompts:
   - Enter `1` to print all employees in JSON format
   - Enter `2` to print Quarterly Upcoming Enrollees report
   - Enter `3` to exit

## CI/CD Pipeline

The project includes a GitHub Actions workflow (`.github/workflows/dotnet-ci.yml`) that:
- Builds the application on push/PR events
- Runs tests (if any)
- Performs security scanning
- Deploys to staging on main branch

## Entity Classes

### Employee
- `employeeId`: long
- `firstName`: string
- `lastName`: string
- `employmentDate`: DateTime
- `yearlySalary`: decimal
- `pensionPlan`: PensionPlan (nullable)

### PensionPlan
- `planReferenceNumber`: string
- `enrollmentDate`: DateTime
- `monthlyContribution`: decimal

## Business Rules

1. **Pension Eligibility**: Employees must be employed for at least 3 years to be eligible for pension enrollment
2. **One Plan Per Employee**: Each employee can only be enrolled in one pension plan
3. **Plan Requirement**: Every pension plan must have an employee enrolled to it
4. **Quarterly Report**: Shows employees who will qualify for enrollment in the next quarter

## Screenshots

Screenshots of the application running are stored in the `screenshots/` folder within each task directory.
