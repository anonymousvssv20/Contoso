using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstMidName { get; set; }
        public DateTime HireDate { get; set; }

        // Removed the ICollection<Instructor> - potential infinite loop
        // If you need to represent the instructors related to this instructor,
        // you would likely do it through a different entity (e.g., DepartmentAssignment)
        public DbSet<Enrollment>? Enrollments { get; set; }
    }
}