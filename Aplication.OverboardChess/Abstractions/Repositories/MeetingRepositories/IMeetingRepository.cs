using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels;
using Domain.OverboardChess.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task<List<MeetingWithUserViewModel>> GetMeetingWithUserViewModels(Expression<Func<MeetingWithUserViewModel, bool>> predicate, long skip = 0, long take = 100);
    }
}
