namespace Moodle.Server.Services.DegreeService
{
    public interface IDegreeService
    {
        Task<ServiceResponse<List<DegreeDto>>> GetAllDegrees();

    }
}
