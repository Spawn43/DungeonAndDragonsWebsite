using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableEntity>()
                .HasMany(t => t.Players)
                .WithMany()
                .UsingEntity(j => j.ToTable("TablePlayers"));
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<TableEntity> Tables { get; set; }
        public DbSet<LoginTokenEntity> LoginTokens { get; set; }

    }
}
