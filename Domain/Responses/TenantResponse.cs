using ChatApi.Domain.Entities.Models;
using System;

namespace ChatApi.Domain.Responses
{
    public class TenantResponse
    {
        public TenantResponse(TenantModel pEntity)
        {
            Id = pEntity.Guid;
            Name = pEntity.Name;
            Database = pEntity.Database;
            CreatedAt = pEntity.CreatedAt;
            IsActive = !pEntity.DeletedAt.HasValue;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Database { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool IsActive { get; private set; }
    }
}
