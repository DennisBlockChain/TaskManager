using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Morning" },
                new Category { Id = 2, Name = "Afternoon" },
                new Category { Id = 3, Name = "Evening" },
                new Category { Id = 4, Name = "Night" }
                );
        }
        public DbSet<TaskManager.Models.Tasks> Tasks { get; set; }
        public DbSet<TaskManager.Models.Category> Category { get; set; }
    }
}