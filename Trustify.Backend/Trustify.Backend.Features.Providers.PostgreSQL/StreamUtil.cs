using System.Reflection;

namespace Trustify.Backend.Features.Providers.PostgreSQL
{
    public static class StreamUtil
    {
        public static string GetManifestResourceName(string resourceName)
          => Assembly.GetExecutingAssembly()
                     .GetManifestResourceNames()
                     .Single(str => str.EndsWith(resourceName, StringComparison.InvariantCulture));

        public static Stream GetManifestResourceStream(string resourceName)
            => Assembly.GetExecutingAssembly().GetManifestResourceStream(GetManifestResourceName(resourceName)) ?? throw new NotSupportedException();

        public static string GetManifestResourceString(string resourceName)
        {
            using var sr = new StreamReader(GetManifestResourceStream(resourceName));
            return sr.ReadToEnd() ?? string.Empty;
        }

        public static byte[] GetManifestResourceBytes(string resourceName)
        {
            using Stream sr = GetManifestResourceStream(resourceName);
            using var memoryStream = new MemoryStream();
            sr.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
