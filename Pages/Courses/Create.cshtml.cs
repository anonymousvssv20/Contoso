using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Reflection.Metadata.Ecma335;

namespace ContosoUniversity.Pages.Courses
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
            Course = new Course();
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
                return Page();
            }

            try
            {
                _context.Courses.Add(Course);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving data: {ex.Message}");
                ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
                return Page();
            }
        }

    }
}
