using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;



namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public object Student;

        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructor { get; set; } 
        public DbSet<Department> Departments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseVM> CourseVM { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course")
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorID);
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<CourseVM>().ToTable("CourseVM");
        }
        public DbSet<ContosoUniversity.Models.StudentVM> StudentVM { get; set; } = default!;
        //public DbSet<ContosoUniversity.Models.Student> Student { get; set; } = default!;
    }
}
