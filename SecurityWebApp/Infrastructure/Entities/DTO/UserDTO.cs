using System.ComponentModel.DataAnnotations;

namespace SecurityWebApp.Infrastructure.Entities.DTO
{
    public class UserDTO
    {
        public UserDTO() { }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
