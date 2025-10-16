<p align="center">
  <img src="https://img.shields.io/badge/One%20Kick%20Heroes%20Academy-%F0%9F%A6%B8%E2%80%8D%E2%99%80%EF%B8%8F-ff512f?style=for-the-badge&labelColor=6a11cb" width="400" />
</p>

# One Kick Heroes Academy — Setup Instructions *(AVD Only)*

---

## ⚙️ Project Name
**Superhero Database System**

---

## 🧭 1. Clone or Open the Project
**Steps:**
1. Open **Visual Studio 2019 / 2022**
2. Go to:  
   `File → Open → Project/Solution`
3. Select:  
   **SuperheroDatabase.sln**

---

## 🗄️ 2. Restore the Database in SQL Server

**In SQL Server Management Studio (SSMS):**

1. Connect to your server with:
Server name: .
Authentication: SQL Server Authentication
Login: sa
Password: sa2025@1

2. Open the script:  
`DB_Bianca.sql`
3. Click **Execute (F5)** to create the database and table.

✅ Database created: `DB_Bianca`  
✅ Table created: `Superheroes`

---

## 🔗 3. Verify the Connection String
Open:
Data/HeroDataAccess.cs


Ensure this line exists:
```csharp
string conn = "Server=.; Initial Catalog=DB_Bianca; User ID=sa; Password=sa2025@1;";
```
💡 If your SQL Server name differs (e.g., localhost\SQLEXPRESS), update it here.

---

## ▶️ 4. Run the Project
In Visual Studio:
Press Ctrl + F5 (Start Without Debugging)

You should see the main form with these buttons:

Button	Function

➕ Add	Add a new superhero

📋 View All	View all heroes

✏️ Update	Edit selected hero

❌ Delete	Remove a hero

🧾 Generate Report	Create summary file

📂 Upload CSV/TXT	Load heroes from file

---

## 📂 5. Upload CSV File
Click Upload CSV, then select:

Copy code
superheroes.csv
📊 The DataGridView should fill instantly with 30 superhero records.

---

## 🧪 6. Test App Functions
Feature	Action	Expected Result

Add	Fill all fields → click “Add”	Adds new superhero

View All	Click “View All”	Displays all heroes

Update	Select hero → edit → click “Update”	Updates hero details

Delete	Select hero → click “Delete”	Removes selected hero

Generate Report	Click “Generate Report”	Creates summary.txt

Upload CSV	Click → select superheroes.csv	Loads 30 heroes into grid

---

## 💾 7. If AVD Wipes Data
When AVD resets, just re-run the setup:

Reconnect to SSMS:

makefile
Copy code
Login: sa
Password: sa2025@1
Re-run:
DB_Bianca.sql

Re-open Visual Studio → Run → Upload CSV again.

🕐 Everything restores in under a minute.

---

## 🧾 8. Notes
📦 All essential files are in the project folder:

DB_Bianca.sql — database setup

superheroes.csv — hero data

summary.txt — generated report

superheroes.txt — optional text export

---
# 👨‍💻 Created By:
## One Kick Heroes Academy Team

“Because every hero deserves a database.”

<p align="center"> <img src="https://img.shields.io/badge/SQL%20Server-2019-red?style=flat-square" /> <img src="https://img.shields.io/badge/C%23-.NET%20Framework-blueviolet?style=flat-square" /> <img src="https://img.shields.io/badge/Visual%20Studio-2022-blue?style=flat-square" /> </p>
