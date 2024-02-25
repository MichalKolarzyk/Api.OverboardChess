using Aplication.OverboardChess.Abstractions.Repositories;
using Domain.OverboardChess.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.OverboardChess.Cryptography;
using Utilities.OverboardChess.TokenProviders;

namespace Aplication.OverboardChess.Requests.UserRequests
{
    public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, string>
    {
        private readonly IRepository<User> _userRepository;

        public LoginUserRequestHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Username == request.Username);

            if (user == null)
                throw new Exception("User not found.");

            var hashPassword = HashConverter.ToSHA256(request.Password);

            if(user.HashPassword != hashPassword)
                throw new Exception("Wrong username or password.");

            var key = Key.GetSymetricSecurityKey("oaisdjasoidaslkdmnaskjdbaskdbasukdjasdsa");

            var claims = new List<Claim>
            {
                new ("Id", user.Id.ToString()),
            };

            return JWTProvider.Create(key, claims, DateTime.Now.AddDays(1));
        }
    }
}
