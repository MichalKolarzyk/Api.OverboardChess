using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.GetUserRequests
{
    public class LoginUserRequest(string username, string password) : IRequest<string>
    {
        public string Username { get; } = username;

        public string Password { get; } = password;
    }
}
