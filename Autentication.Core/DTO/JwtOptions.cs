namespace Autentication.Core.DTO
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
        public int MaxQtdLoginAttempt { get; set; }
        public int MinutesToWaitForANewLogin { get; set; }
    }
}