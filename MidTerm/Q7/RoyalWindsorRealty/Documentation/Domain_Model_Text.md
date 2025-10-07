# Royal Windsor Realty - Domain Model UML Class Diagram
## MidTerm/Q7 - CS489 Applied Software Development
**Student:** Mohamed

---

## Text-Based Domain Model

```
                    ┌─────────────────┐
                    │    Address      │
                    │─────────────────│
                    │ apartmentNumber │
                    │ street          │
                    │ city            │
                    │ state           │
                    │ zipCode         │
                    │─────────────────│
                    │ GetFullAddress()│
                    └─────────────────┘
                            │
                            │ 1:1
                            │
                    ┌─────────────────┐
                    │   Apartment     │
                    │─────────────────│
                    │ apartmentId (PK)│
                    │ apartmentNumber │
                    │ propertyName    │
                    │ floorNo         │
                    │ size            │
                    │ numberOfRooms   │
                    │─────────────────│
                    │ GetTotalRevenue()│
                    │ GetActiveLeases()│
                    └─────────────────┘
                            │
                            │ 1:M
                            │
                    ┌─────────────────┐
                    │     Lease       │
                    │─────────────────│
                    │ leaseId (PK)    │
                    │ leaseNumber(UK) │
                    │ startDate       │
                    │ endDate         │
                    │ monthlyRentalRate│
                    │ apartmentId (FK)│
                    │ tenantId (FK)   │
                    │─────────────────│
                    │ GetLeaseDuration()│
                    │ GetTotalRevenue()│
                    │ IsActive()      │
                    └─────────────────┘
                            │
                            │ M:1
                            │
                    ┌─────────────────┐
                    │     Tenant      │
                    │─────────────────│
                    │ tenantId (PK)   │
                    │ firstName       │
                    │ lastName        │
                    │ phoneNumber     │
                    │ email           │
                    │─────────────────│
                    │ GetFullName()   │
                    │ GetTotalLeases()│
                    │ GetTotalRentPaid()│
                    └─────────────────┘
```

## Entity Relationships

### **Address ↔ Apartment (1:1)**
- **Relationship:** One address can only be associated with one apartment
- **Cardinality:** 1 to 1
- **Business Rule:** Each apartment has a unique physical address

### **Apartment ↔ Lease (1:M)**
- **Relationship:** One apartment can have many leases over time
- **Cardinality:** 1 to Many
- **Business Rule:** Apartment can be leased multiple times to different tenants

### **Tenant ↔ Lease (1:M)**
- **Relationship:** One tenant can hold multiple leases
- **Cardinality:** 1 to Many
- **Business Rule:** Tenant can lease multiple apartments or renew leases

## Key Attributes

### **Address Entity**
- `apartmentNumber`: Unique identifier (K1210, B1109, G815)
- `street`: Street address (123 West Avenue, 900 Johns Street)
- `city`: City name (Phoenix, Cleveland)
- `state`: State abbreviation (AZ, OH)
- `zipCode`: Postal code (85012, 43098)

### **Apartment Entity**
- `apartmentId`: Primary key (1, 2, 3)
- `apartmentNumber`: Links to address (K1210, B1109, G815)
- `propertyName`: Building name (Bells Court, The Galleria)
- `floorNo`: Floor number (12, 11, 8)
- `size`: Square footage (1150, 970, 1150)
- `numberOfRooms`: Room count (2, 1, 2)

### **Tenant Entity**
- `tenantId`: Primary key (1, 2)
- `firstName`: First name (Robert, Anna)
- `lastName`: Last name (Lanskov, Smith)
- `phoneNumber`: Contact phone ((480) 123-1355, (414) 998-0112)
- `email`: Email address (optional, asmith@mail.com)

### **Lease Entity**
- `leaseId`: Primary key (1, 2, 3, 4)
- `leaseNumber`: Unique identifier (D0187-175, W1736-142, DD001-142, P162-0017)
- `startDate`: Lease start (2021-10-1, 2022-8-15, 2022-10-1, 2023-10-1)
- `endDate`: Lease end (2022-9-30, 2024-2-14, 2023-9-30, 2024-9-30)
- `monthlyRentalRate`: Monthly rent ($1,750.00, $1,500.00, $1,975.00, $2,275.00)
- `apartmentId`: Foreign key to apartment
- `tenantId`: Foreign key to tenant

## Business Rules

### **Mandatory Relationships**
- Every lease MUST be associated with an apartment
- Every lease MUST be associated with a tenant
- Every tenant MUST have at least one lease
- Every apartment MUST have an address

### **Optional Relationships**
- Apartment may not have any leases (newly constructed)
- Tenant email is optional
- Apartment floor number is optional

### **Revenue Calculation**
- **Revenue per lease** = monthly rental rate × lease duration (in months)
- **Total apartment revenue** = sum of all lease revenues for that apartment
- **Total tenant payments** = sum of all lease payments by that tenant

## Sample Data Mapping

| Entity | Count | Sample Records |
|--------|-------|----------------|
| Address | 3 | K1210 (Phoenix), B1109 (Cleveland), G815 (Phoenix) |
| Apartment | 3 | Bells Court (2 units), The Galleria (1 unit) |
| Tenant | 2 | Robert Lanskov (3 leases), Anna Smith (1 lease) |
| Lease | 4 | Various dates and rates, all linked to apartments/tenants |

---

**Note:** This domain model follows object-oriented principles and implements all business requirements specified in the problem statement.
