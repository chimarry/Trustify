using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Trustify.Backend.FeaturesService.Security
{
    /// <summary>
    /// Handles authorization of an user.
    /// </summary>
    public class RoleHandler : IAuthorizationHandler
    {
        /// <summary>
        /// Read roles from JWT token without previous validation. Create the identity.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
