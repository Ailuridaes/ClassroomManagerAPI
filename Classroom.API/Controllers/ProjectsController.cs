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
using Classroom.API.DTO;
using AutoMapper;

namespace Classroom.API.Controllers
{
    public class ProjectsController : ApiController
    {
        private ClassroomDataContext db = new ClassroomDataContext();

        // GET: api/Projects
        public IEnumerable<ProjectDto.WithAssignments<AssignmentDto>> GetProjects()
        {
            return Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto.WithAssignments<AssignmentDto>>>(db.Projects);
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProjectDto.WithAssignments<AssignmentDto.WithStudent>>(project));
        }

        // GET: api/Projects/Count
        [HttpGet, Route("api/Projects/Count")]
        public IHttpActionResult GetProjectsCount()
        {
            return Ok(db.Projects.Count());
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            // db.Entry(project).State = EntityState.Modified;

            var projectToUpdate = db.Projects.Find(id);
            db.Entry(projectToUpdate).CurrentValues.SetValues(project);
            db.Entry(projectToUpdate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ProjectId == id) > 0;
        }
    }
}