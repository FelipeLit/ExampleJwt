using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace Backend.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private readonly ExampleContext _context;
    private readonly JwtSettings _jwtSettings;
    public UserController(ExampleContext context, IOptions<JwtSettings> options)
    {
        _context = context;
        _jwtSettings = options.Value;
    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserCred userCred)
    {
        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.UserName == userCred.username && u.Password == userCred.password);
 
        if (user == null)

            return Unauthorized();
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
            var tokenDesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] 
                { new Claim(ClaimTypes.Name, user.UserName)
                }),
            Expires = DateTime.UtcNow.AddSeconds(20),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenhandler.CreateToken(tokenDesc);
        string finalToken = tokenhandler.WriteToken(token);


        return Ok(finalToken);
    }
}