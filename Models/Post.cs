using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [Required]
        [StringLength(255)]
        public string? Title {get; set; }

        [Required]
        public string? Content { get; set; }

        public int CourseID { get; set; }
        public Course? Course { get; set; }

        public int InstructorID { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
