namespace Autentication.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int FailedLoginAttempt { get; set; }   
        public DateTime? Lockouttime { get; set; }
    }
}