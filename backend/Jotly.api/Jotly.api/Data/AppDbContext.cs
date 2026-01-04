using Jotly.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jotly.api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Notes> Notes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notes)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
