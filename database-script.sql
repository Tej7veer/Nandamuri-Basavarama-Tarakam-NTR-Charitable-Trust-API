-- Ashadeep Trust database setup script for SQL Server
-- Run this in SSMS connected to LAPTOP-7GTC680A\MSSQLSERVER01 (or your instance).
-- Column types/sizes here match the [MaxLength] attributes in Models/Entities.cs
-- exactly, and table names match what EF Core generates from the DbSet names in
-- AppDbContext.cs - this is the same schema EnsureCreated() would produce, just
-- runnable directly as your own Windows-authenticated user rather than requiring
-- the app's identity to have CREATE DATABASE permission.

IF DB_ID('TrustAppDb') IS NULL
BEGIN
    CREATE DATABASE TrustAppDb;
END
GO

USE TrustAppDb;
GO

CREATE TABLE Programs (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Title         NVARCHAR(150)  NOT NULL,
    Description   NVARCHAR(1000) NOT NULL,
    IconKey       NVARCHAR(60)   NOT NULL,
    IsLaunched    BIT            NOT NULL,
    CreatedAt     DATETIME2      NOT NULL
);
GO

CREATE TABLE ProjectEvents (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Title         NVARCHAR(150)  NOT NULL,
    Description   NVARCHAR(2000) NOT NULL,
    EventDate     DATETIME2      NULL,
    Location      NVARCHAR(200)  NULL,
    IsUpcoming    BIT            NOT NULL,
    CreatedAt     DATETIME2      NOT NULL
);
GO

CREATE TABLE GalleryImages (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Caption       NVARCHAR(200)  NOT NULL,
    ImageUrl      NVARCHAR(500)  NOT NULL,
    IsPlaceholder BIT            NOT NULL,
    CreatedAt     DATETIME2      NOT NULL
);
GO

CREATE TABLE ContactMessages (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    FullName      NVARCHAR(120)  NOT NULL,
    Email         NVARCHAR(200)  NOT NULL,
    Phone         NVARCHAR(30)   NULL,
    Message       NVARCHAR(2000) NOT NULL,
    SubmittedAt   DATETIME2      NOT NULL
);
GO

CREATE TABLE VolunteerApplications (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    FullName       NVARCHAR(120)  NOT NULL,
    Email          NVARCHAR(200)  NOT NULL,
    Phone          NVARCHAR(30)   NOT NULL,
    AreaOfInterest NVARCHAR(500)  NULL,
    Message        NVARCHAR(1000) NULL,
    SubmittedAt    DATETIME2      NOT NULL
);
GO

CREATE TABLE DonationInquiries (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    FullName      NVARCHAR(120)   NOT NULL,
    Email         NVARCHAR(200)   NOT NULL,
    Amount        DECIMAL(18,2)   NOT NULL,
    Note          NVARCHAR(500)   NULL,
    SubmittedAt   DATETIME2       NOT NULL
);
GO

CREATE TABLE TeamMembers (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(120)  NOT NULL,
    Role          NVARCHAR(80)   NOT NULL,
    PhotoUrl      NVARCHAR(300)  NULL,
    DisplayOrder  INT            NOT NULL,
    CreatedAt     DATETIME2      NOT NULL
);
GO

CREATE TABLE Certificates (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(120)  NOT NULL,
    Status        NVARCHAR(60)   NOT NULL,
    DocumentUrl   NVARCHAR(300)  NULL,
    DisplayOrder  INT            NOT NULL,
    CreatedAt     DATETIME2      NOT NULL
);
GO

CREATE TABLE Videos (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Caption       NVARCHAR(150)  NOT NULL,
    VideoUrl      NVARCHAR(300)  NOT NULL,
    PosterUrl     NVARCHAR(300)  NULL,
    CreatedAt     DATETIME2      NOT NULL
);
GO

CREATE TABLE NewsPosts (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Title         NVARCHAR(200)  NOT NULL,
    Body          NVARCHAR(4000) NOT NULL,
    PublishedAt   DATETIME2      NOT NULL
);
GO

-- Seed data - matches AppDbContext.OnModelCreating's HasData calls exactly,
-- so the app behaves identically to the SQLite version on first run.

SET IDENTITY_INSERT Programs ON;
INSERT INTO Programs (Id, Title, Description, IconKey, IsLaunched, CreatedAt) VALUES
(1, 'Education', 'Supporting learning resources and school access for underserved children.', 'graduation-cap', 0, '2026-01-01'),
(2, 'Healthcare', 'Planned health camps and awareness drives in local communities.', 'heart-pulse', 0, '2026-01-01'),
(3, 'Food Support', 'Meal and ration support programs for families in need.', 'utensils', 0, '2026-01-01'),
(4, 'Environment', 'Tree plantation and cleanliness drives, launching soon.', 'leaf', 0, '2026-01-01'),
(5, 'Women Empowerment', 'Skill-building and livelihood support initiatives for women.', 'users', 0, '2026-01-01');
SET IDENTITY_INSERT Programs OFF;
GO

SET IDENTITY_INSERT TeamMembers ON;
INSERT INTO TeamMembers (Id, Name, Role, PhotoUrl, DisplayOrder, CreatedAt) VALUES
(1, 'Trustee Name', 'Chairman', NULL, 1, '2026-01-01'),
(2, 'Trustee Name', 'President', NULL, 2, '2026-01-01'),
(3, 'Trustee Name', 'Secretary', NULL, 3, '2026-01-01'),
(4, 'Trustee Name', 'Treasurer', NULL, 4, '2026-01-01');
SET IDENTITY_INSERT TeamMembers OFF;
GO

SET IDENTITY_INSERT Certificates ON;
INSERT INTO Certificates (Id, Name, Status, DocumentUrl, DisplayOrder, CreatedAt) VALUES
(1, 'Trust Registration', 'Pending upload', NULL, 1, '2026-01-01'),
(2, '80G Certificate', 'Applied for', NULL, 2, '2026-01-01'),
(3, '12A Certificate', 'Applied for', NULL, 3, '2026-01-01'),
(4, 'PAN Card', 'Pending upload', NULL, 4, '2026-01-01');
SET IDENTITY_INSERT Certificates OFF;
GO

PRINT 'TrustAppDb created with 10 tables and seed data.';
