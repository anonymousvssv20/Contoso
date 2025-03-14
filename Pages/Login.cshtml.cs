using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "Both fields are required.");
                return Page();
            }

            var user = _context.Users
                .Include(u => u.Student)
                .Include(u => u.Instructor)
                .FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),  // Ensure UserID is treated as an int and converted to string
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Debugging: Output the claims
            System.Diagnostics.Debug.WriteLine("Claims: " + string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}")));

            if (user.Role == "Student")
            {
                // Store the user info in session to track the logged-in student
                HttpContext.Session.SetInt32("UserId", user.UserID);  // Store the UserID as int in session
                HttpContext.Session.SetString("UserRole", user.Role);

                return RedirectToPage("/Students/GradesAndPeriods");  // Redirect to GradesAndPeriods page for students
            }
            else if (user.Role == "Instructor")
            {
                return RedirectToPage("/Instructors/Index");  // Redirect to Instructor page
            }

            ModelState.AddModelError(string.Empty, "Invalid Role.");
            return Page();
        }
    }
}
