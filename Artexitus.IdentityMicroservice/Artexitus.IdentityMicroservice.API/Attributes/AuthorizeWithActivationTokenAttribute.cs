using Artexitus.IdentityMicroservice.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Artexitus.IdentityMicroservice.API.Attributes
{
    public class AuthorizeWithActivationTokenAttribute : TypeFilterAttribute
    {
        public AuthorizeWithActivationTokenAttribute()
       : base(typeof(AuthorizeWithActivationTokenFilter))
        {
        }
    }
}
