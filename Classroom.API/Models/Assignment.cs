using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        public int Grade { get; set; }

        public virtual Student Student { get; set; }
        public virtual Project Project { get; set; }

        public Assignment()
        {
            Grade = 0;
        }

        public Assignment(int studentId, int projectId) : this()
        {
            this.StudentId = studentId;
            this.ProjectId = projectId;
        }

        public Assignment(Student student, Project project) : this()
        {
            this.StudentId = student.StudentId;
            this.ProjectId = project.ProjectId;
        }

    }
}