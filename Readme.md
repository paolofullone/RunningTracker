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
