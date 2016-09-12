using Classroom.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classroom.API.DTO
{
    public class ProjectDto
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public class WithAssignments<TAssignment> : ProjectDto
        {
            public IEnumerable<TAssignment> Assignments { get; set; }
        }
    }
}