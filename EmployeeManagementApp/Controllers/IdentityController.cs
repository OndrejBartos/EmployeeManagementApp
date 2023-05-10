using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementAPI.Controllers;

public class TokenGenerationRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IConfiguration _config;
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);

    public IdentityController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    public IActionResult GenerateToken([FromBody] TokenGenerationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TokenLifetime),
            signingCredentials: credentials);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}
