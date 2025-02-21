using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Load Course Titles instead of CourseID
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");

            // Load Student Full Names instead of StudentID
            ViewData["StudentID"] = new SelectList(
                _context.Students.Select(s => new { s.ID, FullName = s.LastName + ", " + s.FirstMidName }),
                "ID",
                "FullName"
            );

            ViewData["Grade"] = new SelectList(new List<string>
                {
                  "F", "D", "C", "B", "A",
                });

            return Page();
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Ensure dropdowns are repopulated if the form submission fails
                ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");
                ViewData["StudentID"] = new SelectList(
                    _context.Students.Select(s => new { s.ID, FullName = s.LastName + ", " + s.FirstMidName }),
                    "ID",
                    "FullName"
                );

                ViewData["Grade"] = new SelectList(new List<string>
                {
                    "No Grade", "Fail", "D-", "D", "D+", "C-", "C", "C+", "B-", "B", "B+", "A-", "A", "A+", "100%"
                });

                return Page();
            }

            _context.Enrollments.Add(Enrollment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
