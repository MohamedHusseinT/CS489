# Lab 11 - Testing Implementation

This repository contains the complete Lab 11 solution with both Part 1 (Java with JUnit and Mockito) and Part 2 (.NET with xUnit and Moq).

## 📁 Project Structure

```
11Lab/
├── Part1/                          # Java Implementation
│   ├── ArrayFlattener/             # ArrayFlattener with JUnit tests
│   │   ├── src/main/java/com/lab11/
│   │   │   └── ArrayFlattener.java
│   │   ├── src/test/java/com/lab11/
│   │   │   ├── ArrayFlattenerTest.java
│   │   │   └── ArrayFlattenerTestSuite.java
│   │   └── pom.xml
│   └── ArrayReversor/             # ArrayReversor with Mockito mocking
│       ├── src/main/java/com/lab11/
│       │   ├── ArrayFlattenerService.java
│       │   └── ArrayReversor.java
│       ├── src/test/java/com/lab11/
│       │   ├── ArrayReversorTest.java
│       │   └── ArrayReversorTestCases.java
│       └── pom.xml
└── Part2/                         # .NET Implementation
    ├── ADSDentalSurgeriesWebAPI/   # Base Web API from Lab 7
    └── ADSDentalSurgeriesWebAPI.Tests/
        ├── IntegrationTests/
        │   └── PatientServiceIntegrationTests.cs
        └── UnitTests/
            └── PatientControllerUnitTests.cs
```

## 🚀 Part 1: Java Implementation

### Prerequisites
- Java 11 or higher
- Maven 3.6 or higher

### ArrayFlattener Component

**Purpose**: Flattens a 2D nested array into a 1D array.

**Example**:
- Input: `[[1,3], [0], [4,5,9]]`
- Output: `[1,3,0,4,5,9]`

#### Running ArrayFlattener Tests

```bash
cd Part1/ArrayFlattener
mvn clean test
```

#### Test Cases Covered:
1. **Valid 2D Array**: Tests with input `[[1,3], [0], [4,5,9]]`
2. **Null Input**: Tests exception handling for null input
3. **Empty Array**: Tests with empty 2D array
4. **Null Sub-arrays**: Tests handling of null sub-arrays

### ArrayReversor Component

**Purpose**: Reverses a 2D nested array by first flattening it using a remote service, then reversing the result.

**Example**:
- Input: `[[1,3], [0], [4,5,9]]`
- After flattening: `[1,3,0,4,5,9]`
- After reversing: `[9,5,4,0,3,1]`

#### Running ArrayReversor Tests

```bash
cd Part1/ArrayReversor
mvn clean test
```

#### Test Cases Covered:
1. **Valid 2D Array**: Tests with input `[[1,3], [0], [4,5,9]]`
2. **Null Input**: Tests exception handling for null input
3. **Service Dependency Verification**: Ensures the service is actually called and not bypassed
4. **Empty Array**: Tests with empty 2D array
5. **Single Element**: Tests with single element array

#### Key Features:
- **Mockito Integration**: Uses Mockito to mock the `ArrayFlattenerService`
- **Service Verification**: Verifies that the remote service is called exactly once
- **Dependency Testing**: Ensures results depend on service output, not hard-coded values

## 🚀 Part 2: .NET Implementation

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Integration Tests

**Purpose**: Tests `PatientService.GetPatientByIdAsync()` method with real database interactions.

#### Running Integration Tests

```bash
cd Part2/ADSDentalSurgeriesWebAPI.Tests
dotnet test --filter "IntegrationTests"
```

#### Test Cases Covered:
1. **Valid Patient ID**: Tests retrieval of existing patient
2. **Invalid Patient ID**: Tests handling of non-existent patient ID
3. **Edge Cases**: Tests with zero and negative IDs
4. **Related Data**: Verifies inclusion of Address and Appointments

