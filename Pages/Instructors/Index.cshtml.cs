using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using ContosoUniversity.Helpers;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Instructor> Instructors { get; set; }

        public InstructorIndexData InstructorData { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int? id, int? courseID)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if(searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            currentFilter = searchString;

            IQueryable<Instructor> instructorIQ = from i in _context.Instructor select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                instructorIQ = instructorIQ.Where(i => i.LastName.Contains(searchString)
                    || i.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    instructorIQ = instructorIQ.OrderByDescending(i => i.LastName);
                    break;
                case "Date":
                    instructorIQ = instructorIQ.OrderBy(i => i.HireDate);
                    break;
                case "date_desc":
                    instructorIQ = instructorIQ.OrderByDescending(i => i.HireDate);
                    break;
                default:
                    instructorIQ = instructorIQ.OrderBy(i => i.LastName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Instructors = await PaginatedList<Instructor>.CreateAsync(
                instructorIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            InstructorData = new InstructorIndexData();
            InstructorData.Instructors = await _context.Instructor
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .ThenInclude(c => c.Department)
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if(id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.ID == id.Value).Single();
                InstructorData.Course = instructor.Courses;
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                IEnumerable<Enrollment> enrollments = await _context.Enrollments
                    .Where(x => x.CourseID == CourseID)
                    .Include(i => i.Student)
                    .ToListAsync();
                InstructorData.Enrollments = enrollments;
            }
        }
    }
}