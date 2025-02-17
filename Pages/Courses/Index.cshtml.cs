using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Helpers;

namespace ContosoUniversity.Pages.Courses
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

        public string TitleSort { get; set; }
        public string CreditsSort { get; set; }
        public string CurrentTitleFilter { get; set; }
        public string CurrentCreditsFilter { get; set; }
        public string CurrentSort { get; set; }


        public PaginatedList<Course> Courses { get; set; }

        public bool PrevDisabled { get; set; }
        public bool NextDisabled { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentTitleFilter, string currentCreditsFilter, string searchTitle, int? searchCredits, int? pageIndex)
        {
            CurrentSort = sortOrder;
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            CreditsSort = sortOrder == "Credits" ? "credits_desc" : "Credits";

            if (searchTitle != null || searchCredits != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchTitle = currentTitleFilter;
                searchCredits = currentCreditsFilter != null ? int.Parse(currentCreditsFilter) : (int?)null;
            }

            CurrentTitleFilter = searchTitle;
            CurrentCreditsFilter = searchCredits?.ToString();

            IQueryable<Course> coursesIQ = _context.Courses.AsQueryable();

            if (!String.IsNullOrEmpty(searchTitle))
            {
                coursesIQ = coursesIQ.Where(c => c.Title.Contains(searchTitle));
            }

            if (searchCredits.HasValue)
            {
                coursesIQ = coursesIQ.Where(c => c.Credits == searchCredits);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    coursesIQ = coursesIQ.OrderByDescending(c => c.Title);
                    break;
                case "Credits":
                    coursesIQ = coursesIQ.OrderBy(c => c.Credits);
                    break;
                case "credits_desc":
                    coursesIQ = coursesIQ.OrderByDescending(c => c.Credits);
                    break;
                default:
                    coursesIQ = coursesIQ.OrderBy(c => c.Title);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Courses = await PaginatedList<Course>.CreateAsync(
                coursesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            // Determine if Previous and Next buttons should be disabled
            PrevDisabled = !Courses.HasPreviousPage;
            NextDisabled = !Courses.HasNextPage;
        }
    }
}
