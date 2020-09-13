using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Domain.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
