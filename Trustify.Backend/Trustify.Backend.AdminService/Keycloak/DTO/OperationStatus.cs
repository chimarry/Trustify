namespace Trustify.Backend.AdminService.Keycloak.DTO
{
    /// <summary>
    /// Descriptive values for some standard outcomes of method execution.
    /// </summary>
    public enum OperationStatus
    {
        Success,
        DatabaseError,
        FileSystemError,
        NotFound,
        Exists,
        InvalidData,
        NotSupported,
        NotCompatible,
        UnknownError,
        ForbiddenAccess,
        Unauthorized
    }
}
