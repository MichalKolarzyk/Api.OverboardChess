using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Providers;
using Domain.OverboardChess.Users;
using MediatR;

namespace Aplication.OverboardChess.Requests.UserRequests
{
    internal class LoginWithEmailRequestHandler(IEmailProvider emailProvider, IRepository<LoginWithEmail> loginWithEmailRepository) : IRequestHandler<LoginWithEmailRequest>
    {
        public async Task Handle(LoginWithEmailRequest request, CancellationToken cancellationToken)
        {
            var existingLogins = await loginWithEmailRepository.DeleteManyAsync(l => l.Email == request.Email && l.State == LoginWithEmailState.InProgress);

            var code = new Random().Next(1000, 9999);
            var loginWithEmail = new LoginWithEmail(request.Email, code.ToString());
            await loginWithEmailRepository.InsertAsync(loginWithEmail);

            var email = new Email(request.Email,
                "Login to overboard chess",
                $"Your code is {code}");

            emailProvider.Send(email);
        }
    }
}
