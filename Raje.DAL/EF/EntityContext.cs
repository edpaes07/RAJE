using Microsoft.EntityFrameworkCore;

namespace Raje.DAL.EF
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
                : base(options)
        {
        }

        //public virtual DbSet<EntidadeExemplo> EntidadeExemplo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
