using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Security.Claims;

namespace ContosoUniversity.Pages.Courses.Teacher.Comments
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
            ViewData["PostID"] = new SelectList(_context.Post, "PostID", "Title"); // Using Title instead of Content for better display
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; } = new Comment(); // Avoid using `default!`, initialize properly

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["PostID"] = new SelectList(_context.Post, "PostID", "Title");
                return Page();
            }

            // Get the logged-in user's ID
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out int userId))
            {
                Comment.UserID = userId.ToString();
            }
            else
            {
                ModelState.AddModelError("", "Invalid user ID.");
                return Page();
            }

            _context.Comment.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
