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
            State = UserState.Draft;
        }

        public User(string username, string hashPassword)
        {
            Email = string.Empty;
            Username = username;
            HashPassword = hashPassword;
            State = UserState.Active;
        }

        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }
        public UserState State { get; set; }
    }

    public enum UserState
    {
        Draft,
        Active,
        Archived,
    }
}
