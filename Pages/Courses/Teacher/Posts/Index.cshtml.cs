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

namespace ContosoUniversity.Pages.Courses.Teacher.Posts
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get; set; } = default!; // List of posts
        public int CurrentInstructorID { get; set; } // Store the logged-in instructor's ID
        public bool IsInstructor { get; set; } // Flag to check if the user is an instructor

        public async Task OnGetAsync()
        {
            // Retrieve the currently logged-in user's ID from the claims
            var instructorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IsInstructor = User.IsInRole("Instructor");

            if (int.TryParse(instructorIdString, out int instructorId))
            {
                CurrentInstructorID = instructorId; // Set the CurrentInstructorID from the logged-in user's ID
            }
            else
            {
                CurrentInstructorID = -1; // Handle the case where the ID is invalid or not set
            }

            // Retrieve posts based on user role:
            if (IsInstructor)
            {
                // If the logged-in user is an instructor, fetch only their posts
                Post = await _context.Post
                    .Where(p => p.UserID == CurrentInstructorID) // Filter posts by the instructor's ID
                    .Include(p => p.Course) // Include related Course
                    .Include(p => p.User) // Include related User (instructor)
                    .ToListAsync();
            }
            else
            {
                // If the logged-in user is a student, fetch all posts
                Post = await _context.Post
                    .Include(p => p.Course) // Include related Course
                    .Include(p => p.User) // Include related User (instructor)
                    .ToListAsync();
            }
        }
    }
}
