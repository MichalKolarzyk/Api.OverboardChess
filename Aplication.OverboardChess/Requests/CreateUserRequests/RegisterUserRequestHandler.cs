using Aplication.OverboardChess.Abstractions;
using Domain.OverboardChess.Users;
using MediatR;
using Utilities.OverboardChess.Cryptography;

namespace Aplication.OverboardChess.Requests.CreateUserRequests
{
    public class RegisterUserRequestHandler(IRepository<User> userRepository) : IRequestHandler<RegisterUserRequest>
    {
        private readonly IRepository<User> _userRepository = userRepository;

        public async Task Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.Any(u => u.Username == request.Username);
            if (userExists)
                throw new Exception("user already exists");

            var user = new User(request.Username, HashConverter.ToSHA256(request.Password));

            await _userRepository.InsertAsync(user);
        }
    }
}
