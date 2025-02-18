using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        /*public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstMidName { get; set; }
        public DateTime HireDate { get; set; }

        // Removed the ICollection<Instructor> - potential infinite loop
        // If you need to represent the instructors related to this instructor,
        // you would likely do it through a different entity (e.g., DepartmentAssignment)
        public DbSet<Enrollment>? Enrollments { get; set; }*/

        public int ID { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string? FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public ICollection<Course> Courses { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }
    }
}