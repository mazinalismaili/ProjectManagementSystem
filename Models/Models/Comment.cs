using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int TodoId { get; set; }
        public Todo Todo { get; set; }


    }
}
