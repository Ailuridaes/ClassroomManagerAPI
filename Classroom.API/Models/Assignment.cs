using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classroom.API.Models
{
    public class Assignment
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ProjectId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Project Project { get; set; }
    }
}