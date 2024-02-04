using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.OverboardChess.TokenProviders
{
    public class ValidationParameters
    {
        public static ValidationParameters Get(Key key, string audience, string issuer) => new (new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key.Value,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateIssuer = true,
            ValidIssuer = issuer
        });

        public static ValidationParameters Get(Key key) => new(new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key.Value,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
        });


        public TokenValidationParameters Value { get; set; }

        private ValidationParameters(TokenValidationParameters value)
        {
            Value = value;
        }

    }
}
