using Microsoft.EntityFrameworkCore;
using School_Management_System.Models.Admin;

namespace School_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Classes> Class { get; set; }

        public DbSet<Fees> fees { get; set; }

        public DbSet<Subjets> subjets { get; set; }
    }
}
