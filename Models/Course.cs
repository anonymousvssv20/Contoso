﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {
        /*[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string? Title { get; set; }
        public int? Credits { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }*/

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Title { get; set; }

        [Range(0, 10)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public int InstructorID { get; set; }

        public int StudentID { get; set; }
        public Department? Department { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Instructor>? Instructors { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}