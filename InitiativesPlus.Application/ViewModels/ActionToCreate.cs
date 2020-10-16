using System;

namespace InitiativesPlus.Application.ViewModels
{
    public class ActionToCreate
    {
        public int UserId { get; set; }
        public int InitiativeId { get; set; }
        public string Action { get; set; }
        public Int16 Progress { get; set; }
    }
}
