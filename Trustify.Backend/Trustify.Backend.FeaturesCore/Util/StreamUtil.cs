using System.Reflection;
using System;

namespace Trustify.Backend.FeaturesCore.Util
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
            return sr.ReadToEnd().Replace(Environment.NewLine, " ", StringComparison.InvariantCulture) ?? string.Empty;
        }

        public static byte[] GetManifestResourceBytes(string resourceName)
        {
            using Stream sr = GetManifestResourceStream(resourceName);
            using var memoryStream = new MemoryStream();
            sr.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static async Task<byte[]> ReadAllBytes(string fileName)
        {
            byte[] buffer = Array.Empty<byte>();
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer.AsMemory(0, (int)fs.Length));
            }

            return buffer;
        }
    }
}
