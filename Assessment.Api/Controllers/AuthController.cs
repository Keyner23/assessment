using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Assessment.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        // Validación simple de usuario
        if (dto.Username != "admin" || dto.Password != "123456")
            return Unauthorized("Credenciales incorrectas");

        // Claims estándar JWT
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, dto.Username), // sujeto
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // id único del token
            new Claim("role", "Admin") // rol
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: null, // dejar null si ValidateAudience = false
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
