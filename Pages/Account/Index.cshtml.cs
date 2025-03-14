using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public Student? Student { get; set; }
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>(); // To store the student's enrollments

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"Logged-in UserId: {userId ?? "NULL"}");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            // Fetch the logged-in student from the database based on the UserId
            Student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == int.Parse(userId));

            if (Student == null)
            {
                Console.WriteLine("❌ No matching student found in the database.");
                return RedirectToPage("/Account/Login");
            }
            else
            {
                Console.WriteLine($"✅ Student Loaded: {Student.FullName}");
            }

            // Now fetch the enrollments for the logged-in student
            Enrollments = await _context.Enrollments
                .Where(e => e.StudentID == Student.ID)
                .Include(e => e.Course) // Include course info
                .ToListAsync();

            return Page();
        }
    }
}
