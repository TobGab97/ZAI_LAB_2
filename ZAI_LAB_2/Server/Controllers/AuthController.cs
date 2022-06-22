//using ZAI_LAB_2.Server.KodyContext;
using ZAI_LAB_2.Server.Services.JwtToken;
using ZAI_LAB_2.Shared;
using ZAI_LAB_2.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ZAI_LAB_2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly LAB1Context _lab1Context;
        private readonly ILogger<AuthController> _logger;
        private readonly TokenGenerator _tokenGenrator;

        public AuthController(LAB1Context lab1Context, ILogger<AuthController> logger
            ,TokenGenerator tokenGenrator)
        {
            _lab1Context = lab1Context;
            _logger = logger;
            _tokenGenrator = tokenGenrator;
        }
        [HttpPost("Login")]
        public  IActionResult Login([FromBody] UserLogin DaneLogowania)
        {
            User User = _lab1Context.User
                .Include(x=>x.Usery)
                .ThenInclude(x=>x.Role)
                .FirstOrDefault(x => x.Login == DaneLogowania.Login);
            //Brak użytkownika
            if (User == null)
            {
                return Unauthorized();
            }
            //Nieprawidłowe hasło
            if (!BCrypt.Net.BCrypt.Verify(DaneLogowania.Password, User.PasswordHash)) 
            {
                return Unauthorized();
            }
            //GenerujToken
            var token = _tokenGenrator.GenerujToken(User);
            return Ok(token);
        }
    }
}
