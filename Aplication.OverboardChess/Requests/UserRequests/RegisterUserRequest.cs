using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.UserRequests
{
    public class RegisterUserRequest(string username, string password) : IRequest
    {
        public string Username { get; } = username;
        public string Password { get; } = password;
    }
}
