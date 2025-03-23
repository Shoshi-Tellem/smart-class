using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using smart_class.Core.Classes;
using smart_class.Core.Entities;
using smart_class.Core.Extensions;
using smart_class.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IConfiguration configuration, DataContext context) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
    private readonly DataContext _context = context;
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] Login login)
    {
        // בדוק אם המנהל קיים
        Admin? admin = await _context.Admin
            .FirstOrDefaultAsync(a => a.Email == login.Email && a.Password == login.Password);
        if (admin != null)
        {
            return auth("Admin", admin);
        }

        // בדוק אם המורה קיים
        Teacher? teacher = await _context.Teacher
            .FirstOrDefaultAsync(t => t.Email == login.Email && t.Password == login.Password);
        if (teacher != null)
        {
            return auth("Teacher", teacher);
        }

        // בדוק אם הסטודנט קיים
        Student? student = await _context.Student
            .FirstOrDefaultAsync(s => s.Email == login.Email && s.Password == login.Password);
        if (student != null)
        {
            return auth("Student", student);
        }

        return Unauthorized();
    }

    private IActionResult auth(string role, User user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role),
            new Claim("InstitutionId", user.InstitutionId.ToString())
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JWT:Issuer"),
            audience: _configuration.GetValue<string>("JWT:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(6),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return Ok(new { Token = tokenString });
    }
}