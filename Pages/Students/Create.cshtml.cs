using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoUniversity.Models;
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = new Student();

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyStudent = new Student();

            if(await TryUpdateModelAsync<Student>(
                emptyStudent,
                "student",
                s => s.FirstMidName, s=> s.LastName, s=> s.EnrollmentDate))
            {
                _context.Students.Add(emptyStudent);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Indexx");
            }

            return Page();
        }
    }
}