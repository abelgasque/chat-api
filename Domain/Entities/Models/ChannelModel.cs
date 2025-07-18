using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Models
{
    [Table("Channels")]
    public class ChannelModel : BaseModel
    {
        public ChannelModel() { }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Lang { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public long TenantId { get; set; }

        public TenantModel Tenant { get; set; }
    }
}