using ChatApi.Domain.Entities.Models;
using System;

namespace ChatApi.Domain.Responses
{
    public class UserResponse
    {
        public UserResponse(UserModel pEntity)
        {
            Id = pEntity.Guid;
            CreatedAt = pEntity.CreatedAt;
            UpdatedAt = pEntity.UpdatedAt;
            Username = pEntity.Name;
            Email = pEntity.Email;
            ActiveAt = pEntity.ActiveAt;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime? ActiveAt { get; set; }
    }
}
