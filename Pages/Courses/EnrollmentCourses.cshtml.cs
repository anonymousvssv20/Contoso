using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContosoUniversity.Pages.Courses
{
    public class EnrollmentCoursesModel : PageModel
    {
        private readonly SchoolContext _context;

        public EnrollmentCoursesModel(SchoolContext context)
        {
            _context = context;
        }

        // The property to hold the course details
        public Course Course { get; set; }

        // The OnGet method accepts the CourseID from the URL and fetches the course details
        public IActionResult OnGet(int courseId)
        {
            // Get the course based on CourseID including its Instructor, Department, and Enrollments
            Course = _context.Courses
                             .Include(c => c.Instructor)       // Include the instructor details
                             .Include(c => c.Department)       // Include the department details
                             .Include(c => c.Enrollments)      // Include the enrollments
                                 .ThenInclude(e => e.Student)  // Include the student details in enrollments
                             .FirstOrDefault(c => c.CourseID == courseId);

            // If no course is found, return a NotFound page
            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
