# Running API Project

This is an backend API project that allows users to track their running activities. The project is built using .NET 8 amd a very tinny architecture.

For the database I choose the MySQL due to the fact that it's a very popular database and it's easy to use. The database is running on a docker container.

## Installation

1. Clone the repository
2. Run docker-compose up to initialize database
3. Run the project via Visual Studio

## Docker compose commands and issues

If the script is modified, we need to remove the volume then run the docker-compose up again.

```bash
docker-compose down -v
docker-compose up
```

## FunctionalTests

In order to run the functional tests, you need to run the project first. Then, you can run the tests.

To run the project, it's faster to navigate to the project folder (RunningTracker\src\Running\bin\Debug\net8.0) and run the following command:

```bash
dotnet RunningTracker.dll
```

Then you can run the tests via Visual Studio.

## Integrated Tests

In order to run the integrated tests, you need to run the docker compose file first. Then, you can run the tests.

To run the docker compose file, you can run the following command:

```bash
docker-compose up
```

Then you can run the tests via Visual Studio.

The integrated tests can be executed using the docker-compose file that will make use of src/Infra files init.sql and entrypoint.sh to create the database and run the project or only the testcontainer nuget already configured in the IntegratedTests project.

I've had some issues running via testcontainers, had to split the database initializer into 2 methods and replace the Database in the connection string at builder.ConfigureServices:

```csharp
builder.ConfigureServices(services =>
{
    // Configure your services to use the test container's connection string
    var connectionString = _dbContainer.GetConnectionString();

    // Update your services to use the connection string
    services.Configure<DatabaseConfig>(options =>
    {
        options.MSSQL = $"{connectionString};Database=RunningTrackerDb;";
    });

    services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
});
```

```csharp
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
```

Maybe there is another way, but it worked like this, I am fine with it for now.
