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
            try {
                var dbDegrees = await _context.Degrees.ToListAsync();
                if (dbDegrees == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ResponseMessages.NoDegreesFound;
                    return serviceResponse;
                }
                serviceResponse.Data = _mapper.Map<List<DegreeDto>>(dbDegrees);   
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
