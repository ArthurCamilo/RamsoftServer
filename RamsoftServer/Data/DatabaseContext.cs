using Microsoft.EntityFrameworkCore;
using RamsoftServer.Domain.Entities;

namespace RamsoftServer.Infrastructure
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Column> Columns { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Card>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
