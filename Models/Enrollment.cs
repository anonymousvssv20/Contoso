using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        [Display(Name = "Course")]
        public int CourseID { get; set; }

        [Display(Name = "Student")]
        public int StudentID { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Course? Course { get; set; }
        public Student? Student { get; set; }

        [StringLength(300, ErrorMessage = "Information cannot be longer than 300 characters.")]
        [Display(Name = "Info")]
        public string? Info { get; set; }
    }
}
