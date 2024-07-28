namespace ProductManager.WebAPI.Security
{
    public static class Schemes
    {
        public const string ApiKeyScheme = "ApiKeyScheme";
        public const string ClientMachineScheme = "ClientMachineScheme";
        public const string JwtScheme = "JwtScheme";
        public const string CreatePermissionScheme = "CreatePermissionScheme";
        public const string DeletePermissionScheme = "DeletePermissionScheme";
        public const string UpdatePermissionScheme = "UpdatePermissionScheme";
        public const string WritePermissionScheme = "CreatePermissionScheme,DeletePermissionScheme,UpdatePermissionScheme";
        public const string ReadPermissionScheme = "ReadPermissionScheme";
        public const string ApiKeyAndJwtScheme = "ApiKeyScheme,JwtScheme";
    }
}
