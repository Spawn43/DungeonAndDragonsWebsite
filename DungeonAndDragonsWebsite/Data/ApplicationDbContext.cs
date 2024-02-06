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
            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventPlanner)
                .WithMany()
                .HasForeignKey(e => e.EventPlannerID);

            modelBuilder.Entity<Table>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Tables)
                .HasForeignKey(t => t.EventID);

            modelBuilder.Entity<Table>()
                .HasOne(t => t.DungeonMaster)
                .WithMany()
                .HasForeignKey(t => t.DungeonMasterID)
                .OnDelete(DeleteBehavior.Restrict); // Add this line to specify ON DELETE NO ACTION
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
