namespace API.DTO
{
    public class LoginDto
    {
        public string username {get;set;}
        public string password {get;set;}
    }

    public class LoginResponseDto
    {
        public string username {get;set;}
        public string token {get;set;}
    }
}