using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        [Display(Name = "Task Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(450)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Task Starting Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Task Ending Date")]
        public DateTime EndDate { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
            
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public List<Comment>? Comments { get; set; } = new List<Comment>();

    }
}

