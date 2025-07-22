using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Models
{
    [Table("Tenants")]
    public class TenantModel : BaseModel
    {
        public string Domain { get; set; } = null;

        [Required]
        [MaxLength(255)]
        public string Database { get; set; }
    }
}