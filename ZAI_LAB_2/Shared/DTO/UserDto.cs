using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1MarekMatkowski.Shared.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public DateTime AddTime { get; set; }

        public List<RoleDto> roles { get; set; }
    }
}
