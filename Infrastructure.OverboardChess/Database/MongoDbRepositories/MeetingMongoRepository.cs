using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels;
using Domain.OverboardChess.Meetings;
using Domain.OverboardChess.Users;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
namespace Infrastructure.OverboardChess.Database.MongoDbRepositories
{
    public class MeetingMongoRepository(MongoDatabase database) : MongoRepository<Meeting>(database), IMeetingRepository
    {
        public async Task<List<MeetingWithUserViewModel>> FindMeetings(Guid userOwnerId, long skip = 0, long take = 100)
        {
            return await _mongoCollection.Aggregate()
                .Match(m => m.State != MeetingState.Done 
                    && m.IsPrivate != true
                    && m.OwnerId != userOwnerId 
                    && !m.Participants.Contains(userOwnerId))
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
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        public async Task<List<MeetingWithUserViewModel>> GetMeetingsWhereUserParticipate(Guid userParticipate, long skip = 0, long take = 100)
        {
            return await _mongoCollection.Aggregate()
                .Match(m => m.Participants.Contains(userParticipate) || m.OwnerId == userParticipate)
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
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        public async Task<List<MeetingWithUserViewModel>> GetUserOwnerMeetings(Guid userOwnerId, long skip = 0, long take = 100)
        {
            return await _mongoCollection.Aggregate()
                .Match(m => m.OwnerId == userOwnerId)
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
