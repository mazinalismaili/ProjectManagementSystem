using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(450)]
        [Display(Name = "Project Description")]
        public string Description { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name="Priority")]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();

    }
}
