using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Requests.GetInvitationRequests
{
    public class GetRecivedInvitationsResponse
    {
        public List<Guid> RecivedInvitations { get; set; } = new List<Guid>();
    }
}
