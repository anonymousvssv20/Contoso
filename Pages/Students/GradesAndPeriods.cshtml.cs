using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Helpers;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Pages.Students
{
    public class GradesAndPeriodsModel : PageModel
    {
        private readonly SchoolContext _context;

        private readonly TimeSpan startTime = new TimeSpan(8, 30, 0);
        private readonly TimeSpan endTime = new TimeSpan(16, 35, 0);
        private readonly TimeSpan startOnlineTime = new TimeSpan(16, 30, 0);
        private readonly TimeSpan endOnlineTime = new TimeSpan(20, 0, 0);
        private readonly int periodLength = 45; // Minutes

        public StudentGradesViewModel Data { get; set; }

        public GradesAndPeriodsModel(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get userId and userRole from session
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? userRole = HttpContext.Session.GetString("UserRole");

            if (userId == null || userRole != "Student")
            {
                return Unauthorized(); // If user is not logged in or role is incorrect
            }

            // Fetch the user and their associated student ID
            var user = await _context.Users
                .Include(u => u.Student)
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user?.Student == null)
            {
                return Unauthorized(); // If student record is missing
            }

            int studentId = user.StudentID.Value;

            // Fetch student's enrollments
            var enrollments = await _context.Enrollments
                .Where(e => e.StudentID == studentId)
                .Include(e => e.Course)
                .ToListAsync();

            Data = new StudentGradesViewModel
            {
                Username = User.Identity.Name,
                Timetable = GenerateTimeTable(),
                Enrollments = enrollments
            };

            return Page();
        }

        public List<TimetableEntry> GenerateTimeTable()
        {
            List<TimetableEntry> timetable = new List<TimetableEntry>();
            DateTime currentTime = DateTime.Today.Add(startTime);

            while (currentTime.TimeOfDay < endTime)
            {
                timetable.Add(new TimetableEntry
                {
                    StartTime = currentTime,
                    EndTime = currentTime.AddMinutes(periodLength)
                });
                currentTime = currentTime.AddMinutes(periodLength);
            }

            return timetable;
        }

        public class TimetableEntry
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }

        public class StudentGradesViewModel
        {
            public List<TimetableEntry>? Timetable { get; set; }
            public List<Enrollment>? Enrollments { get; set; }
            public string? Username { get; internal set; }
        }
    }
}