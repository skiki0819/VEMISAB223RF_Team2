namespace Moodle.Server.Models.Dtos
{  
        public class LoginRequestDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class LoginResponseDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }
        }
    
}
