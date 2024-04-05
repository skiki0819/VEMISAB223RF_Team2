using Moodle.Server.Models.Entities;
using Moodle.Server.Models.Dtos;

namespace Moodle.Server.Services.DegreeService
{
    public interface IDegreeService
    {
        Task<ServiceResponse<List<DegreeDto>>> GetAllDegrees();

    }
}
