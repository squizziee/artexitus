using Artexitus.IdentityMicroservice.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Artexitus.IdentityMicroservice.API.Attributes
{
    public class AuthorizeWithRefreshTokenAttribute : TypeFilterAttribute
    {
        public AuthorizeWithRefreshTokenAttribute()
       : base(typeof(AuthorizeWithRefreshTokenFilter))
        {
        }

    }
}
