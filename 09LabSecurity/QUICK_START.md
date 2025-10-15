# Quick Start Guide - Lab 9 Secure API

## 🚀 Run the Application (3 Steps)

### 1. Navigate to Project
```bash
cd 09LabSecurity/ADSDentalSurgeriesSecureAPI
```

### 2. Run the Application
```bash
dotnet run --urls "http://localhost:5000"
```

### 3. Open Swagger UI
```
http://localhost:5000
```

---

## 🔐 Quick Test with curl

### Step 1: Login (Get Token)
```bash
curl -X POST http://localhost:5000/adsweb/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"usernameOrEmail": "admin", "password": "admin123"}' | jq .
```

**Copy the `token` from the response!**

### Step 2: Use Token to Access Protected Endpoint
```bash
curl -X GET http://localhost:5000/adsweb/api/v1/patients \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" | jq .
```

### Step 3: Try Without Token (Should Fail)
```bash
curl -w "\nHTTP Status: %{http_code}\n" \
  -X GET http://localhost:5000/adsweb/api/v1/patients
```

**Expected**: HTTP Status 401 (Unauthorized)

---

## 👥 Default Users

| Username | Password | Role |
|----------|----------|------|
| admin | admin123 | ADMIN |
| tony.smith | password123 | DENTIST |
| helen.pearson | password123 | DENTIST |
| receptionist | password123 | RECEPTIONIST |

---

## 🧪 Using Swagger UI

### 1. Open Swagger
```
http://localhost:5000
```

### 2. Login
- Expand `POST /adsweb/api/v1/auth/login`
- Click "Try it out"
- Use: `{"usernameOrEmail": "admin", "password": "admin123"}`
- Click "Execute"
- **Copy the token** from the response

### 3. Authorize
- Click the **"Authorize"** button (🔒) at the top right
- Enter: `Bearer YOUR_TOKEN_HERE`
- Click "Authorize"
- Click "Close"

### 4. Test Protected Endpoints
- Now try any endpoint (e.g., `GET /adsweb/api/v1/patients`)
- It will work because you're authenticated!

---

## 📦 Import Postman Collection

### Option 1: Import File
1. Open Postman
2. Click "Import"
3. Select: `ADS_Secure_API.postman_collection.json`
4. Run "1. Login - Admin" first
5. Token will be automatically saved
6. Test other endpoints

### Option 2: Manual Setup
1. Create new request
2. Set URL: `http://localhost:5000/adsweb/api/v1/auth/login`
3. Method: POST
4. Body (raw JSON):
   ```json
   {
     "usernameOrEmail": "admin",
     "password": "admin123"
   }
   ```
5. Send request
6. Copy token from response
7. For protected endpoints:
   - Add Header: `Authorization`
   - Value: `Bearer YOUR_TOKEN_HERE`

---

## ⚡ Quick Commands

### Build
```bash
dotnet build
```

### Run
```bash
dotnet run --urls "http://localhost:5000"
```

### Clean & Rebuild
```bash
dotnet clean && dotnet build
```

### Run in Background
```bash
dotnet run --urls "http://localhost:5000" &
```

### Stop Background Process
```bash
pkill -f "ADSDentalSurgeriesSecureAPI"
```

---

## 🔍 API Endpoints Quick Reference

### Authentication (No Auth Required)
```
POST   /adsweb/api/v1/auth/login
POST   /adsweb/api/v1/auth/register
GET    /adsweb/api/v1/auth/health
```

### Protected Endpoints (Auth Required)
```
GET    /adsweb/api/v1/auth/me
GET    /adsweb/api/v1/patients
GET    /adsweb/api/v1/patients/{id}
POST   /adsweb/api/v1/patients
PUT    /adsweb/api/v1/patients/patient/{id}
DELETE /adsweb/api/v1/patients/patient/{id}
GET    /adsweb/api/v1/addresses
GET    /adsweb/api/v1/addresses/{id}
```

---

## 🐛 Troubleshooting

### Issue: Port 5000 Already in Use
**Solution:**
```bash
# Kill existing process
pkill -f "ADSDentalSurgeriesSecureAPI"
# Or use different port
dotnet run --urls "http://localhost:5001"
```

### Issue: 401 Unauthorized
**Solution:**
1. Make sure you logged in
2. Check token is correct
3. Token format: `Bearer YOUR_TOKEN`
4. Token might be expired (24 hours)

### Issue: Database Not Found
**Solution:**
The database is automatically created on first run. If issues persist:
```bash
rm ads_dental_surgeries_secure.db
dotnet run
```

---

## 📚 More Information

- **Full Documentation**: See `README.md`
- **Implementation Details**: See `LAB9_IMPLEMENTATION_SUMMARY.md`
- **Postman Collection**: See `ADS_Secure_API.postman_collection.json`

---

## ✅ Verification Checklist

- [ ] Application runs on port 5000
- [ ] Swagger UI accessible
- [ ] Can login with admin credentials
- [ ] Receive JWT token
- [ ] Can access protected endpoints with token
- [ ] Cannot access protected endpoints without token (401)
- [ ] All default users can login

---

**Ready to test!** 🎉

