using System;

namespace InitiativesPlus.Domain.Models
{
    public class InitiativeAction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int InitiativeId { get; set; }
        public Initiative Initiative { get; set; }
        public string Action { get; set; }
        public Int16 Progress { get; set; }
    }
}
