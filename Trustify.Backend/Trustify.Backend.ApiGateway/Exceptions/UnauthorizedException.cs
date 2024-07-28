namespace Trustify.Backend.ApiGateway.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Not authorized")
        {
        }
    }
}
