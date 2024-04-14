using Microsoft.AspNetCore.Mvc;
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
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
