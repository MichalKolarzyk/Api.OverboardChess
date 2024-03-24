using Utilities.OverboardChess.DomainBase;

namespace Domain.OverboardChess.Users
{
    public class User : AggregateRoot
    {
        public User(string email)
        {
            Email = email;
            Username = string.Empty;
            HashPassword = string.Empty;
        }

        public User(string username, string hashPassword)
        {
            Email = string.Empty;
            Username = username;
            HashPassword = hashPassword;
        }

        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }
    }
}
