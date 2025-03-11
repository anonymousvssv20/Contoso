using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Linq;

namespace ContosoUniversity.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SchoolContext _context;

        public LoginModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "Both fields are required.");
                return Page();
            }

            // Check if user exists in the database
            var user = _context.Users
                .Include(u => u.Student)
                .Include(u => u.Instructor)
                .FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            // Debugging log to check if the user has a role and student/instructor relationships
            System.Diagnostics.Debug.WriteLine($"User found: {user.Username}, Role: {user.Role}");

            // Store user information in the session
            HttpContext.Session.SetInt32("UserId", user.UserID);
            HttpContext.Session.SetString("UserRole", user.Role);

            if (user.Role == "Student")
            {
                if (user.Student != null)
                {
                    // Log student ID for debugging
                    System.Diagnostics.Debug.WriteLine($"Student ID: {user.Student.ID}");
                    return RedirectToPage("/Students/GradesAndPeriods");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Student record not found.");
                    return Page();
                }
            }
            else if (user.Role == "Instructor")
            {
                return RedirectToPage("/Instructors/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid role.");
                return Page();
            }
        }

    }
}
