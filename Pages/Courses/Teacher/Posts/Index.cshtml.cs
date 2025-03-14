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

        public async Task OnGetAsync()
        {
            // Retrieve the currently logged-in user's ID from the claims
            var instructorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(instructorIdString, out int instructorId))
            {
                CurrentInstructorID = instructorId; // Set the CurrentInstructorID from the logged-in user's ID
            }
            else
            {
                CurrentInstructorID = -1; // Handle the case where the ID is invalid or not set
            }

            Console.WriteLine($"Current Instructor ID: {CurrentInstructorID}");

            // Retrieve all posts from the database
            Post = await _context.Post
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .ToListAsync();

            Console.WriteLine($"Total Posts Retrieved: {Post.Count}");
        }
    }
}
