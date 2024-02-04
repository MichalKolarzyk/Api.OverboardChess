using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.OverboardChess.TokenProviders
{
    public class Key
    {
        public static Key GetSymetricSecurityKey(string password) => new (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(password)));


        public SecurityKey Value { get;}

        private Key(SecurityKey securityKey)
        {
            Value = securityKey;
        }
    }
}
