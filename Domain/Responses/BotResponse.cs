using System;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Domain.Responses
{
    public class BotResponse
    {
        public BotResponse(BotModel pEntity)
        {
            Id = pEntity.Id;
            Name = pEntity.Name;
            CreatedAt = pEntity.CreatedAt;
            IsActive = pEntity.DeletedAt.HasValue ? false : true;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
