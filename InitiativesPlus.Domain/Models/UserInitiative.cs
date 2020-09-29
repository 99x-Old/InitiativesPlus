using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Domain.Models
{
    public class UserInitiative
    {
        public int UserId { get; set; }
        public User User { get; set; }  
        public int InitiativeId { get; set; }
        public Initiative Initiative { get; set; }
    }
}
