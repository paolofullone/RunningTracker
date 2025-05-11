using Dapper;
using RunningTracker.Infrastructure.Database;
using System.Data;

namespace RunningTracker.Infrastructure.Repositories.Identity;

public class IdentityRepository(IDbConnectionFactory connection) : IIdentityRepository
{
    private readonly IDbConnection _connection = connection.CreateConnection();
    public async Task<Models.User> GetByEmailAsync(string email)
    {
        const string sql = "SELECT Email, PasswordHash, FirstName, LastName, IsActive, CreatedAt, UpdatedAt FROM Users WHERE Email = @Email AND IsActive = 1";
        return await _connection.QuerySingleOrDefaultAsync<Models.User>(sql, new { Email = email });
    }

    public async Task CreateUserAsync(Models.User user)
    {
        const string sql = @"
                INSERT INTO Users (Email, PasswordHash, FirstName, LastName, IsActive, CreatedAt, UpdatedAt)
                VALUES (@Email, @PasswordHash, @FirstName, @LastName, @IsActive, GETUTCDATE(), GETUTCDATE())";
        await _connection.ExecuteAsync(sql, user);
    }
}


//{
//  "username": "Paolo@123",
//  "email": "paolo@gmail.com",
//  "password": "Password123",
//  "firstName": "Paolo",
//  "lastName": "Fullone"
//}