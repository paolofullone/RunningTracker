using Dapper;
using RunningTracker.Infra;
using RunningTracker.Models;

namespace RunningTracker.Repositories
{
    public class RunRepository : IRunRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RunRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Run> AddRunAsync(Run run)
        {
            const string sql = @"
                INSERT INTO Runs (Distance, Duration, Pace, Date, StartTime, EndTime, CreatedAt, UpdatedAt)
                VALUES (@Distance, @Duration, @Pace, @Date, @StartTime, @EndTime, @CreatedAt, @UpdatedAt);
                SELECT LAST_INSERT_ID();";

            run.CreatedAt = DateTime.UtcNow;
            run.UpdatedAt = DateTime.UtcNow;

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                run.Id = await connection.ExecuteScalarAsync<int>(sql, run);
            }
            return run;
        }

        public async Task<IEnumerable<Run>> GetAllRunsAsync()
        {
            const string sql = "SELECT * FROM Runs;";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Run>(sql);
            }
        }

        public async Task<Run> GetRunByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Runs WHERE Id = @Id;";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Run>(sql, new { Id = id });
            }
        }

        public async Task<Run> UpdateRunAsync(Run run)
        {
            const string sql = @"
                UPDATE Runs
                SET Distance = @Distance, Duration = @Duration, Pace = @Pace, Date = @Date, StartTime = @StartTime, EndTime = @EndTime, UpdatedAt = @UpdatedAt
                WHERE Id = @Id;";

            run.UpdatedAt = DateTime.UtcNow;
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, run);
            }
            return run;
        }

        public async Task DeleteRunAsync(int id)
        {
            const string sql = "DELETE FROM Runs WHERE Id = @Id;";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
