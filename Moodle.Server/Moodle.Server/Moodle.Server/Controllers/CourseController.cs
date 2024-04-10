using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Moodle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService  courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        public async Task<ActionResult<List<GetCourseDto>>> GetAllCourse() 
        {
            var result = await _courseService.GetAllCourse();
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetCourseDto>> GetSingleCourse(int id)
        {
            var result = await _courseService.GetSingleCourse(id);
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCourseDto>>> AddCourse([FromBody] AddCourseDto course)
        {
            var result = await _courseService.AddCourse(course);
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCoursesByDegree/{id}")]
        public async Task<ActionResult<List<GetCourseDto>>> GetCoursesByDegree(int id)
        {
            var result = await _courseService.GetCoursesByDegree(id);
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }


    }




}

