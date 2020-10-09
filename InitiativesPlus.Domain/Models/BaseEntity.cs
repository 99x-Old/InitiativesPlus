using System;

namespace InitiativesPlus.Domain.Models
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
