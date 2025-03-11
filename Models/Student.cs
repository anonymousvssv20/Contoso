using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Student
    {
        /*public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? Secret { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }*/

        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 charcaters.")]
        [Column("FirstName")]
        [Display(Name ="First Name")]
        public string? FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
        public string? Secret { get; set; }

        public int CourseID { get; set; }

        public Course? Course { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }

        [StringLength(300, ErrorMessage = "Information cannot be longer than 300 characters.")]
        [Display(Name = "Info")]
        public string? Info { get; set; }

        public ICollection<User> Users { get; set; }

    }
}