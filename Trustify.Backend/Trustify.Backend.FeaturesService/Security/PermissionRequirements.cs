using Microsoft.AspNetCore.Authorization;

namespace ProductManager.WebAPI.Security
{
    /// <summary>
    /// Requires read operation on a resource.
    /// </summary>
    public class ReadPermission : IAuthorizationRequirement
    {
    }

    /// <summary>
    /// Requires write operation on a resource.
    /// </summary>
    public class WritePermission : IAuthorizationRequirement
    {

    }

    /// <summary>
    /// Requires update operation on a resource.
    /// </summary>
    public class UpdatePermission : IAuthorizationRequirement
    {

    }

    /// <summary>
    /// Requires delete operation on a resource.
    /// </summary>
    public class DeletePermission : IAuthorizationRequirement
    {

    }

    /// <summary>
    /// Requires create operation on a resource.
    /// </summary>
    public class CreatePermission : IAuthorizationRequirement
    {

    }
}
