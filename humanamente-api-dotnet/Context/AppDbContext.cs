using Microsoft.EntityFrameworkCore;
using humanamente.Models;

namespace humanamente.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profession> Professions { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}