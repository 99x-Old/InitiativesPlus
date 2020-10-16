using System;

namespace InitiativesPlus.Application.ViewModels
{
    public class ActionToReturn
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InitiativeId { get; set; }
        public string Action { get; set; }
        public Int16 Progress { get; set; }
    }
}
