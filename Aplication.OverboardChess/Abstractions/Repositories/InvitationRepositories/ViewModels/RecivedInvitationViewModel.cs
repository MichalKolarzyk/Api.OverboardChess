using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories.ViewModels
{
    public class RecivedInvitationViewModel
    {
        public Guid Id { get; set; }
        public string OwnerUser { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public string MeetingTitle { get; set; } = string.Empty;
    }
}
