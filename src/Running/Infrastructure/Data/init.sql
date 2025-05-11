-- Drop the database if it exists
IF
EXISTS (SELECT * FROM sys.databases WHERE name = 'RunningTrackerDb')
BEGIN
    DROP
DATABASE RunningTrackerDb;
END
GO

-- Create the database
CREATE
DATABASE RunningTrackerDb;
GO

-- Use the newly created database
USE RunningTrackerDb;
GO

-- Create the Runs table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Runs]') AND type in (N'U'))
BEGIN
CREATE TABLE Runs
(
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Distance  FLOAT    NOT NULL,
    Duration  TIME     NOT NULL,
    Pace      FLOAT    NOT NULL,
    Date      DATE     NOT NULL,
    StartTime TIME     NOT NULL,
    EndTime   TIME     NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
END
GO

-- Insert initial data into the Runs table
INSERT INTO Runs (
    Distance,
    Duration,
    Pace,
    Date,
    StartTime,
    EndTime,
    CreatedAt,
    UpdatedAt
)
VALUES (
    10.0,
    '01:00:00',
    6.0,
    '2024-08-10',
    '07:00:00',
    '08:00:00',
    GETDATE(),
    GETDATE()
);
GO

-- Users
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1, -- Logical deletion (1 = active, 0 = deactivated)
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Insert 5 random users
INSERT INTO Users (Username, Email, PasswordHash, FirstName, LastName, IsActive, CreatedAt, UpdatedAt)
VALUES
    ('joe.fullone@gmail.com', '$2a$11$3qZ5gY8Y9gZ5Xz6kY8Y9gY8Y9gZ5Xz6kY8Y9gY8Y9gZ5Xz6kY8Y9', 'Joe', 'Fullone', 1, GETUTCDATE(), GETUTCDATE()),
    ('lola.fullone@yahoo.com', '$2a$11$3qZ5gY8Y9gZ5Xz6kY8Y9gY8Y9gZ5Xz6kY8Y9gY8Y9gZ5Xz6kY8Y9', 'Lola', 'Fullone', 1, GETUTCDATE(), GETUTCDATE());


-- Verify the inserted users
SELECT * FROM Users;