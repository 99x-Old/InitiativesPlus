using System.Collections.Generic;

namespace InitiativesPlus.Domain.Models
{
    public class InitiativeStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public ICollection<Initiative> Initiatives { get; set; }
    }
}
