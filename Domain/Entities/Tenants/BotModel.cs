using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("Bots")]
    public class BotModel : BaseModel
    {
        public BotModel() { }

        [Required]
        [MaxLength(550)]
        public string Code { get; set; }
    }
}