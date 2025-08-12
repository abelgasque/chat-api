namespace ChatApi.Domain.Responses
{
    public class TokenResponse
    {
        public TokenResponse() { }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; } = null;
        public int ExpiresIn { get; set; }
    }
}
