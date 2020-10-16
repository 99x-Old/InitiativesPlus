namespace InitiativesPlus.Infrastructure.Data.StaticClasses
{
    public static class RoleTypes
    {
        public const string
            User = "User",
            InitiativeLead = "Initiative Lead",
            SuperAdmin = "Super Admin",
            InitiativeEvaluator = "Initiative Evaluator";

        public enum RoleIds
        {
            User = 1,
            InitiativeLead = 2,
            SuperAdmin = 3,
            InitiativeEvaluator = 4
        }
    }
}