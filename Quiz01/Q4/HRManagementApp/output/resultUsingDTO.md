(base) m@Ms-MacBook-Pro HRManagementApp % dotnet run
🏢 Allied Building Contractors HR Management System
============================================================
CS489 Quiz01/Q4 - CLI Application
Student: Mohamed
============================================================

📊 Loading Departments Data...

============================================================
 DEPARTMENTS LIST (Sorted by Total Annual Salary - Descending)
============================================================
[
  {
    "departmentNo": "29274582650152771644",
    "name": "Marketing",
    "employees": [
      {
        "employeeNo": "000-61-0987",
        "firstName": "Anna",
        "lastName": "Smith",
        "dateOfBirth": "1981-09-17T00:00:00",
        "dateOfEmployment": "2022-08-21T00:00:00",
        "biweeklySalary": 2500.00,
        "yearsOfEmployment": 3,
        "annualSalary": 65000.00
      },
      {
        "employeeNo": "000-41-1768",
        "firstName": "Barbara",
        "lastName": "Jones",
        "dateOfBirth": "2000-11-18T00:00:00",
        "dateOfEmployment": "2025-03-13T00:00:00",
        "biweeklySalary": 1650.55,
        "yearsOfEmployment": 0,
        "annualSalary": 42914.30
      }
    ],
    "headOfDepartment": null,
    "totalAnnualSalary": 107914.30
  },
  {
    "departmentNo": "29274599650152771609",
    "name": "Engineering",
    "employees": [
      {
        "employeeNo": "000-11-1234",
        "firstName": "Michael",
        "lastName": "Philips",
        "dateOfBirth": "1995-12-31T00:00:00",
        "dateOfEmployment": "2021-10-15T00:00:00",
        "biweeklySalary": 2750.50,
        "yearsOfEmployment": 3,
        "annualSalary": 71513.00
      }
    ],
    "headOfDepartment": {
      "employeeNo": "000-11-1234",
      "firstName": "Michael",
      "lastName": "Philips",
      "dateOfBirth": "1995-12-31T00:00:00",
      "dateOfEmployment": "2021-10-15T00:00:00",
      "biweeklySalary": 2750.50,
      "yearsOfEmployment": 3,
      "annualSalary": 71513.00
    },
    "totalAnnualSalary": 71513.00
  },
  {
    "departmentNo": "31288741190182539912",
    "name": "Sales",
    "employees": [
      {
        "employeeNo": "000-99-1766",
        "firstName": "John",
        "lastName": "Hammonds",
        "dateOfBirth": "2001-07-15T00:00:00",
        "dateOfEmployment": "2025-01-23T00:00:00",
        "biweeklySalary": 1560.75,
        "yearsOfEmployment": 0,
        "annualSalary": 40579.50
      }
    ],
    "headOfDepartment": {
      "employeeNo": "000-61-0987",
      "firstName": "Anna",
      "lastName": "Smith",
      "dateOfBirth": "1981-09-17T00:00:00",
      "dateOfEmployment": "2022-08-21T00:00:00",
      "biweeklySalary": 2500.00,
      "yearsOfEmployment": 3,
      "annualSalary": 65000.00
    },
    "totalAnnualSalary": 40579.50
  }
]
============================================================


👥 Loading Employees Data...

============================================================
 EMPLOYEES LIST (Sorted by Years of Employment - Descending, Last Name - Ascending)
============================================================
[
  {
    "employeeNo": "000-11-1234",
    "firstName": "Michael",
    "lastName": "Philips",
    "dateOfBirth": "1995-12-31T00:00:00",
    "dateOfEmployment": "2021-10-15T00:00:00",
    "biweeklySalary": 2750.50,
    "yearsOfEmployment": 3,
    "annualSalary": 71513.00,
    "department": {
      "departmentNo": "29274599650152771609",
      "name": "Engineering"
    }
  },
  {
    "employeeNo": "000-61-0987",
    "firstName": "Anna",
    "lastName": "Smith",
    "dateOfBirth": "1981-09-17T00:00:00",
    "dateOfEmployment": "2022-08-21T00:00:00",
    "biweeklySalary": 2500.00,
    "yearsOfEmployment": 3,
    "annualSalary": 65000.00,
    "department": {
      "departmentNo": "29274582650152771644",
      "name": "Marketing"
    }
  },
  {
    "employeeNo": "000-99-1766",
    "firstName": "John",
    "lastName": "Hammonds",
    "dateOfBirth": "2001-07-15T00:00:00",
    "dateOfEmployment": "2025-01-23T00:00:00",
    "biweeklySalary": 1560.75,
    "yearsOfEmployment": 0,
    "annualSalary": 40579.50,
    "department": {
      "departmentNo": "31288741190182539912",
      "name": "Sales"
    }
  },
  {
    "employeeNo": "000-41-1768",
    "firstName": "Barbara",
    "lastName": "Jones",
    "dateOfBirth": "2000-11-18T00:00:00",
    "dateOfEmployment": "2025-03-13T00:00:00",
    "biweeklySalary": 1650.55,
    "yearsOfEmployment": 0,
    "annualSalary": 42914.30,
    "department": {
      "departmentNo": "29274582650152771644",
      "name": "Marketing"
    }
  }
]
============================================================


📈 SUMMARY STATISTICS
========================================
Total Departments: 3
Total Employees: 4
Total Annual Salary Budget: $220,006.80
Average Employee Salary: $55,001.70
Departments with Heads: 2
Departments without Heads: 1

✅ HR Management System execution completed successfully!