using Microsoft.EntityFrameworkCore;
using Raje.DL.DB.Admin;

namespace Raje.DAL.EF
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
                : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Media> Media { get; set; }

        public virtual DbSet<Assessment> Assessment { get; set; }

        public virtual DbSet<Contents> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [User]
            modelBuilder.Entity<User>()
                   .HasMany(b => b.Assessment);
            #endregion

            #region [Contents]
            modelBuilder.Entity<Contents>()
                   .HasMany(b => b.Assessment);
            #endregion

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
