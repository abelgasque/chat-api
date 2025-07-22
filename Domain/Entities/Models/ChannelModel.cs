using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Models
{
    [Table("Channels")]
    public class ChannelModel : BaseModel
    {
        public ChannelModel() { }

        [Required]
        public int Type { get; set; } = 1;

        public string Lang { get; set; } = null;

        public string Url { get; set; } = null;

        [Required]
        public long TenantId { get; set; }

        public TenantModel Tenant { get; set; }
    }
}