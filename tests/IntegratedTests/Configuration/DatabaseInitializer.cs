using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IntegratedTests.Configuration
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InitializeDatabaseAsync()
        {
            var createDatabaseScript = @"
                IF DB_ID('RunningTrackerDb') IS NULL
                BEGIN
                    CREATE DATABASE RunningTrackerDb;
                END
            ";

            var initializeDatabaseScript = @"
                USE RunningTrackerDb;

                IF OBJECT_ID('Runs', 'U') IS NULL
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
                        '07:15:00',
                        '08:45:00',
                        GETDATE(),
                        GETDATE()
                    );
                END
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Create the database if it doesn't exist
                using (var createDbCommand = new SqlCommand(createDatabaseScript, connection))
                {
                    await createDbCommand.ExecuteNonQueryAsync();
                }

                // Initialize the database
                using (var initializeDbCommand = new SqlCommand(initializeDatabaseScript, connection))
                {
                    await initializeDbCommand.ExecuteNonQueryAsync();
                }
            }
        }
    }
}