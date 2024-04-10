using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Moodle.Server.Models.Dtos;

namespace Moodle.Server.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCourseDto>>> GetAllCourse()
         {
            var response = new ServiceResponse<List<GetCourseDto>>();
            var dbCourses = await _context.Courses.ToListAsync();
            response.Data = dbCourses.Select(c => _mapper.Map<GetCourseDto>(c)).ToList();
            if (response.Data.Count == 0)
            {
                response.Message = ResponseMessages.EmptyCourseList;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetCourseDto>> GetSingleCourse(int id)
        {
            var response = new ServiceResponse<GetCourseDto>();
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course is null)
            {
                response.Message = ResponseMessages.NoCourseFoundForDegree;
                response.Success = false;
            }
            response.Success = true;
            response.Data = _mapper.Map<GetCourseDto>(course);
            return response;
        }

        public async Task<ServiceResponse<List<GetCourseDto>>> AddCourse([FromBody] AddCourseDto course)
        {
            var response = new ServiceResponse<List<GetCourseDto>>();
            var newCourse = new Course
            {
                Code = course.Code,
                Name = course.Name,
                Credit = course.Credit,
            };
            await _context.Courses.AddAsync(newCourse);
            await _context.SaveChangesAsync();

            var dbCourses = await _context.Courses.ToListAsync();
            response.Data = dbCourses.Select(c => _mapper.Map<GetCourseDto>(c)).ToList();
            if (response.Data.Count == 0)
            {
                response.Message = ResponseMessages.NoCourseFoundForDegree;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCourseDto>>> GetCoursesByDegree(int id)
        {
            var response = new ServiceResponse<List<GetCourseDto>>();
            var dbCourses = await _context.Degrees
                .Where(d => d.Id == id)
                .SelectMany(d => d.Courses)
                .ToListAsync();
            response.Data = dbCourses.Select(c => _mapper.Map<GetCourseDto>(c)).ToList();
            if (response.Data.Count == 0)
            {
                response.Message = ResponseMessages.NoCourseFoundForDegree;
                response.Success = false;
            }
            return response;
        }
    }
}