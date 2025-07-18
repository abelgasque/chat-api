using System.ComponentModel.DataAnnotations;

namespace ChatApi.Domain.Entities.DTO
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
