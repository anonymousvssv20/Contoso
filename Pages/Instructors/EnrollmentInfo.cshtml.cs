using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoUniversity.Pages.Instructors
{

    public class EnrollmentInfoModel : PageModel
    {
        private readonly SchoolContext _context;

        public EnrollmentInfoModel(SchoolContext context)
        {
            _context = context;
        }

        // Property to hold the list of instructors hired in the given year
        public List<Instructor> Instructors { get; set; }

        // The OnGet method accepts the teaching year from the URL
        public IActionResult OnGet(int teachingYear)
        {
            // Fetch all instructors hired in the given teaching year
            Instructors = _context.Instructor
                .Where(i => i.HireDate.Year == teachingYear)  // Filter by year of HireDate
                .ToList();

            // If no instructors found, return a NotFound result
            if (Instructors.Count == 0)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
