namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        public string? Name { get; set; }
        public int? Budget { get; set; }
        public int AdministratorID { get; set; }
        public DateTime StartDate { get; set; }


        public ICollection<Instructor>? instructors { get; set; }
    }
}
