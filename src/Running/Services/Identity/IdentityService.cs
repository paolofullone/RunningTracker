using Microsoft.IdentityModel.Tokens;
using RunningTracker.Infrastructure.Repositories.Identity;
using RunningTracker.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RunningTracker.Services.Identity;

public class IdentityService(IIdentityRepository identityRepository, IConfiguration configuration) : IIdentityService
{
    public async Task<string> AuthenticateAsync(Login login)
    {
        var user = await identityRepository.GetByEmailAsync(login.Email);

        // não gostei dessa parte, avaliar depois...
        if (user is null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            return null;

        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryInMinutes"])),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task RegisterAsync(Register register)
    {
        var existingUser = await identityRepository.GetByEmailAsync(register.Email);

        if (existingUser is not null)
        {
            throw new Exception($"User {register.Email} already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);
        var user = new User
        {
            Email = register.Email,
            PasswordHash = passwordHash,
            FirstName = register.FirstName,
            LastName = register.LastName,
            IsActive = true
        };

        await identityRepository.CreateUserAsync(user);
    }
}


