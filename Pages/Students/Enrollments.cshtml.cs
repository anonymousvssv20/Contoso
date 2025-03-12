using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Pages.Students
{

    public class EnrollmentsModel : PageModel
    {
        private readonly SchoolContext _context;

        public EnrollmentsModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Students { get; set; } = new List<Student>();

        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if(_context.Students == null)
            {
                return NotFound();
            }

            Students = await _context.Students
                .Where(s => s.EnrollmentDate.Year == Year)
                .ToListAsync();

            return Page();
        }
    }
}