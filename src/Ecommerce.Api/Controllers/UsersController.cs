using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] object request)
    {
        return Ok(new { userId = Guid.NewGuid() });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] object request)
    {
        return Ok(new { token = "jwt-token", refreshToken = "refresh-token" });
    }

    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        return Ok(new { });
    }

    [HttpPut("profile")]
    public IActionResult UpdateProfile([FromBody] object request)
    {
        return Ok(new { updated = true });
    }

    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromBody] object request)
    {
        return Ok(new { token = "new-jwt-token", refreshToken = "new-refresh-token" });
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword([FromBody] object request)
    {
        return Ok(new { message = "Reset email sent" });
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] object request)
    {
        return Ok(new { message = "Password reset" });
    }
}
