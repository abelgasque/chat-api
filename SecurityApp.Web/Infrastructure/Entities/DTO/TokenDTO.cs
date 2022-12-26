namespace SecurityApp.Web.Infrastructure.Entities.DTO
{
    public class TokenDTO
    {
        public TokenDTO() { }

        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public object Customer { get; set; }
    }
}
