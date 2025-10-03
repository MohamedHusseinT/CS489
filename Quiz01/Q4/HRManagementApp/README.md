# Allied Building Contractors HR Management System

**CS489 Quiz01/Q4 - CLI Application**  
**Student**: Mohamed  
**Technology**: .NET 8.0 Console Application

## 📋 Project Overview

This is a Command-Line Interface (CLI) application for Allied Building Contractors' Human Resource Management system. The application manages employee and department data with JSON output capabilities.

## 🏗️ Architecture

### Domain Model
- **Department**: Represents company departments with employees and head of department
- **Employee**: Represents company employees with department associations

### Key Features
1. **Department List JSON**: Includes employees, head of department, and total annual salary
2. **Employee List JSON**: Includes department data and years of employment
3. **Proper Sorting**: Departments by salary (desc), Employees by years of employment (desc) and last name (asc)

## 📊 Data Structure

### Departments
- Sales (31288741190182539912) - Head: Anna Smith
- Marketing (29274582650152771644) - No Head
- Engineering (29274599650152771609) - Head: Michael Philips

### Employees
- Michael Philips (000-11-1234) - Engineering - $71,513.00/year
- Anna Smith (000-61-0987) - Marketing - $65,000.00/year  
- John Hammonds (000-99-1766) - Sales - $40,579.50/year
- Barbara Jones (000-41-1768) - Marketing - $42,914.30/year

## 🚀 Running the Application

```bash
cd Quiz01/Q4/HRManagementApp
dotnet run
```

## 📸 Screenshots Required

1. **Department List Output**: JSON with employees, head, and total salary
2. **Employee List Output**: JSON with department data and years of employment
3. **Application Execution**: Complete CLI output

## 🎯 Requirements Met

- ✅ UML Domain Model diagram
- ✅ .NET CLI application (equivalent to Maven/Gradle)
- ✅ Department and Employee entities
- ✅ In-memory data store with provided sample data
- ✅ JSON processing library usage
- ✅ Proper sorting requirements
- ✅ Layered architecture (Models, Services)
- ✅ Professional software design principles

## 📁 Project Structure

```
HRManagementApp/
├── Models/
│   ├── Department.cs
│   └── Employee.cs
├── Services/
│   ├── DataService.cs
│   └── JsonService.cs
├── screenshots/
├── Program.cs
├── HR_Domain_Model_UML.puml
└── README.md
```

## 🔧 Technical Implementation

- **JSON Processing**: System.Text.Json library
- **Data Management**: In-memory collections with proper relationships
- **Sorting**: LINQ OrderBy/ThenBy for complex sorting requirements
- **Architecture**: Layered approach with separation of concerns
- **Error Handling**: Try-catch blocks for robust execution
