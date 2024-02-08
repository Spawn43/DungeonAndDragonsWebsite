using DungeonAndDragonsWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace DungeonAndDragonsWebsite.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>()
                .HasMany(t => t.Players)
                .WithMany()
                .UsingEntity(j => j.ToTable("TablePlayers"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<LoginToken> LoginTokens { get; set; }

    }
}
