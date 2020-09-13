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
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User to UserRole dependancy
            modelBuilder.Entity<User>()
                    .HasOne(a => a.UserRole)
                    .WithMany(b => b.Users)
                    .HasForeignKey(a => a.RoleId);

        }
    }
}
