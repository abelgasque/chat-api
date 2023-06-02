using System.ComponentModel.DataAnnotations;

namespace SecurityApp.Web.Infrastructure.Entities.DTO
{
    public class CustomerLeadDTO
    {
        public CustomerLeadDTO() { }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(250)]
        public string Mail { get; set; }
    }
}
