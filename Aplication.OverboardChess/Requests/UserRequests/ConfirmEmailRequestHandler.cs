using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Providers;
using Domain.OverboardChess.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.OverboardChess.Exceptions;
using Utilities.OverboardChess.Validation;

namespace Aplication.OverboardChess.Requests.UserRequests
{
    public class ConfirmEmailRequestHandler(
        ISecurityTokenProvider securityTokenProvider, 
        IRepository<LoginWithEmail> loginWithEmailRepository, 
        IRepository<User> userRepository) : IRequestHandler<ConfirmEmailRequest, string>
    {
        public async Task<string> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var loginWithEmail = await loginWithEmailRepository
                .GetAsync(l => l.Email == request.Email && l.State == LoginWithEmailState.InProgress) 
                ?? throw Result.FromError("email", "The email is not valid.").ToDomainException(DomainExceptionType.BadRequest);
            
            var result = loginWithEmail.TryComplete(request.Code);
            await loginWithEmailRepository.UpdateAsync(loginWithEmail);

            if(result.HasErrors())
                throw result.ToDomainException(DomainExceptionType.Forbidden);

            var user = await userRepository.GetAsync(u => u.Email == request.Email);
            if(user == null) {
                user = new User(request.Email);
                await userRepository.InsertAsync(user);
            }

            return securityTokenProvider.GetJwt(Claims.FromUser(user), DateTime.Now.AddYears(10));
        }
    }
}
