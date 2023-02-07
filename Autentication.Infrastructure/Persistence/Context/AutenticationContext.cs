using Autentication.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Autentication.Infrastructure.Persistence.Context
{
    public class AutenticationContext : DbContext
    {
        public AutenticationContext(DbContextOptions<AutenticationContext> options)
            : base(options) {}

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutenticationContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}