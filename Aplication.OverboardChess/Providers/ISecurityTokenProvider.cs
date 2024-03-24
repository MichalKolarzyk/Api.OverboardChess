using Domain.OverboardChess.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OverboardChess.Providers
{
    public interface ISecurityTokenProvider
    {
        string GetJwt(Claims claims, DateTime expires);
    }

    public class Claims
    {
        public List<Claim> SeciurityClaims { get; set; }

        private Claims(List<Claim> seciurityClaims)
        {
            SeciurityClaims = seciurityClaims;
        }

        public static Claims FromUser(User user)
        {
            var claims = new List<Claim>
            {
                new ("Id", user.Id.ToString()),
            };

            return new Claims(claims);
        }
    }
}
