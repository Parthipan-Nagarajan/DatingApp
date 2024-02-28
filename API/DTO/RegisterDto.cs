using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDto
    {
        public RegisterDto(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        [Required]
        public string username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string password { get; set; }
    }
}