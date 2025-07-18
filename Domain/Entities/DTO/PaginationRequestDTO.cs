namespace ChatApi.Domain.Entities.DTO
{
    public class PaginationRequestDTO
    {
        public PaginationRequestDTO() { }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 25;
    }
}
