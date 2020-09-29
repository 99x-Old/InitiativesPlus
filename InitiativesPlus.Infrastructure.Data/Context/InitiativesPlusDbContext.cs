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
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<InitiativeYear> InitiativeYears { get; set; }
        public DbSet<UserInitiative> UserInitiatives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.UserRole)
                .WithMany(b => b.Users)
                .HasForeignKey(a => a.RoleId);

            modelBuilder.Entity<User>()
                .HasOne(a => a.UserStatus)
                .WithMany(b => b.Users)
                .HasForeignKey(a => a.StatusId);

            modelBuilder.Entity<InitiativeYear>()
                .HasKey(p => new { p.Year, p.InitiativeId });

            modelBuilder.Entity<UserInitiative>()
                .HasKey(p => new { p.UserId, p.InitiativeId });
        }
    }
}
