using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Classroom.API.Controllers;
using System.Linq;
using System.Web.Http.Results;
using Classroom.API.Models;
using System.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Classroom.API.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new StudentsController();

            var actionResult = controller.GetStudents();

            var contentResult = actionResult as OkNegotiatedContentResult<IDbSet<Student>>;

            string json = JsonConvert.SerializeObject(contentResult.Content, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            Assert.IsNotNull(json);

            Assert.IsTrue(contentResult.Content.Count() > 0);
        }
    }
}
