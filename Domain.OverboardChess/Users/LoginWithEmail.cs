using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.OverboardChess.DomainBase;
using Utilities.OverboardChess.Validation;

namespace Domain.OverboardChess.Users
{
    public class LoginWithEmail(string email, string code) : AggregateRoot
    {
        public string Email { get; set; } = email;
        public string Code { get; set; } = code;
        public int Attempts { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public LoginWithEmailState State { get; set; } = LoginWithEmailState.InProgress;

        public Result TryComplete(string code)
        {
            if (Attempts > 3)
            {
                State = LoginWithEmailState.Rejected;
                return Result.FromError("code", "To many attempts.");
            }
            Attempts++;

            if(Code != code)
                return Result.FromError("code", "Wrong code");

            State = LoginWithEmailState.Complete;
            return Result.Ok;
        }
    }

    public enum LoginWithEmailState
    {
        InProgress,
        Complete,
        Rejected,
    }
}
