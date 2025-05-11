using Dapper;
using RunningTracker.Infrastructure.Database;
using RunningTracker.Models;

namespace RunningTracker.Infrastructure.Repositories.Run;

public class RunRepository : IRunRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public RunRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<RunActivity> AddRunAsync(RunActivity run)
    {
        const string sql = """
                               INSERT INTO Runs (Distance, Duration, Pace, Date, StartTime, EndTime, CreatedAt, UpdatedAt)
                               VALUES (@Distance, @Duration, @Pace, @Date, @StartTime, @EndTime, @CreatedAt, @UpdatedAt);
                               SELECT LAST_INSERT_ID();

                           """;

        run.CreatedAt = DateTime.UtcNow;
        run.UpdatedAt = DateTime.UtcNow;

        using (var connection = _dbConnectionFactory.CreateConnection())
        {
            run.Id = await connection.ExecuteScalarAsync<int>(sql, run);
        }

        return run;
    }

    public async Task<IEnumerable<RunActivity>> GetAllRunsAsync()
    {
        const string sql = "SELECT Id, Distance, Duration, Pace, Date, StartTime, EndTime, CreatedAt, UpdatedAt FROM Runs;";
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<RunActivity>(sql);
    }

    public async Task<IEnumerable<RunActivity>> GetRunsByDateAsync(DateTime date)
    {
        const string sql = "SELECT Id, Distance, Duration, Pace, Date, StartTime, EndTime, CreatedAt, UpdatedAt FROM Runs WHERE Date = @Date;";

        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<RunActivity>(sql, new { Date = date });
    }

    public async Task<RunActivity> GetRunByIdAsync(int id)
    {
        const string sql = "SELECT Id, Distance, Duration, Pace, Date, StartTime, EndTime, CreatedAt, UpdatedAt FROM Runs WHERE Id = @Id;";
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<RunActivity>(sql, new { Id = id }) ?? new RunActivity();
    }

    public async Task<RunActivity> UpdateRunAsync(RunActivity run)
    {
        const string sql = @"
                UPDATE Runs
                SET Distance = @Distance, Duration = @Duration, Pace = @Pace, Date = @Date, StartTime = @StartTime, EndTime = @EndTime, UpdatedAt = @UpdatedAt
                WHERE Id = @Id;";

        run.UpdatedAt = DateTime.UtcNow;
        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, run);
        return run;
    }

    public async Task DeleteRunAsync(int id)
    {
        const string sql = "DELETE FROM Runs WHERE Id = @Id;";
        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task DeleteAllRunsAsync()
    {
        const string sql = "DELETE FROM Runs;";
        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql);
    }
}