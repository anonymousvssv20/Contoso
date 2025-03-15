using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        [StringLength(255)]
        public string? CommentText { get; set; }

        public int PostID { get; set; }
        public Post? Post { get; set; }

        public string? UserID { get; set; }
        public User? User { get; set; }
    }
}
