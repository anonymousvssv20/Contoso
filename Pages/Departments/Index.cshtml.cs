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
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Pages.Departments
{

    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<string> DepartmentNames { get; set; }
        public List<string> DepartmentStartDates { get; set; }
        public string NameSort { get; set; }
        public string BudgetSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Department> Departments { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            BudgetSort = sortOrder == "Budget" ? "budget_desc" : "Budget";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Department> departmentsIQ = from d in _context.Departments
                                                   select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                departmentsIQ = departmentsIQ.Where(d => d.Name.Contains(searchString)
                    || d.Budget.ToString().Contains(searchString)
                    || d.StartDate.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    departmentsIQ = departmentsIQ.OrderByDescending(d => d.Name);
                    break;
                case "Budget":
                    departmentsIQ = departmentsIQ.OrderBy(d => d.Budget);
                    break;
                case "budget_desc":
                    departmentsIQ = departmentsIQ.OrderByDescending(d => d.Budget);
                    break;
                case "Date":
                    departmentsIQ = departmentsIQ.OrderBy(d => d.StartDate);
                    break;
                case "date_desc":
                    departmentsIQ = departmentsIQ.OrderByDescending(d => d.StartDate);
                    break;
                default:
                    departmentsIQ = departmentsIQ.OrderBy(d => d.Name);
                    break;
            }

            int pageSize = _configuration.GetValue<int>("PageSize", 10);
            Departments = await PaginatedList<Department>.CreateAsync(departmentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            DepartmentNames = await _context.Departments
                .OrderBy(d => d.Name)
                .Select(d => d.Name)
                .ToListAsync();

            DepartmentStartDates = await _context.Departments
        .OrderBy(d => d.Name)
        .Select(d => d.StartDate.ToString("yyyy-MM-dd")) // Format start date
        .ToListAsync();
        }
    }
}