using Microsoft.AspNetCore.Mvc;
using Moodle.Server.Models.Dtos;
using Moodle.Server.Services.DegreeService;

namespace Moodle.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DegreeController : ControllerBase
    {
        private readonly IDegreeService _degreeService;

        public DegreeController(IDegreeService degreeService)
        {
            _degreeService = degreeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<DegreeDto>>> Get()
        {
            var result = await _degreeService.GetAllDegrees();
            if (result is null)
                return NotFound("No degree found.");

            return Ok(result);
        }
    }
}
