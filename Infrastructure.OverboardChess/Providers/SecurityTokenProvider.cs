using Aplication.OverboardChess.Providers;
using System.Security.Claims;
using Utilities.OverboardChess.TokenProviders;

namespace Infrastructure.OverboardChess.Providers
{
    public class SecurityTokenProvider : ISecurityTokenProvider
    {
        public string GetJwt(Claims claims, DateTime expires)
        {
            var key = Key.GetSymetricSecurityKey("oaisdjasoidaslkdmnaskjdbaskdbasukdjasdsa");

            return JWTProvider.Create(key, claims.SeciurityClaims, expires);
        }
    }
}
