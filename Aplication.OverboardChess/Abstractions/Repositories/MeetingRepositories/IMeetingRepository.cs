using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels;
using Domain.OverboardChess.Meetings;
using System.Linq.Expressions;

namespace Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task<List<MeetingWithUserViewModel>> GetMeetingsWhereUserParticipate(Guid userParticipate, long skip = 0, long take = 100);
        Task<List<MeetingWithUserViewModel>> GetUserOwnerMeetings(Guid userOwnerId, long skip = 0, long take = 100);
        Task<List<MeetingWithUserViewModel>> FindMeetings(Guid userOwnerId, long skip = 0, long take = 100);
    }
}
