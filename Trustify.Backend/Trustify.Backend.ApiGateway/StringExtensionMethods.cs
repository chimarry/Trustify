using System.Runtime.CompilerServices;

namespace Trustify.Backend.ApiGateway
{
    public static class StringExtensionMethods
    {
        public static string[] ToStringArray(this string? value)
        {
            return value?.Replace("[", "").Replace("]", "").Split(",") ?? [];
        }
    }
}
