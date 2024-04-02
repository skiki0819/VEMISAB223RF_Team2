using AutoMapper;
using Moodle.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moodle.Server.Services;
using Microsoft.AspNetCore.Cors;

namespace Moodle.Server.Controllers
{
    [Route("../Models/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet("courses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<Course>))]
        public async Task<IActionResult> GetCourses(int pageNumber, int pageSize)
        {
            PagedResult.CheckParameters(ref pageNumber, ref pageSize);
            var courses = await _courseService.GetCourses(pageNumber, pageSize);
            var coursesDtos = new PagedResult<Course>
            {
                CurrentPage = courses.CurrentPage,
                PageSize = courses.PageSize,
                TotalItems = courses.TotalItems,
                TotalPages = courses.TotalPages,
                Items = _mapper.Map<List<Course>>(courses.Items)
            };
            return Ok(coursesDtos);
        }
    }
}
