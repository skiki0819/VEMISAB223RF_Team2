using AutoMapper;
using Moodle.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moodle.Server.Services;
using Microsoft.AspNetCore.Cors;
using System.Xml.Linq;

namespace Moodle.Server.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]

    public class CourseController : ControllerBase
    {
        private static List<Course> courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Code = "VEMILEKVAR",
                    Name = "Lekvár",
                    Credit = 8,
                },
                new Course
                {
                    Id = 2,
                    Code = "VEMIGEM",
                    Name = "GEM",
                    Credit = 3,
                }
            };



        [HttpGet]
        public async Task<IActionResult> GetAllCourse() => Ok(courses);


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleCourse(int id)
        {
            var course = courses.Find(x => x.Id == id);
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            courses.Add(course);
            return Ok(courses);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course updateCourse)
        {
            var course = courses.Find(x => x.Id == id);
            if (updateCourse is null)
                return NotFound("The course doesn't exist.");

            course.Id = updateCourse.Id;
            course.Code = updateCourse.Code;
            course.Credit = updateCourse.Credit;
            course.Name = updateCourse.Name;

            return Ok(course);
        
        }
    }




}






