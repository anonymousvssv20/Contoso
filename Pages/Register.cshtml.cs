using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly SchoolContext _context;

        public RegisterModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string Role { get; set; }

        [BindProperty]
        public string FirstMidName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string StudentEnrollmentDate { get; set; }

        [BindProperty]
        public string InstructorHireDate { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if passwords match
            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return Page();
            }

            // Check if username already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == Username);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Username already exists.");
                return Page();
            }

            // Create the user
            var user = new User
            {
                Username = Username,
                Password = Password, // You should hash the password in production
                Role = Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Now, create the specific Student or Instructor entry
            if (Role == "Student")
            {
                if (!DateTime.TryParse(StudentEnrollmentDate, out DateTime enrollmentDate))
                {
                    ModelState.AddModelError(string.Empty, "Invalid enrollment date.");
                    return Page();
                }

                var student = new Student
                {
                    FirstMidName = FirstMidName,
                    LastName = LastName,
                    EnrollmentDate = enrollmentDate,
                    // Associating the user with the student (users collection in the student)
                    Users = new List<User> { user }
                };

                _context.Students.Add(student);
            }
            else if (Role == "Instructor")
            {
                if (!DateTime.TryParse(InstructorHireDate, out DateTime hireDate))
                {
                    ModelState.AddModelError(string.Empty, "Invalid hire date.");
                    return Page();
                }

                var instructor = new Instructor
                {
                    FirstMidName = FirstMidName,
                    LastName = LastName,
                    HireDate = hireDate,
                    // Associating the user with the instructor (users collection in the instructor)
                    Users = new List<User> { user }
                };

                _context.Instructor.Add(instructor);
            }

            await _context.SaveChangesAsync();

            // Redirect to login page after successful registration
            return RedirectToPage("/Login");
        }
    }
}
