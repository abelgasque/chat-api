namespace ChatApi.Domain.Entities.DTO
{
    public class TokenDTO
    {
        public TokenDTO() { }

        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
