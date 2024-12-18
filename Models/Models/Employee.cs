using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models.Models
{
    public class Employee : IdentityUser
    {
        [StringLength(450)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(450)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public bool isManager { get; set; } = false;

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Todo> Todos { get; set; } = new List<Todo>();

    }
}
