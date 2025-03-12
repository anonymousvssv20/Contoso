using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Pages.Students
{
    public class ChartModel : PageModel
    {
        private readonly SchoolContext _context;

        public ChartModel(SchoolContext context)
        {
            _context = context;
        }

        public List<int> EnrollmentYears { get; set; }
        public List<int> EnrollmentCounts { get; set; }

        public async Task OnGetAsync()
        {
            EnrollmentYears = await _context.Students
                .GroupBy(s => s.EnrollmentDate.Year)
                .OrderBy(g => g.Key)
                .Select(g => g.Key)
                .ToListAsync();

            EnrollmentCounts = await _context.Students
                .GroupBy(s => s.EnrollmentDate.Year)
                .OrderBy(g => g.Key)
                .Select(g => g.Count())
                .ToListAsync();
        }
    }
}
