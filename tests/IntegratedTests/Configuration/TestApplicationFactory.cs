using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;
using Microsoft.Extensions.DependencyInjection;
using RunningTracker.Infra;

namespace IntegratedTests.Configuration
{
    public class TestApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPassword("@TestPassword123")
            .WithPortBinding(1433, true)
            .Build();

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            var connectionString = _dbContainer.GetConnectionString();
            var databaseInitializer = new DatabaseInitializer(connectionString);
            await databaseInitializer.InitializeDatabaseAsync();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings-integrated.json")
                    .AddUserSecrets<Program>()
                    .Build();

                config.AddConfiguration(integrationConfig);
            });

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
        }
    }
}

// I've decided to leave both configs here, the one needed for testcontainers and if we run via docker compose, only the ConfigureWebHost method will be used.

// I could remove the docker-compose, initialization files at src/Infra and it would work fine.

// To see it in action, open the docker while the test runs, it will be created and destroyed. 