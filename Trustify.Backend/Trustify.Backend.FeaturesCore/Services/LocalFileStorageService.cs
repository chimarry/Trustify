using Microsoft.Extensions.Options;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;
using Trustify.Backend.FeaturesCore.Util;

namespace Trustify.Backend.FeaturesCore.Services
{
    /// <summary>
    /// Manipulates file storage that is on local machine.
    /// </summary>
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly FileStorageOptions fileStorageOptions;
        private readonly IExceptionHandler exceptionHandler;

        public LocalFileStorageService(IOptions<FileStorageOptions> fileStorageOptions, IExceptionHandler exceptionHandler)
        {
            this.fileStorageOptions = fileStorageOptions.Value;
            this.exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Deletes file with specified relative path.
        /// </summary>
        /// <param name="relativePath">Relative path for the file</param>
        /// <returns>OperationStatus.NotFound in case of a missing file</returns>
        public async Task<ResultMessage<bool>> DeleteFile(string relativePath)
        {
            try
            {
                string absolutePath = PathBuilder.BuildApsolutePathForFile(fileStorageOptions.FileStorageLocation, relativePath);
                if (!File.Exists(absolutePath))
                    return new ResultMessage<bool>(OperationStatus.NotFound);

                await Task.Run(() => File.Delete(absolutePath));

                return new ResultMessage<bool>(true, OperationStatus.Success);

            }
            catch (IOException e)
            {
                (OperationStatus status, string detailedMessage) = exceptionHandler.HandleException(e);
                return new ResultMessage<bool>(status, detailedMessage);
            }
        }

        /// <summary>
        /// Finds specified file on file system based on relative path and returns its data.
        /// </summary>
        /// <param name="relativePath">Relative path to file</param>
        /// <returns>OperationStatus.NotFound in case of a missing file</returns>
        public async Task<ResultMessage<BasicFileInfo>> DownloadFile(string relativePath)
        {
            try
            {
                if (relativePath == null)
                    return new ResultMessage<BasicFileInfo>(OperationStatus.InvalidData);

                if (relativePath == PathBuilder.DefaultMachineImage)
                    return DownloadDefaultResource(PathBuilder.DefaultMachineImage);

                string absolutePath = PathBuilder.BuildApsolutePathForFile(fileStorageOptions.FileStorageLocation, relativePath);
                if (!File.Exists(absolutePath))
                    return new ResultMessage<BasicFileInfo>(OperationStatus.NotFound);

                byte[] data = await File.ReadAllBytesAsync(absolutePath);
                var fileInfo = new BasicFileInfo()
                {
                    FileData = data,
                    FileName = Path.GetFileName(absolutePath)
                };

                return new ResultMessage<BasicFileInfo>(fileInfo, OperationStatus.Success);
            }
            catch (IOException e)
            {
                (OperationStatus status, string detailedMessage) = exceptionHandler.HandleException(e);
                return new ResultMessage<BasicFileInfo>(status, detailedMessage);
            }
        }

        /// <summary>
        /// Writes file from provided data to local file system, on location built from 
        /// configured root path and provided relative path.
        /// </summary>
        /// <param name="data">File data</param>
        /// <param name="relativePath">Relative path for the file</param>
        /// <returns></returns>
        public async Task<ResultMessage<bool>> UploadFile(byte[] data, string relativePath)
        {
            try
            {
                string absolutePath = PathBuilder.BuildApsolutePathForFile(fileStorageOptions.FileStorageLocation, relativePath);
                string? directoryName = Path.GetDirectoryName(absolutePath);
                if (directoryName != null)
                {
                    Directory.CreateDirectory(directoryName);
                    using FileStream fileStream = File.Create(absolutePath);
                    await fileStream.WriteAsync(data);

                    return new ResultMessage<bool>(true, OperationStatus.Success);
                }
                else
                    return new ResultMessage<bool>(false, OperationStatus.FileSystemError);
            }
            catch (IOException ex)
            {
                (OperationStatus status, string detailedMessage) = exceptionHandler.HandleException(ex);
                return new ResultMessage<bool>(status, detailedMessage);
            }
        }

        private static ResultMessage<BasicFileInfo> DownloadDefaultResource(string resourceName)
        {
            byte[] data = StreamUtil.GetManifestResourceBytes(resourceName);
            var fileInfo = new BasicFileInfo()
            {
                FileData = data,
                FileName = resourceName
            };
            return new ResultMessage<BasicFileInfo>(fileInfo);
        }
    }
}
