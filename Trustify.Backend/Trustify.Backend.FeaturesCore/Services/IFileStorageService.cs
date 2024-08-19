using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesCore.Services
{
    public interface IFileStorageService
    {
        Task<ResultMessage<bool>> UploadFile(byte[] data, string relativePath);

        Task<ResultMessage<BasicFileInfo>> DownloadFile(string relativePath);

        Task<ResultMessage<bool>> DeleteFile(string relativePath);
    }
}
