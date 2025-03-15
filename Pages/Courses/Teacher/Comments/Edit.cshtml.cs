using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Courses.Teacher.Comments
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comment Comment { get; set; } = default!;

        public string UserFullName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment = await _context.Comment
                .Include(c => c.Post)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommentID == id);

            if (Comment == null)
            {
                return NotFound();
            }

            // Load User's Full Name for display in the form
            UserFullName = Comment.User?.Username ?? "Unknown User";

            ViewData["PostID"] = new SelectList(_context.Post, "PostID", "Title", Comment.PostID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["PostID"] = new SelectList(_context.Post, "PostID", "Title", Comment.PostID);
                return Page();
            }

            var existingComment = await _context.Comment.FindAsync(Comment.CommentID);
            if (existingComment == null)
            {
                return NotFound();
            }

            // Preserve existing UserID and PostID
            existingComment.CommentText = Comment.CommentText;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Comment.Any(e => e.CommentID == Comment.CommentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
