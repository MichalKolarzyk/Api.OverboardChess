using Aplication.OverboardChess.Abstractions;

namespace Http.OverboardChess.Providers
{
    public class CurrentIdentityHttp : ICurrentIdentity
    {
        public CurrentIdentityHttp(IHttpContextAccessor contextAccessor)
        {
            var userIdValue = contextAccessor?.HttpContext?.User.Claims.First(c => c.Type == "Id")?.Value;
            if(userIdValue != null)
                UserId = Guid.Parse(userIdValue);
        }

        public Guid? UserId { get ; }
    }
}
