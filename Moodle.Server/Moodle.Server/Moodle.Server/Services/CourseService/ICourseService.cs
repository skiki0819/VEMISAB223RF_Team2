﻿using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Services.CourseService
{
    public interface ICourseService
    {
        List<Course> GetAllCourse();
        Course GetSingleCourse(int id);
        List<Course> AddCourse([FromBody] Course course);
        List<Course>? UpdateCourse(int id, string code, string name, int credit);
        List<Course>? DeleteCourse(int id);
        Task<ServiceResponse<List<GetCourseDto>>> GetCoursesByUser(int id);

    }
}
