﻿using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("AddCourseToUser")]
        public async Task<ActionResult<List<GetCourseDto>>> AddCourseToUser(AddCourseToUserDto requestObject)
        {
            var result = await _userService.AddCourseToUser(requestObject);

            return Ok(result);
        }


        [HttpGet]
        [Route("GetCoursesByUser/{id}")]
        public async Task<ActionResult<List<GetCourseDto>>> GetCoursesByUser(int id)
        {
            var result = await _userService.GetCoursesByUser(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetUsersByCourseId/{courseId}")]
        public async Task<IActionResult> GetUsersByCourseId(int courseId)
        {
            var response = await _userService.GetUsersByCourseId(courseId);

            if (response.Success)
                return Ok(response.Data);

            return NotFound(response);
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var response = await _userService.Login(username, password);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
