using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace RunningTracker.Infra
{
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
}