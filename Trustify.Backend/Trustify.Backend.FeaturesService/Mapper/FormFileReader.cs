using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesService.Mapper
{
    public static class FormFileReader
    {
        public static BasicFileInfo AsBasicFileInfo(this IFormFile file)
        {
            if (file.Length < 0)
                return null;
            using Stream stream = file.OpenReadStream();
            byte[] bytes = new byte[file.Length];
            stream.Read(bytes, 0, (int)file.Length);
            return new BasicFileInfo(file.FileName, bytes);
        }

        public static string AsBase64(this IFormFile file)
        {
            BasicFileInfo fileInfo = file.AsBasicFileInfo();
            File.WriteAllBytes(fileInfo.FileName, fileInfo.FileData);
            string fileString = Convert.ToBase64String(File.ReadAllBytes(fileInfo.FileName));
            File.Delete(fileInfo.FileName);
            return fileString;
        }
    }
}
