namespace Trustify.Backend.AdminService.Exceptions
{
    public class AppNotConfiguredException : Exception
    {
        public AppNotConfiguredException() : base("Application is not configured properly. Check appsettings for missing key-value pairs.")
        {
        }
    }
}
