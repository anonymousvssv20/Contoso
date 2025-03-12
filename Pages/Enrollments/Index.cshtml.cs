using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Pages.Enrollments
{

    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Enrollment> Enrollment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .Select(e => new Enrollment
                {
                    EnrollmentID = e.EnrollmentID,
                    Grade = e.Grade,
                    Course = new Course { Title = e.Course.Title }, // Load Course Title
                    Student = new Student { FirstMidName = e.Student.FirstMidName, LastName = e.Student.LastName } // Load Student Name
                })
                .ToListAsync();
        }
    }
}
