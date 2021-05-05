namespace SC.API.CleanArchitecture.Application.Common
{
    public class TokenSettings
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string JwtKey { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
