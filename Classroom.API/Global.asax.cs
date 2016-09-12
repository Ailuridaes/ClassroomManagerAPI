using AutoMapper;
using Classroom.API.DTO;
using Classroom.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Classroom.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Mapper.CreateMap<Assignment, AssignmentDto>();
            Mapper.CreateMap<Assignment, AssignmentDto.WithBoth>();
            Mapper.CreateMap<Assignment, AssignmentDto.WithProject>();
            Mapper.CreateMap<Assignment, AssignmentDto.WithStudent>();
            Mapper.CreateMap<Project, ProjectDto.WithAssignments<AssignmentDto>>();
            Mapper.CreateMap<Project, ProjectDto.WithAssignments<AssignmentDto.WithBoth>>();
            Mapper.CreateMap<Project, ProjectDto.WithAssignments<AssignmentDto.WithProject>>();
            Mapper.CreateMap<Project, ProjectDto.WithAssignments<AssignmentDto.WithStudent>>();
            Mapper.CreateMap<Project, ProjectDto>();
            Mapper.CreateMap<Student, StudentDto.WithAssignments<AssignmentDto>>();
            Mapper.CreateMap<Student, StudentDto.WithAssignments<AssignmentDto.WithBoth>>();
            Mapper.CreateMap<Student, StudentDto.WithAssignments<AssignmentDto.WithStudent>>();
            Mapper.CreateMap<Student, StudentDto.WithAssignments<AssignmentDto.WithProject>>();
            Mapper.CreateMap<Student, StudentDto>();

        }
    }
}
