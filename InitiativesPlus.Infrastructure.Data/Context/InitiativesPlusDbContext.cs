using InitiativesPlus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativesPlus.Infrastructure.Data.Context
{
    public class InitiativesPlusDbContext : DbContext
    {
        public InitiativesPlusDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Initiative> Initiatives { get; set; }
    }
}
