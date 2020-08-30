using InitiativesPlus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Application.ViewModels
{
    public class InitiativeViewModel
    {
        public IEnumerable<Initiative> Initiatives { get; set; }
    }
}
