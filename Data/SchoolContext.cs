﻿using System;
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
        public DbSet<User> Users { get; set; }
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

            // Define the User entity
            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)  // A user has one student
                .WithMany(s => s.Users)  // A student can have multiple users (for different roles)
                .HasForeignKey(u => u.StudentID);  // Foreign key in User table

            modelBuilder.Entity<User>()
                .HasOne(u => u.Instructor)  // A user has one instructor
                .WithMany(i => i.Users)
                .HasForeignKey(u => u.InstructorID);  // Foreign key in User table

            modelBuilder.Entity<Post>();
            modelBuilder.Entity<Comment>();
        }
        public DbSet<ContosoUniversity.Models.StudentVM> StudentVM { get; set; } = default!;
        public DbSet<ContosoUniversity.Models.Post> Post { get; set; } = default!;
        public DbSet<ContosoUniversity.Models.Comment> Comment { get; set; } = default!;
        //public DbSet<ContosoUniversity.Models.Student> Student { get; set; } = default!;
    }
}
