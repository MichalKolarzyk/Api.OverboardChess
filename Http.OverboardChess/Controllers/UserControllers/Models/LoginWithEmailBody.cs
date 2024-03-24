namespace Http.OverboardChess.Controllers.UserControllers.Models
{
    public class LoginWithEmailBody(string email)
    {
        public string Email { get; } = email;
    }
}
