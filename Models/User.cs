using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        public string? Username { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; } // Student, Instructor

        // One-to-one relationship with Student and Instructor
        public int? StudentID { get; set; }
        public Student Student { get; set; }

        public int? InstructorID { get; set; }
        public Instructor Instructor { get; set; }
    }
}
