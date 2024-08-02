namespace Trustify.Backend.FeaturesService.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Not authorized")
        {
        }
    }
}
