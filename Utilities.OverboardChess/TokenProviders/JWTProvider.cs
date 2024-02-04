using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.OverboardChess.TokenProviders
{
    public class JWTProvider
    {
        public static string Create(Key key, List<Claim> claims, DateTime expires, string? audience = null, string? issuer = null)
        {
            var signingCredentials = new SigningCredentials(key.Value, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                audience: audience,
                issuer: issuer,
                expires: expires);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public static bool Validate(string token, ValidationParameters validationParameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, validationParameters.Value, out SecurityToken validatedToken);
                return true;
            }
            catch
            { 
                return false; 
            }
        }
    }
}
