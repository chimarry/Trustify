using Microsoft.AspNetCore.Authorization;
using ProductManager.Core.Enums;
using ProductManager.Core.Services;
using System.Security.Claims;

namespace ProductManager.WebAPI.Security
{
    /// <summary>
    /// Handles authorization of an user.
    /// </summary>
    public class PermissionHandler : IAuthorizationHandler
    {
        private readonly IUserService userService;

        public PermissionHandler(IUserService userService) => this.userService = userService;

        /// <summary>
        /// Authorizes an user.
        /// Is user does not have permissions to that are required to access one resource, 
        /// user will be forbidden from accessing that resource. In other cases, access will be granted to 
        /// that user.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            if (!pendingRequirements.Any())
                return;
            string username = context.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            Permission[]? permissions = await userService.GetPermissions(username);

            if (CheckPermissions(pendingRequirements, permissions))
                pendingRequirements.ForEach(x => context.Succeed(x));
            else
                context.Fail();
        }

        /// <summary>
        /// Based on authorization requirements determines wheter user has needed permissions.
        /// </summary>
        /// <param name="pendingRequirements">Requirements needed to access the resource</param>
        /// <param name="permissions">Permissions that user has</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool CheckPermissions(List<IAuthorizationRequirement> pendingRequirements, Permission[]? permissions)
        {
            if (permissions == null || !permissions.Any())
                return false;

            Permission[] pendingPermissions = pendingRequirements.Select(x => x switch
            {
                ReadPermission r => Permission.Read,
                WritePermission w => Permission.Write,
                UpdatePermission u => Permission.Update,
                DeletePermission d => Permission.Delete,
                CreatePermission c => Permission.Create,
                _ => throw new NotImplementedException()
            }).ToArray();

            return pendingPermissions.All(x => permissions.Contains(x) || (RequiresWriteSubOperation(x) && permissions.Contains(Permission.Write)));
        }

        private static bool RequiresWriteSubOperation(Permission permission)
            => permission == Permission.Create
            || permission == Permission.Update
            || permission == Permission.Delete
            || permission == Permission.Write
            || permission == Permission.Read;
    }
}
