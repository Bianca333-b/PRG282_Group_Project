-- ========================================
-- Superhero Database Script
-- File: Create_DB_Bianca.sql
-- ========================================
-- 1. Create the database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DB_Bianca')
BEGIN
    CREATE DATABASE DB_Bianca;
END
GO

-- 2. Use the database
USE DB_Bianca;
GO

-- 3. Create table for superheroes
IF OBJECT_ID('dbo.Superheroes', 'U') IS NOT NULL
    DROP TABLE dbo.Superheroes;
GO

CREATE TABLE dbo.Superheroes (
    HeroID NVARCHAR(50) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Superpower NVARCHAR(100) NOT NULL,
    Score INT NOT NULL,
    Rank NVARCHAR(10) NOT NULL,
    ThreatLevel NVARCHAR(50) NOT NULL
);
GO

-- 4. Insert some dummy heroes (optional)
INSERT INTO dbo.Superheroes (HeroID, Name, Age, Superpower, Score, Rank, ThreatLevel)
VALUES 
('H001', 'Lightning Bolt', 18, 'Electric Shock', 85, 'S-Rank', 'Finals Week'),
('H002', 'Iron Fist', 20, 'Super Strength', 70, 'A-Rank', 'Midterm Madness'),
('H003', 'Shadow', 19, 'Invisibility', 55, 'B-Rank', 'Group Project Gone Wrong'),
('H004', 'QuickStep', 21, 'Super Speed', 40, 'C-Rank', 'Pop Quiz');
GO

-- 5. Optional: Check data
SELECT * FROM dbo.Superheroes;
GO
