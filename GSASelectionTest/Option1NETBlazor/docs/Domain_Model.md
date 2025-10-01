# Domain Model - Customer-Accounts Management System (CAMS)

## Overview
The Special Bank of Burlington requires a Customer-Accounts Management System (CAMS) to manage customer and account data with focus on liquidity position and prime accounts.

## Business Rules
1. A Customer can own one and only one Account
2. Each Account is owned by only one Customer
3. Prime Account: Account with balance greater than $10,000
4. Liquidity Position: Sum of all Account balances

## Domain Entities

### Customer
- **customerId**: long (Primary Key)
- **firstName**: string (required)
- **lastName**: string (required)

### Account
- **accountId**: long (Primary Key)
- **accountNumber**: string (required, unique)
- **accountType**: string (required) - e.g., "Checking", "Savings"
- **dateOpened**: DateTime (optional)
- **balance**: decimal (required) - Amount in dollars and cents
- **customerId**: long (Foreign Key to Customer)

## Relationship
- **Customer** 1:1 **Account** (One-to-One relationship)

## Sample Data

### Customer Data
| Customer Id | First Name | Last Name |
|-------------|------------|-----------|
| 1           | Bob        | Jones     |
| 2           | Anna       | Smith     |
| 3           | Carlos     | Jimenez   |

### Account Data
| Account Id | Account Number | Account Type | Date Opened | Balance  | Customer Id |
|------------|----------------|--------------|-------------|----------|-------------|
| 1          | AC1002         | Checking     | 2022-07-10  | 10900.50 | 2           |
| 2          | AC1001         | Savings      | 2021-11-15  | 125.95   | 1           |
| 3          | AC1003         | Savings      | 2022-07-11  | 15000    | 3           |

## Calculated Values
- **Liquidity Position**: $26,026.45 (10900.50 + 125.95 + 15000)
- **Prime Accounts**: AC1002 ($10,900.50) and AC1003 ($15,000)
