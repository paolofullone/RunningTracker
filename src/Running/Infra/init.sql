-- Drop the database if it exists
DROP DATABASE IF EXISTS RunningTrackerDb;

-- Create the database
CREATE DATABASE RunningTrackerDb;

-- Use the newly created database
USE RunningTrackerDb;

-- Create the Runs table
CREATE TABLE IF NOT EXISTS Runs (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Distance FLOAT NOT NULL,
    Duration TIME NOT NULL,
    Pace FLOAT NOT NULL,
    Date DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL
);

-- Insert initial data into the Runs table
INSERT INTO
    Runs (
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
        NOW(),
        NOW()
    );