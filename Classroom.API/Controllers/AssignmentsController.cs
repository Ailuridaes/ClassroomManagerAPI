using System;
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

namespace Classroom.API.Controllers
{
    public class AssignmentsController : ApiController
    {
        private ClassroomDataContext db = new ClassroomDataContext();

        // GET: api/Assignments
        //public IQueryable<Assignment> GetAssignments()
        //{
        //    return db.Assignments;
        //}

        // GET: api/Assignments/5
        //[ResponseType(typeof(Assignment))]
        //public IHttpActionResult GetAssignment(int id)
        //{
        //    Assignment assignment = db.Assignments.Find(id);
        //    if (assignment == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(assignment);
        //}

        // GET: api/Assignments/Count
        [HttpGet, Route("api/Assignments/Count")]
        public IHttpActionResult GetAssignmentsCount()
        {
            return Ok(db.Assignments.Count());
        }

        // PUT: api/Assignments/
        [HttpPut, Route("api/Assignments"), ResponseType(typeof(void))]
        public IHttpActionResult PutAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // db.Entry(assignment).State = EntityState.Modified;

            var assignmentToUpdate = db.Assignments.Find(assignment.StudentId, assignment.ProjectId);

            if (assignmentToUpdate == null)
            {
                return NotFound();
            }

            db.Entry(assignmentToUpdate).CurrentValues.SetValues(assignment);
            db.Entry(assignmentToUpdate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Assignments
        [HttpPost, ResponseType(typeof(Assignment))]
        public IHttpActionResult PostAssignment([FromBody]Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assignments.Add(assignment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AssignmentExists(assignment.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = assignment.StudentId }, assignment);
        }

        // DELETE: api/Assignments
        [HttpDelete, Route("api/Assignments"), ResponseType(typeof(Assignment))]
        public IHttpActionResult DeleteAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Assignment assignmentToDelete = db.Assignments.Find(assignment.StudentId, assignment.ProjectId);

            if (assignmentToDelete == null)
            {
                return NotFound();
            }

            db.Assignments.Remove(assignmentToDelete);
            db.SaveChanges();

            return Ok(assignmentToDelete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssignmentExists(int id)
        {
            return db.Assignments.Count(e => e.StudentId == id) > 0;
        }
    }
}