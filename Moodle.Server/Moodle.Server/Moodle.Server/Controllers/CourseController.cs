using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Moodle.Server.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]

    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService  courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCourse() 
        {
            var result = _courseService.GetAllCourse();
            if (result is null)
                return NotFound("No course found.");

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleCourse(int id)
        {
            var result = _courseService.GetSingleCourse(id);
            if (result is null)
                return NotFound("Course not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            var result = _courseService.AddCourse(course);
            if (result is null)
                return NotFound("Course not found.");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, string code, string name, int credit)
        {
            var result = _courseService.UpdateCourse(id, code, name, credit);
            if (result is null)
                return NotFound("Course not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = _courseService.DeleteCourse(id);
            if (result is null)
                return NotFound("Course not found.");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCoursesByUser/{id}")]
        public async Task<ActionResult<List<GetCourseDto>>> GetCoursesByUser(int id)
        {
            var result = await _courseService.GetCoursesByUser(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCoursesByDegree/{id}")]
        public async Task<ActionResult<List<GetCourseDto>>> GetCoursesByDegree(int id)
        {
            var result = await _courseService.GetCoursesByDegree(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }


    }




}

