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

        public DbSet<Subjects> subjets { get; set; }

        public DbSet<StudentDetails> students { get; set; }

        public DbSet<TeacherDetails> Teachers { get; set; }

        public DbSet<AssignedSubjects> assignedSubjects { get; set; }


        public DbSet<StaffSalary> staffSalaries { get; set; }


    }
}
