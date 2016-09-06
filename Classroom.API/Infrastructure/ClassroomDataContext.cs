using Classroom.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Classroom.API.Infrastructure
{
    public class ClassroomDataContext : DbContext
    {
        public ClassroomDataContext() : base("ClassroomDatabase")
        {

        }
        public IDbSet<Student> Students { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Assignments)
                .WithRequired(a => a.Project)
                .HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<Student>()
                .HasMany(p => p.Assignments)
                .WithRequired(a => a.Student)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<Assignment>()
                .HasKey(a => new { a.StudentId, a.ProjectId });
        }
    }
}