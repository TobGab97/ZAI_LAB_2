using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using ZAI_LAB_2.Shared;

namespace ZAI_LAB_2.Server.Controllers
{
    public class UserController : Controller
    {

        private readonly LAB1Context _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, LAB1Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        // GET: UserController/Details/5
        public ActionResult GetUser(int id)
        {
            User user = _context.User.Include(u => u.Users).FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("GetUsers")]
        public ActionResult GetUsers()
        {
            var users = _context.User.Include(u => u.Users);

            if (users.Any())
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
