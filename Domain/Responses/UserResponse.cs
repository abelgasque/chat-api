using ChatApi.Domain.Entities.Models;
using System;

namespace ChatApi.Domain.Responses
{
    public class UserResponse
    {
        public UserResponse(UserModel pEntity)
        {
            Id = pEntity.Guid;
            Username = pEntity.Name;
            Email = pEntity.Email;
            CreatedAt = pEntity.CreatedAt;
            IsActive = !pEntity.DeletedAt.HasValue;
            IsBlock = pEntity.BlockedAt.HasValue;
        }

        public Guid Id { get; private set; }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsBlock { get; private set; }
    }
}
