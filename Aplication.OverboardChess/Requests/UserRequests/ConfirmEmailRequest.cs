using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.UserRequests
{
    public class ConfirmEmailRequest(string email, string code) : IRequest<string>
    {
        public string Email { get; } = email;
        public string Code { get; } = code;
    }
}
