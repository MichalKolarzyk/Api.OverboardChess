using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Abstractions
{
    public interface ICurrentIdentity
    {
        public Guid? UserId { get; }
    }

    public static class CurrentIdentityExtensions
    {
        public static Guid GetUserId(this ICurrentIdentity currentIdentity)
        {
            if (currentIdentity.UserId == null)
                throw new Exception("User is not provided for the current request.");

            return currentIdentity.UserId.Value;
        }
    }
}
