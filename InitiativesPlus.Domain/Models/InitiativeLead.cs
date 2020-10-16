namespace InitiativesPlus.Domain.Models
{
    public class InitiativeLead
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int InitiativeId { get; set; }
        public Initiative Initiative { get; set; }
    }
}
