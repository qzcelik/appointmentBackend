using Microsoft.AspNetCore.Mvc;
using RandevuBackend.Data;
using RandevuBackend.Models;

namespace RandevuBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
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
            userPass = userDto.userMail
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Kayıt Başarılı");
    }
}