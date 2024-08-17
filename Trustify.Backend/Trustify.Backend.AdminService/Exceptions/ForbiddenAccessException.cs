namespace Trustify.Backend.AdminService.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException():base("Forbidden access")
        {
        }
    }
}
