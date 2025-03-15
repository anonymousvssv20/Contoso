using System;
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
            ViewData["UserId"] = instructorIdString;

            // ViewData for the CourseID SelectList, displaying the Course Title
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");

            // Fetch users with the "Instructor" role and pass them to the ViewData
            var instructors = _context.Users
                .Where(u => u.Role == "Instructor") // Filter by Role == "Instructor"
                .Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.Username // Assuming 'Username' or 'FullName' for the display name
                }).ToList();

            ViewData["InstructorID"] = new SelectList(instructors, "Value", "Text");

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
                Post.UserID = instructorId;
            }

            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
