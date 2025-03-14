using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Security.Claims;

namespace ContosoUniversity.Pages.Courses.Teacher.Posts
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
            // Retrieve the logged-in user's ID (InstructorID)
            var instructorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Optionally, set the ID as ViewData to be displayed in the view
            ViewData["UserId"] = instructorIdString;

            // ViewData for the CourseID SelectList
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID");

            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var instructorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(instructorIdString, out int instructorId))
            {
                Post.InstructorID = instructorId;
            }

            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}
