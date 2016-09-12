﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Classroom.API.Infrastructure;
using Classroom.API.Models;
using Classroom.API.DTO;
using AutoMapper;

namespace Classroom.API.Controllers
{
    public class StudentsController : ApiController
    {
        private ClassroomDataContext db = new ClassroomDataContext();

        // GET: api/Students
        public IHttpActionResult GetStudents()
        {
            //return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto.WithAssignments<AssignmentDto.WithProject>>>(db.Students);
            return Ok(db.Students.Select(s => new
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                Assignments = s.Assignments.Select(a => new
                {
                   ProjectName = a.Project.Name 
                })
            }));
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // GET: api/Students/Count
        [HttpGet, Route("api/Students/Count")]
        public IHttpActionResult GetStudentsCount()
        {
            return Ok(db.Students.Count());
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentId)
            {
                return BadRequest();
            }

            //db.Entry(student).State = EntityState.Modified;

            var studentToUpdate = db.Students.Find(id);
            db.Entry(studentToUpdate).CurrentValues.SetValues(student);
            db.Entry(studentToUpdate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.StudentId == id) > 0;
        }
    }
}