using Domain.OverboardChess.Base;

namespace Domain.OverboardChess.Users
{
    public class User(string username, string hashPassword) : AggregateRoot
    {
        public string Username { get; set; } = username;
        public string HashPassword { get; set; } = hashPassword;
    }
}
