using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classroom.API.DTO
{
    public class StudentDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Telephone { get; set; }

        public class WithAssignments<TAssignment> : StudentDto
        {
            public IEnumerable<TAssignment> Assignments { get; set; }
        }
    }
}