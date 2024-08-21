using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesCore.Services
{
    public interface IImageContentService
    {
        Task<ResultMessage<ImageContentDTO>> AddImage(ImageContentDTO dto, BasicFileInfo? file);

        Task<ResultMessage<BasicFileInfo>> DownloadImage(int imageId);

        Task<ResultMessage<bool>> Delete(int imageId);

        Task<ResultMessage<ImageContentDTO>> GetImage(int imageId);

        ResultMessage<IEnumerable<ImageContentDTO>> GetAll(int skip, int take);
    }
}
