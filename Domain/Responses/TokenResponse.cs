namespace ChatApi.Domain.Responses
{
    public class TokenResponse
    {
        public TokenResponse() { }

        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
