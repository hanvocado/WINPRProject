using Microsoft.EntityFrameworkCore;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Professor> Professors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasOne(t => t.Professor)
                        .WithMany(p => p.Topics)
                        .HasForeignKey(t => t.ProfessorId);
            });
        }
    }
}
