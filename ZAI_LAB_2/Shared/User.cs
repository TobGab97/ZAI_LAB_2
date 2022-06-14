using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

#nullable disable

namespace ZAI_LAB_2.Shared
{
    public partial class User
    {
        public User()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public DateTime AddTime { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
