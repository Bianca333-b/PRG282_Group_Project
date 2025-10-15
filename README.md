<p align="center">
  <img src="https://img.shields.io/badge/Superhero-Database-ff512f?style=for-the-badge&labelColor=6a11cb" 
       alt="Superhero Database" 
       width="500">
</p>

---

# One Kick Heroes Academy — Superhero Database System


[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge\&logo=c-sharp\&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-0078D7?style=for-the-badge\&logo=windows\&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge\&logo=github\&logoColor=white)](https://github.com/)
[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge\&logo=.net\&logoColor=white)](https://dotnet.microsoft.com/)

---

## Module Information 

* **Module:** PRG2782 (Programming 2782)
* **Year:** 2025
* **Institution:** Belgium Campus ITversity

---

## Team Members

| Name           | Role      | Feature / Responsibility                        | Branch               |
| -------------- | --------- | ----------------------------------------------- | -------------------- |
| ![Bianca](https://img.shields.io/badge/Bianca-ff6f61) | Developer | Project setup, UI design, Add Superhero feature | Bianca-AddHero       |
| ![Aqeel](https://img.shields.io/badge/Aqeel-ff6f61)| Developer | View & Update features                          | Aqeel-ViewUpdate     |
| ![Roebin](https://img.shields.io/badge/Roebin-ff6f61)| Developer | Delete feature + Validation & Error Handling    | Roebin-DeleteFeature |
| ![Willem](https://img.shields.io/badge/Willem-ff6f61)| Developer | Summary Report + Git Progress Documentation     | Willem-SummaryReport |

---

## Project Overview

The **Superhero Database System** is a Windows Forms C# application developed for the One Kick Heroes Academy, a fictional sister campus of Belgium Campus.

It replaces manual paper-based records with a digital system for managing trainee heroes. The application allows assessors to:

* Record hero information
* Calculate rankings and threat levels automatically
* Generate reports for analytics

This streamlines tracking and evaluating hero trainees efficiently.

---

## Core Features

| # | Feature                     | Description                                                                                                                                              |
| - | --------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1 | Add New Superhero           | Enter hero details (ID, Name, Age, Superpower, Exam Score). The system calculates Rank & Threat Level automatically and saves data to `superheroes.txt`. |
| 2 | View All Superheroes        | Display all heroes from `superheroes.txt` in a DataGridView, showing Rank and Threat Level clearly.                                                      |
| 3 | Update Superhero Info       | Edit hero records. Rank/Threat Level recalculates automatically if exam scores change.                                                                   |
| 4 | Delete a Superhero          | Delete a selected superhero from both the file and DataGridView, with confirmation prompts.                                                              |
| 5 | Generate Summary Report     | Compute total heroes, average age, average exam score, and counts per rank. Display results and save to `summary.txt`.                                   |
| 6 | Version Control Integration | Project managed with GitHub, using meaningful commits and branch-based collaboration.                                                                    |

---

## Ranking System

| Score Range | Rank   | Threat Level                                       |
| ----------- | ------ | -------------------------------------------------- |
| 81–100      | S-Rank | Finals Week — Threat to the entire academy         |
| 61–80       | A-Rank | Midterm Madness — Threat to a department           |
| 41–60       | B-Rank | Group Project Gone Wrong — Threat to a study group |
| 0–40        | C-Rank | Pop Quiz — Threat to an individual student         |

---

## Technologies Used

```text
C#               - Core programming language
Windows Forms    - GUI development
Text Files       - Data storage (superheroes.txt & summary.txt)
Git + GitHub     - Version control & collaboration
.NET Framework   - Application runtime
```

---

## System Workflow

1. User enters hero details in the form interface
2. System calculates Rank and Threat Level automatically
3. Data is saved to `superheroes.txt`
4. Heroes can be viewed, updated, or deleted from the interface
5. Summary Report can be generated and saved in `summary.txt`

---

## File Outputs

| File                | Description                                                                     |
| ------------------- | ------------------------------------------------------------------------------- |
| `superheroes.txt`   | Stores all superhero records including Rank and Threat Level                    |
| `summary.txt`       | Stores summary statistics: total heroes, averages, and rank counts              |
| Git Progress Photos | Screenshots of Git commits, branches, and pull requests for assessment evidence |

---

## Project Highlights

* Automatic ranking system based on exam scores
* CSV-style text file storage for easy tracking
* Full CRUD functionality (Create, Read, Update, Delete)
* User-friendly interface with modern layout and color scheme
* Input validation and error handling for reliability
* Team collaboration via Git branching and pull requests
