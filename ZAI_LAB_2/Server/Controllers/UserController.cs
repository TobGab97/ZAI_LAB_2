using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ZAI_LAB_2.Shared;
using ZAI_LAB_2.Shared.DTO;

namespace ZAI_LAB_2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly LAB1Context _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, LAB1Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Wszyscy")]
        public ActionResult GetUsers()
        {
            var userss = _context.User.Include(x => x.Usery).ThenInclude(x => x.Role);

            List<UserDto> userDtos = new List<UserDto>();
            foreach (var user in userss)

            {
                List<RoleDto> roles = new List<RoleDto>();
                foreach (var item in user.Usery.Select(x => x.Role).ToList())
                {
                    roles.Add(new RoleDto { RoleId = item.Id, RoleName = item.RoleName });
                }

                userDtos.Add(new UserDto
                {
                    Login = user.Login,
                    UserId = user.Id,
                    AddTime = user.AddTime,
                    PasswordHash = user.PasswordHash,
                    roles = roles

                });
            }

            if (userDtos.Any())
            {
                return Ok(userDtos);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetUser(int id)
        {
            User user = _context.User.Include(x => x.Usery).ThenInclude(x => x.Role).FirstOrDefault(x => x.Id == id);
            List<RoleDto> roles = new List<RoleDto>();
            foreach (var item in user.Usery.Select(x => x.Role).ToList())
            {
                roles.Add(new RoleDto { RoleId = item.Id, RoleName = item.RoleName });
            }
            UserDto userDto = new UserDto
            {
                Login = user.Login,
                UserId = user.Id,
                AddTime = user.AddTime,
                PasswordHash = user.PasswordHash,
                roles = roles
            };

            if (userDto != null)
            {
                return Ok(userDto);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("Roles")]
        public ActionResult GetRoles()
        {
            var r = _context.Role;
            List<RoleDto> roles = new List<RoleDto>();
            foreach (var item in r)
            {
                roles.Add(new RoleDto { RoleId = item.Id, RoleName = item.RoleName });
            }

            return Ok(roles);
        }
        [HttpPost]
        public ActionResult AddUser(UserRegisterDto user)
        {
            var ist = _context.User.FirstOrDefault(x => x.Login == user.Login);
            if (ist == null)
            {

                User nowy = new User
                {
                    Login = user.Login,
                    AddTime = DateTime.Now,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                };

                _context.User.Add(nowy);
                foreach (var item in user.roles)
                {
                    _context.Users.Add(new Users
                    {
                        User = nowy,
                        UserID = nowy.Id,
                        Role = _context.Role.FirstOrDefault(x => x.Id == item.RoleId),
                        RoleID = item.RoleId
                    });
                }
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("jest już taki użytkownik");
        }
        [HttpPut]
        public ActionResult EditUser(UserEditDto user)
        {
            var ist = _context.User.FirstOrDefault(x => x.Id == user.Id);
            if (ist != null)
            {
                ist.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                ist.Login = user.Login;

                var x = _context.Users.Where(x => x.UserID == ist.Id);
                _context.Users.RemoveRange(x);

                foreach (var item in user.roles)
                {
                    _context.Users.Add(new Users
                    {
                        User = ist,
                        UserID = ist.Id,
                        Role = _context.Role.FirstOrDefault(x => x.Id == item.RoleId),
                        RoleID = item.RoleId
                    });
                }


                _context.SaveChanges();
                return Ok();
            }
            return NotFound("Nie ma takiego użytkownika");
        }
        [HttpDelete("{id}")]
        public ActionResult EditUser(int id)
        {
            var user = _context.User.Include(x => x.Usery).FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound("nie ma takiego użytkownika");
            }
            _context.User.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
