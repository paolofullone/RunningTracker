using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace RunningTracker.Infrastructure.Database;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly DatabaseConfig _config;

    public DbConnectionFactory(IOptions<DatabaseConfig> config)
    {
        _config = config.Value;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_config.MSSQL);
    }
}
