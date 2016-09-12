using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classroom.API.DTO
{
    public class AssignmentDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int Grade { get; set; }

        public class WithStudent : AssignmentDto
        {
            public StudentDto Student { get; set; }
        }

        public class WithProject : AssignmentDto
        {
            public ProjectDto Project { get; set; }
        }

        public class WithBoth : AssignmentDto
        {
            public StudentDto Student { get; set; }
            public ProjectDto Project { get; set; }
        }
    }
}