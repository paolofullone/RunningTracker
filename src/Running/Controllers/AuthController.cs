using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunningTracker.Models;
using RunningTracker.Services.Identity;

namespace RunningTracker.Controllers;

[ApiController]
[Route("api/v1/")]
[ApiVersion("1.0")]
public class AuthController(IIdentityService identityService) : ControllerBase
{

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await identityService.AuthenticateAsync(login);
        if (token == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new { token });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] Register register)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await identityService.RegisterAsync(register);
            return Ok(new { message = $"User {register.Email} registered successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
