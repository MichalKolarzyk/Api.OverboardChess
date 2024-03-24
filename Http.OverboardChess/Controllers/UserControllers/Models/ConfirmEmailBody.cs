namespace Http.OverboardChess.Controllers.UserControllers.Models
{
    public class ConfirmEmailBody(string email, string code)
    {
        public string Email { get; } = email;
        public string Code { get; } = code;
    }
}
