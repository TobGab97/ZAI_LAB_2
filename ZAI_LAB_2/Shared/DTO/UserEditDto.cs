using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1MarekMatkowski.Shared.DTO
{
    public class UserEditDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<RoleDto> roles { get; set; }
    }
}
