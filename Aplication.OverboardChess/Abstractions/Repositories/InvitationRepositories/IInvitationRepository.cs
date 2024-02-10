using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories.ViewModels;
using Domain.OverboardChess.Invitations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Task<List<RecivedInvitationViewModel>> GetRecivedInvitiationsViewModel(Guid userId);
    }
}
