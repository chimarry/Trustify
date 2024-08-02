namespace Trustify.Backend.FeaturesService.Security
{
    public interface IJwtManager
    {
        string GetToken();

        string[] GetRoles();

        bool IsValid();
    }
}
