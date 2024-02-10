using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories.ViewModels;
using Domain.OverboardChess.Invitations;
using Domain.OverboardChess.Meetings;
using Domain.OverboardChess.Users;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.OverboardChess.Database.MongoDbRepositories
{
    public class InvitationMongoRepository : MongoRepository<Invitation>, IInvitationRepository
    {
        public InvitationMongoRepository(MongoDatabase database) : base(database)
        {
        }

        public async Task<List<RecivedInvitationViewModel>> GetRecivedInvitiationsViewModel(Guid userId)
        {
            return await _mongoCollection.Aggregate()
                .Match(i => i.InvitedUserId == userId && i.State == InvitationState.Created)
                .Lookup<User, InvitationWithMeetingWithUser>(
                    nameof(User),
                    nameof(Invitation.OwnerUserId),
                    nameof(User.Id),
                    nameof(InvitationWithMeetingWithUser.UserOwner))
                .Unwind<InvitationWithMeetingWithUser>(nameof(InvitationWithMeetingWithUser.UserOwner))
                .Lookup<Meeting, InvitationWithMeetingWithUser>(
                    nameof(Meeting),
                    nameof(Invitation.MeetingId),
                    nameof(Meeting.Id),
                    nameof(InvitationWithMeetingWithUser.Meeting))
                .Unwind<InvitationWithMeetingWithUser>(nameof(InvitationWithMeetingWithUser.Meeting))
                .Match(a => a.Meeting.State != MeetingState.Done)
                .Project(a => new RecivedInvitationViewModel
                {
                    Id = a.Id,
                    MeetingTitle = a.Meeting.Title,
                    OwnerUser = a.UserOwner.Username,
                    StartDate = a.Meeting.Start,
                })
                //.Skip(skip)
                //.Limit(take)
                .ToListAsync();
        }

        private class InvitationWithMeetingWithUser : MongoAggregateModel
        {
            public required Meeting Meeting { get; set; }
            public required User UserOwner { get; set; }
            public required Invitation Invitation { get; set; }
        }
    }
}
