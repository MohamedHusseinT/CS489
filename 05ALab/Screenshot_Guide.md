# 📸 Screenshot Guide for Lab 05A Submission

## Advantis Dental Surgeries (ADS) Database System
**Lab 05A - CS489 Applied Software Development**  
**Student:** Mohamed

---

## 🎯 **Required Screenshots**

You need to take **5 screenshots** for your lab submission:

1. **ERD Image** (Entity Relationship Diagram)
2. **Query 1**: All Dentists (sorted by last name)
3. **Query 2**: Appointments for Dentist ID = 1
4. **Query 3**: Appointments at Surgery Location ID = 1
5. **Query 4**: Patient Appointments on Specific Date

---

## 🎨 **Step 1: Generate ERD Image**

### Option A: Using PlantUML (Recommended)
```bash
# Navigate to lab folder
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/05ALab

# Generate ERD image
./generate-erd.sh
```

### Option B: Online PlantUML Editor
1. Go to: http://www.plantuml.com/plantuml/uml/
2. Copy contents of `ADS_ERD.puml`
3. Paste and click "Submit"
4. Take screenshot of the generated diagram

### Option C: Manual Drawing
- Use any drawing tool (Draw.io, Lucidchart, etc.)
- Create ERD based on the entities and relationships in `ER_Model_Documentation.md`

**Save as:** `ERD_ADS_Dental_Surgery.png`

---

## 🔍 **Step 2: Take Query Screenshots**

Open **Terminal** and run these commands one by one:

### **Screenshot 1: Query 1 - All Dentists**
```bash
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/05ALab

echo "=== QUERY 1: All Dentists (sorted by last name) ==="
sqlite3 ADSDentalSurgery.db "SELECT dentist_id, first_name, last_name, contact_phone, email, specialization FROM Dentists ORDER BY last_name ASC;"
```
**Take screenshot** → Save as: `Query1_Dentists.png`

### **Screenshot 2: Query 2 - Dentist Appointments**
```bash
echo "=== QUERY 2: Appointments for Dentist ID = 1 (Tony Smith) ==="
sqlite3 ADSDentalSurgery.db "SELECT a.appointment_id, a.appointment_date, a.appointment_time, a.status, p.patient_id, p.first_name AS patient_first_name, p.last_name AS patient_last_name, p.contact_phone AS patient_phone, p.email AS patient_email, s.surgery_name FROM Appointments a INNER JOIN Patients p ON a.patient_id = p.patient_id INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id WHERE a.dentist_id = 1 ORDER BY a.appointment_date, a.appointment_time;"
```
**Take screenshot** → Save as: `Query2_DentistAppointments.png`

### **Screenshot 3: Query 3 - Surgery Appointments**
```bash
echo "=== QUERY 3: Appointments at Surgery Location ID = 1 ==="
sqlite3 ADSDentalSurgery.db "SELECT a.appointment_id, a.appointment_date, a.appointment_time, a.status, d.first_name || ' ' || d.last_name AS dentist_name, d.specialization, p.first_name || ' ' || p.last_name AS patient_name FROM Appointments a INNER JOIN Dentists d ON a.dentist_id = d.dentist_id INNER JOIN Patients p ON a.patient_id = p.patient_id WHERE a.surgery_id = 1 ORDER BY a.appointment_date, a.appointment_time;"
```
**Take screenshot** → Save as: `Query3_SurgeryAppointments.png`

### **Screenshot 4: Query 4 - Patient Appointments**
```bash
echo "=== QUERY 4: Patient Appointments on Specific Date ==="
sqlite3 ADSDentalSurgery.db "SELECT a.appointment_id, a.appointment_time, a.status, d.first_name || ' ' || d.last_name AS dentist_name, d.specialization, s.surgery_name FROM Appointments a INNER JOIN Dentists d ON a.dentist_id = d.dentist_id INNER JOIN Surgeries s ON a.surgery_id = s.surgery_id WHERE a.patient_id = 3 AND a.appointment_date = '2013-09-12' ORDER BY a.appointment_time;"
```
**Take screenshot** → Save as: `Query4_PatientAppointments.png`

---

## 🚀 **Quick All-in-One Script**

Or run this single command to see all queries:

```bash
cd /Users/m/Library/CloudStorage/OneDrive-Personal/CS489/CS489/05ALab
./run-queries.sh
```

Then take screenshots of each section.

---

## 📱 **Screenshot Tips**

### **For Terminal Screenshots:**
- **Use Cmd+Shift+4** on macOS to select area
- **Include the command** and the results
- **Make sure text is readable** (zoom in if needed)
- **Use full-screen terminal** for better quality

### **For ERD Screenshots:**
- **Use high resolution** (at least 1920x1080)
- **Include the title** and all entities
- **Make sure relationships are visible**
- **Use PNG format** for best quality

---

## 📁 **Final File Structure**

Your submission should include:

```
05ALab/
├── myADSDentalSurgeryDBScript.sql          # SQL script
├── ER_Model_Documentation.md               # E-R model documentation
├── ADSDentalSurgery.db                     # SQLite database
├── ERD_ADS_Dental_Surgery.png             # ERD image
├── Query1_Dentists.png                     # Query 1 screenshot
├── Query2_DentistAppointments.png          # Query 2 screenshot
├── Query3_SurgeryAppointments.png          # Query 3 screenshot
└── Query4_PatientAppointments.png          # Query 4 screenshot
```

---

## ✅ **Submission Checklist**

- [ ] **ERD Image**: `ERD_ADS_Dental_Surgery.png`
- [ ] **Query 1 Screenshot**: `Query1_Dentists.png`
- [ ] **Query 2 Screenshot**: `Query2_DentistAppointments.png`
- [ ] **Query 3 Screenshot**: `Query3_SurgeryAppointments.png`
- [ ] **Query 4 Screenshot**: `Query4_PatientAppointments.png`
- [ ] **SQL Script**: `myADSDentalSurgeryDBScript.sql`
- [ ] **Documentation**: `ER_Model_Documentation.md`
- [ ] **Database File**: `ADSDentalSurgery.db`

---

## 🎓 **Lab Requirements Met**

1. ✅ **E-R Model**: Complete entity-relationship diagram
2. ✅ **Database Implementation**: SQLite database with all tables
3. ✅ **Sample Data**: All provided data mapped and inserted
4. ✅ **SQL Queries**: All 4 required queries implemented
5. ✅ **Screenshots**: Query execution results captured
6. ✅ **Documentation**: Comprehensive design documentation

---

**🎉 Ready for submission! All requirements fulfilled.**

