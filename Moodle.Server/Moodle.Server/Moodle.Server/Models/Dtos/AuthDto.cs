namespace Moodle.Server.Models.Dtos
{  
        public class LoginRequestDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class LoginResponseDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }
            public string Token { get; set; }

        }

        public class RegisterRequestDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public int DegreeId { get; set; }
            public int RoleId { get; set; }
        }
        public class RegisterResponseDto
        {
            public string Username { get; set; }
        }
    
}
