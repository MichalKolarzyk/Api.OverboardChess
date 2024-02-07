using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels;
using Domain.OverboardChess.Meetings;
using Domain.OverboardChess.Users;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
namespace Infrastructure.OverboardChess.Database.MongoDbRepositories
{
    public class MeetingMongoRepository(MongoDatabase database) : MongoRepository<Meeting>(database), IMeetingRepository
    {
        public async Task<List<MeetingWithUserViewModel>> GetMeetingWithUserViewModels(Expression<Func<MeetingWithUserViewModel, bool>> predicate, long skip = 0, long take = 100)
        {
            return await _mongoCollection.Aggregate()
                .Lookup<User, MeetingWithUser>(
                    nameof(User),
                    nameof(Meeting.OwnerId),
                    nameof(User.Id),
                    nameof(MeetingWithUser.User))
                .Unwind<MeetingWithUser>(nameof(MeetingWithUser.User))
                .Project(u => new MeetingWithUserViewModel
                {
                    MeetingId = u.Id,
                    UserOwnerId = u.User.Id,
                    UserOwnerName = u.User.Username,
                })
                .Match(predicate)
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        private class MeetingWithUser : MongoAggregateModel
        {
            public required Meeting Meeting { get; set; }
            public required User User { get; set; }
        }
    }
}
