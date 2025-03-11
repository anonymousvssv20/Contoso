﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        [StringLength(50, MinimumLength =3)]
        public string? Name { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName ="money")]
        public int? Budget { get; set; }
        public int AdministratorID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public ICollection<Instructor>? instructors { get; set; }

        [StringLength(300, ErrorMessage = "Information cannot be longer than 300 characters.")]
        [Display(Name = "Info")]
        public string? Info { get; set; }
    }
}
