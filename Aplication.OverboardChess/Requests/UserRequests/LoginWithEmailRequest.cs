using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.UserRequests
{
    public class LoginWithEmailRequest(string email) : IRequest
    {
        public string Email { get; } = email;
    }
}
