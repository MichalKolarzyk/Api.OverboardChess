using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories.ViewModels
{
    public class MeetingWithUserViewModel
    {
        public Guid MeetingId { get; set; }
        public Guid UserOwnerId { get; set; }
        public string UserOwnerName { get; set; } = string.Empty;
    }
}
