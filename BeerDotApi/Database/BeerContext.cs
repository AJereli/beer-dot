using System;
using System.Linq;
using BeerDotApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerDotApi.Database
{
    public class BeerContext: DbContext
    {
        public DbSet<UserEntity> User { get; set; }
        public DbSet<BeerEntity> Beer { get; set; }
        public DbSet<BeerReviewEntity> Review { get; set; }
        
        public BeerContext(DbContextOptions options/*, UserContext userContext */) : base(options)
        {
            // _userContext = userContext;
        }
        
        public override int SaveChanges()
        {
            SaveUpdateHook();
            
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(p => new { p.Email })
                .IsUnique(true);
            modelBuilder.Entity<BeerEntity>()
                .HasIndex(p => new { p.Title })
                .IsUnique(true);
        }

        private void SaveUpdateHook()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                                e.State == EntityState.Added
                                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (!(entityEntry.Entity is BaseEntity baseEntity)) continue;
                if (entityEntry.State == EntityState.Added)
                {
                    baseEntity.CreatedDate = DateTime.Now;
                }
                else
                {
                    baseEntity.UpdatedDate = DateTime.Now;
                }
            }
        }
    }
}