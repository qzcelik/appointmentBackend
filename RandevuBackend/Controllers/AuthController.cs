using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RandevuBackend.Data;
using RandevuBackend.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace RandevuBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User userDto)
    {
        if (_context.Users.Any(u => u.userMail == userDto.userMail)) 
        {
            return BadRequest("Bu mail zaten kayıtlı");
        }

        var user = new User
        {
            userName = userDto.userName,
            userMail = userDto.userMail,
            userPass = userDto.userPass
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Kayıt Başarılı");
    }

    [HttpPost("login")]
    [Produces("application/json")]
    public IActionResult Login([FromBody] LoginRequest userDto)
    {
        var user = _context.Users.FirstOrDefault(u => u.userName == userDto.userName);
        if (user == null)
        {
            return BadRequest("Kullanıcı Bulunamadı");
        }
        if (user.userPass.ToString() != userDto.userPass.ToString())
        {
            return BadRequest("Kullanıcı Şifre Yanlış");
        }

        var token = GenerateJTWToken(user);
        return Ok(token);
    }

    private string GenerateJTWToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.userMail),
            new Claim("id", user.id.ToString()),
            new Claim("name", user.userName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public class LoginRequest
    {
        public string userName {get; set; }
        public string userPass { get; set; }
    }
}