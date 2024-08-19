namespace Trustify.Backend.FeaturesCore.Util
{
    /// <summary>
    /// Generate relative and absolute paths for product manager.
    /// </summary>
    public static class PathBuilder
    {
        public const string DefaultMachineImage = "noImage.png";
        public const string Separator = "_";
        private const string rootImagesDirectory = "images";

        /// <summary>
        /// Build relative path for machine's profile image.
        /// </summary>
        /// <param name="oldFileName">Old file name necessary for getting file extension.</param>
        /// <returns>Relative path for the image file with new random name and extension</returns>
        public static string BuildRelativePathForImages(string oldFileName)
            => Path.Combine(rootImagesDirectory,
                GetRandomFileName() + Path.GetExtension(oldFileName));


        public static string BuildApsolutePathForFile(string fileStorageLocation, string relativePath)
            => Path.Combine(fileStorageLocation, relativePath);

        /// <summary>
        /// Generates random file name with extension that is extracted from base64 encoded content.
        /// </summary>
        /// <param name="base64Content">Base64 encoded file content</param>
        /// <returns>Random file name with extension</returns>
        public static string BuildRandomFileNameFromBase64(string base64Content)
        {
            string extension = base64Content[..5] switch
            {
                "IVBOR" => ".png",
                "AAABA" => ".ico",
                _ => ".jpg"
            };

            return $"{GetRandomFileName()}{extension}";
        }

        public static string GetRandomFileName()
            => Path.GetRandomFileName().Replace(".", Separator);
    }
}
