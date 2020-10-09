using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InitiativesPlus.Infrastructure.Data.Context
{
    public class InitiativesPlusDbContext : DbContext
    {
        private const string EntityCreatedPropertyName = "CreatedDate";
        private const string EntityModifiedPropertyName = "ModifiedDate";
        public InitiativesPlusDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Initiative> Initiatives { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<InitiativeYear> InitiativeYears { get; set; }
        public DbSet<UserInitiative> UserInitiatives { get; set; }
        public DbSet<InitiativeStatus> InitiativeStatuses { get; set; }

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

            modelBuilder.Entity<Initiative>()
                .HasOne(a => a.InitiativeStatus)
                .WithMany(b => b.Initiatives)
                .HasForeignKey(a => a.StatusId);

            modelBuilder.Entity<InitiativeYear>()
                .HasKey(p => new { p.Year, p.InitiativeId });

            modelBuilder.Entity<UserInitiative>()
                .HasKey(p => new { p.UserId, p.InitiativeId });
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            #region Automatically set "CreatedDate" property on an a new entity
            var addedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            addedEntities.ForEach(E =>
            {
                var prop = E.Metadata.FindProperty(EntityCreatedPropertyName);
                if (prop != null)
                {
                    E.Property(EntityCreatedPropertyName).CurrentValue = DateTime.Now;
                }
            });
            #endregion

            #region Automatically set "ModifiedDate" property on an a new entity
            var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            editedEntities.ForEach(E =>
            {
                var prop = E.Metadata.FindProperty(EntityModifiedPropertyName);
                if (prop != null)
                {
                    E.Property(EntityModifiedPropertyName).CurrentValue = DateTime.Now;
                }
            });
            #endregion

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
