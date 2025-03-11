using static ContosoUniversity.Pages.Students.GradesAndPeriodsModel;
using ContosoUniversity.Models;

namespace ContosoUniversity.Models
{
    public class StudentGradesViewModel
    {
        public List<TimetableEntry>? Timetable { get; set; }
        public List<Enrollment>? Enrollments { get; set; }

    }

    public class TimeTableEntry
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTiem { get; set; }
    }
}
