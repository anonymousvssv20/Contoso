using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.OfficeAsignment
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<OfficeAssignment> OfficeAssignment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            OfficeAssignment = await _context.OfficeAssignments
                .Include(o => o.Instructor).ToListAsync();
        }
    }
}
