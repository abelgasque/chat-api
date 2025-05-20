using SecurityWebApp.Infrastructure.Entities.DTO;
using System;

namespace SecurityWebApp.Infrastructure.Entities.Filter
{
    public class CustomerFilter : PaginationRequestDTO
    {
        public Guid? Id { get; set; }
        
        public DateTime? CreationDateStart { get; set; }
        
        public DateTime? CreationDateEnd { get; set; }

        public DateTime? UpdateDateStart { get; set; }

        public DateTime? UpdateDateEnd { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public bool? Active { get; set; }

        public bool? Block { get; set; }
    }
}
