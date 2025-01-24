using SecurityWebApp.Infrastructure.Entities.Models;
using System;

namespace SecurityWebApp.Infrastructure.Entities.DTO
{
    public class CustomerDTO
    {
        public CustomerDTO() { }

        public CustomerDTO(CustomerModel pEntity) 
        {
            Id = pEntity.Id;
            CreationDate = pEntity.CreationDate;
            UpdateDate = pEntity.UpdateDate;
            FirstName = pEntity.FirstName;
            LastName = pEntity.LastName; 
            Mail = pEntity.Mail;
            Active = pEntity.Active;
            Block = pEntity.Block;
        }

        public Guid Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public bool Active { get; set; }

        public bool Block { get; set; }
    }
}
