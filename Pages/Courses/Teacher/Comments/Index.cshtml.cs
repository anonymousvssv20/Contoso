using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Security.Claims;

namespace ContosoUniversity.Pages.Courses.Teacher.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; } = default!;
        public int CurrentInstructorID { get; set; }
        public bool IsInstructor { get; set; }
        public bool IsStudent { get; set; }

        public async Task OnGetAsync()
        {
            var instructorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IsInstructor = User.IsInRole("Instructor");
            IsStudent = User.IsInRole("Student");

            if(int.TryParse(instructorIdString, out int instructorId))
            {
                CurrentInstructorID = instructorId;
            }
            else
            {
                CurrentInstructorID = -1;
            }

            if (IsInstructor)
            {
                Comment = await _context.Comment
                    .Where(c => c.UserID == CurrentInstructorID.ToString())
                    .Include(c => c.Post)
                    .Include(c => c.User)
                    .ToListAsync();
            }
            else
            {
                Comment = await _context.Comment
                    .Include(c => c.Post)
                    .Include(c => c.User)
                    .ToListAsync();
            }
        }
    }
}
