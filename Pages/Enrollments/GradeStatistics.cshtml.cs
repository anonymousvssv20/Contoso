using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Enrollments
{
    public class GradeStatisticsModel : PageModel
    {
        private readonly SchoolContext _context;

        public GradeStatisticsModel(SchoolContext context)
        {
            _context = context;
        }

        public List<CourseGradeStats> CourseStats { get; set; } = new();

        public async Task OnGetAsync()
        {
            CourseStats = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student) // Include students for grade lookup
                .Select(c => new CourseGradeStats
                {
                    CourseTitle = c.Title,
                    InstructorName = c.Instructor != null ? c.Instructor.FullName : "N/A",
                    HighestGrade = c.Enrollments.Any(e => e.Grade.HasValue)
                        ? ConvertToLetterGrade(c.Enrollments.Where(e => e.Grade.HasValue).Max(e => (int)e.Grade))
                        : "N/A",
                    LowestGrade = c.Enrollments.Any(e => e.Grade.HasValue)
                        ? ConvertToLetterGrade(c.Enrollments.Where(e => e.Grade.HasValue).Min(e => (int)e.Grade))
                        : "N/A",
                    HighestGradeStudent = c.Enrollments
                        .Where(e => e.Grade.HasValue)
                        .OrderByDescending(e => e.Grade) // Get the student with the highest grade
                        .Select(e => e.Student != null ? e.Student.FullName : "N/A")
                        .FirstOrDefault(),
                    LowestGradeStudent = c.Enrollments
                        .Where(e => e.Grade.HasValue)
                        .OrderBy(e => e.Grade) // Get the student with the lowest grade
                        .Select(e => e.Student != null ? e.Student.FullName : "N/A")
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        private static string ConvertToLetterGrade(int grade)
        {
            return grade switch
            {
                0 => "A", // 0 = F
                1 => "B", // 1 = D
                2 => "C", // 2 = C
                3 => "D", // 3 = B
                4 => "F", // 4 = A
                _ => "N/A" // Handles unexpected values
            };
        }

    }

    public class CourseGradeStats
    {
        public string? CourseTitle { get; set; }
        public string? InstructorName { get; set; }
        public string HighestGrade { get; set; } = "N/A";
        public string LowestGrade { get; set; } = "N/A";
        public string HighestGradeStudent { get; set; } = "N/A";
        public string LowestGradeStudent { get; set; } = "N/A";
    }
}