#### Key Features:
- **In-Memory Database**: Uses Entity Framework In-Memory provider
- **Test Data Seeding**: Automatically seeds test data for each test
- **Isolation**: Each test uses a unique database instance

### Unit Tests

**Purpose**: Tests `PatientController.GetAllPatients()` endpoint using mocking.

#### Running Unit Tests

```bash
cd Part2/ADSDentalSurgeriesWebAPI.Tests
dotnet test --filter "UnitTests"
```

#### Test Cases Covered:
1. **Valid Data**: Tests successful response with patient data
2. **Empty Data**: Tests response with empty patient list
3. **Exception Handling**: Tests graceful handling of service exceptions
4. **Mapping Verification**: Ensures correct mapping from Patient to PatientResponse
5. **Service Dependency**: Verifies service is called and not bypassed

#### Key Features:
- **Moq Integration**: Uses Moq framework for mocking
- **Service Verification**: Verifies service method calls
- **Response Validation**: Tests proper HTTP response codes and data structure

## 🧪 Running All Tests

### Java Tests (Part 1)
```bash
# Run ArrayFlattener tests
cd Part1/ArrayFlattener && mvn clean test

# Run ArrayReversor tests
cd ../ArrayReversor && mvn clean test
```

### .NET Tests (Part 2)
```bash
# Run all .NET tests
cd Part2/ADSDentalSurgeriesWebAPI.Tests
dotnet test

# Run only integration tests
dotnet test --filter "IntegrationTests"

# Run only unit tests
dotnet test --filter "UnitTests"
```

## 📊 Test Coverage Summary

### Part 1 (Java)
- **ArrayFlattener**: 4 test cases + TestSuite
- **ArrayReversor**: 5 test cases + TestSuite
- **Total**: 9 test cases with comprehensive coverage

### Part 2 (.NET)
- **Integration Tests**: 4 test cases for PatientService
- **Unit Tests**: 5 test cases for PatientController
- **Total**: 9 test cases with comprehensive coverage

## 🔧 Key Testing Concepts Demonstrated

### Part 1 (Java)
1. **JUnit 5**: Modern testing framework with annotations
2. **Mockito**: Mocking framework for dependency injection testing
3. **Test Suites**: Organized test execution
4. **Service Verification**: Ensuring mocked services are called
5. **Exception Testing**: Proper exception handling validation

### Part 2 (.NET)
1. **xUnit**: .NET testing framework
2. **Moq**: .NET mocking framework
3. **Integration Testing**: Real database interaction testing
4. **Unit Testing**: Isolated component testing with mocks
5. **In-Memory Database**: Test database isolation
6. **Dependency Injection**: Service mocking and verification

## 📝 Test Execution Results

When you run the tests, you should see output similar to:

### Java (Maven)
```
[INFO] Tests run: 9, Failures: 0, Errors: 0, Skipped: 0
[INFO] BUILD SUCCESS
```

### .NET (dotnet test)
```
Passed!  - Failed:     0, Passed:     9, Skipped:     0, Total:     9
```

## 🎯 Learning Objectives Achieved

1. ✅ **Unit Testing**: Implemented comprehensive unit tests for both Java and .NET
2. ✅ **Integration Testing**: Created integration tests with real database interactions
3. ✅ **Mocking**: Used Mockito (Java) and Moq (.NET) for dependency mocking
4. ✅ **Test Suites**: Organized tests into logical suites
5. ✅ **Service Verification**: Ensured mocked services are properly called
6. ✅ **Exception Handling**: Tested proper error handling and edge cases
7. ✅ **Test Isolation**: Each test runs independently with clean state

## 🚀 Next Steps

1. Run all tests to verify they pass
2. Examine test output and coverage
3. Modify tests to explore different scenarios
4. Add additional test cases as needed
5. Submit the solution to GitHub and provide the URL

---

**Note**: This implementation demonstrates professional testing practices used in real-world software development, including proper test organization, mocking strategies, and comprehensive coverage of both happy path and edge cases.

