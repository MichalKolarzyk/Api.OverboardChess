using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.OverboardChess.DomainBase;

namespace Domain.OverboardChess.Users
{
    public class LoginWithEmail(string email, string code) : AggregateRoot
    {
        public string Email { get; set; } = email;
        public string Code { get; set; } = code;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public LoginWithEmailState State { get; set; } = LoginWithEmailState.InProgress;


        public void Complete()
        {
            State = LoginWithEmailState.Complete;
        }
    }

    public enum LoginWithEmailState
    {
        InProgress,
        Complete,
    }
}
