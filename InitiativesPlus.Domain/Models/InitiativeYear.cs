using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Domain.Models
{
    public class InitiativeYear
    {
        public DateTime Year { get; set; }
        public int InitiativeId { get; set; }
        public Initiative Initiative { get; set; }
    }
}
