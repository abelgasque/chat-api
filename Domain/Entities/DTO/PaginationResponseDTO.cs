﻿namespace ChatApi.Domain.Entities.DTO
{
    public class PaginationResponseDTO : PaginationRequestDTO
    {
        public PaginationResponseDTO() { }

        public int Total { get; set; }
        public object Data { get; set; }
    }
}
