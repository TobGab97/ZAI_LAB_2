using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ZAI_LAB_2.Shared
{
    public partial class Role
    {
        public Role()
        {
            UsersRoles = new HashSet<Users>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
      
        public virtual ICollection<Users> UsersRoles { get; set; }
    }
}
