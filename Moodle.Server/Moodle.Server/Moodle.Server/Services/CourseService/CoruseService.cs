﻿using Microsoft.AspNetCore.Mvc;

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

        private static List<Course> courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Code = "VEMILEKVAR",
                    Name = "Lekvár",
                    Credit = 8,
                },
                new Course
                {
                    Id = 2,
                    Code = "VEMIGEM",
                    Name = "GEM",
                    Credit = 3,
                }
            };

        public List<Course> GetAllCourse()
        {
            return courses;
        }

        public Course GetSingleCourse(int id)
        {
            var course = courses.Find(x => x.Id == id);
            return course;
        }

        public List<Course> AddCourse([FromBody] Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            course.Events = new List<Event>();
            course.Degrees = new List<Degree>();
            course.Users = new List<User>();
            courses.Add(course);
            return courses;
        }

        public List<Course>? UpdateCourse(int id, string code, string name, int credit)
        {
            var course = courses.Find(x => x.Id == id);
            if (course is null)
                return null;

            course.Code = code;
            course.Name = name;
            course.Credit = credit;

            return courses;
        }

        public List<Course>? DeleteCourse(int id)
        {
            var course = courses.Find(x => x.Id == id);
            if (course is null)
                return null;

            courses.Remove(course);
            return courses;
        }

        public async Task<ServiceResponse<List<GetCourseDto>>> GetCoursesByUser(int id)
        {
            var response = new ServiceResponse<List<GetCourseDto>>();
            var dbCourses = await _context.Users
                .Where(u => u.Id == id)
                .SelectMany(u => u.Courses)
                .ToListAsync();
            response.Data = dbCourses.Select(c => _mapper.Map<GetCourseDto>(c)).ToList();
            if (response.Data.Count == 0)
            {
                response.Message = ResponseMessages.NoCourseFoundForUser;
                response.Success = false;
            }
            return response;
        }
    }
}