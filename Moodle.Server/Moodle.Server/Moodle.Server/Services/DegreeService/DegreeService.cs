namespace Moodle.Server.Services.DegreeService
{
    public class DegreeService : IDegreeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DegreeService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<DegreeDto>>> GetAllDegrees()
        {
            var serviceResponse = new ServiceResponse<List<DegreeDto>>();
            var dbDegrees = await _context.Degrees.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<DegreeDto>>(dbDegrees);
            return serviceResponse;
        }
    }
}
