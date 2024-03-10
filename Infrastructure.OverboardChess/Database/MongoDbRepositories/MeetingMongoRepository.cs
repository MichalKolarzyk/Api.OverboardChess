using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels;
using Aplication.OverboardChess.Providers;
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
        public async Task<List<MeetingWithUserViewModel>> FindMeetings(Guid userOwnerId, DateTime minDate, long skip = 0, long take = 100)
        {
            return await _mongoCollection.Aggregate()
                .Match(m => m.State != MeetingState.Done 
                    && m.IsPrivate != true
                    && m.OwnerId != userOwnerId 
                    && m.End >= minDate
                    && !m.Participants.Contains(userOwnerId))
                .Lookup<User, MeetingWithUser>(
                    nameof(User),
                    nameof(Meeting.OwnerId),
                    nameof(User.Id),
                    nameof(MeetingWithUser.User))
                .Unwind<MeetingWithUser>(nameof(MeetingWithUser.User))
                .Project(m => new MeetingWithUserViewModel
                {
                    MeetingId = m.Id,
                    UserOwnerId = m.User.Id,
                    UserOwnerName = m.User.Username,
                    MeetingStartDate = m.Start,
                    MeetingTitle = m.Title,
                })
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        public async Task<List<MeetingWithUserViewModel>> GetMeetingsWhereUserParticipate(Guid userParticipate, DateTime minDate, long skip = 0, long take = 100)
        {
            return await _mongoCollection.Aggregate()
                .Match(m => m.End >= minDate && (m.Participants.Contains(userParticipate) || m.OwnerId == userParticipate))
                .Lookup<User, MeetingWithUser>(
                    nameof(User),
                    nameof(Meeting.OwnerId),
                    nameof(User.Id),
                    nameof(MeetingWithUser.User))
                .Unwind<MeetingWithUser>(nameof(MeetingWithUser.User))
                .Project(m => new MeetingWithUserViewModel
                {
                    MeetingId = m.Id,
                    UserOwnerId = m.User.Id,
                    UserOwnerName = m.User.Username,
                    MeetingStartDate = m.Start,
                    MeetingTitle = m.Title,
                })
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        public async Task<List<MeetingWithUserViewModel>> GetUserOwnerMeetings(Guid userOwnerId, DateTime minDate, long skip = 0, long take = 100)
        {
            return await _mongoCollection.Aggregate()
                .Match(m => m.End >= minDate && m.OwnerId == userOwnerId)
                .Lookup<User, MeetingWithUser>(
                    nameof(User),
                    nameof(Meeting.OwnerId),
                    nameof(User.Id),
                    nameof(MeetingWithUser.User))
                .Unwind<MeetingWithUser>(nameof(MeetingWithUser.User))
                .Project(m => new MeetingWithUserViewModel
                {
                    MeetingId = m.Id,
                    UserOwnerId = m.User.Id,
                    UserOwnerName = m.User.Username,
                    MeetingStartDate = m.Start,
                    MeetingTitle = m.Title,
                })
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        private class MeetingWithUser : Meeting
        {
            public required User User { get; set; }


        }
    }
}
