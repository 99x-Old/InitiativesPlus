using System.Collections.Generic;

namespace InitiativesPlus.Domain.Models
{
    public class Initiative : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<InitiativeYear> InitiativeYears { get; set; }
        public int StatusId { get; set; }
        public InitiativeStatus InitiativeStatus { get; set; }
    }
}
