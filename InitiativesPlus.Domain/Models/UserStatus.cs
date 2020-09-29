using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Domain.Models
{
    public class UserStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
