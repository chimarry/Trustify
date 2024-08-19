using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.Database.Entities;
using Trustify.Backend.FeaturesCore.DTO;
using Trustify.Backend.FeaturesCore.Util;

namespace Trustify.Backend.FeaturesCore.Services
{
    public class ImageContentService(TrustifyDbContext context, IMapper mapper, IExceptionHandler exceptionHandler,
        IFileStorageService fileStorageService) : IImageContentService
    {
        private readonly TrustifyDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly IExceptionHandler handler = exceptionHandler;
        private readonly IFileStorageService fileStorageService = fileStorageService;

        public async Task<ResultMessage<bool>> AddImage(ImageContentDTO dto, BasicFileInfo? file)
        {
            try
            {
                ImageContent image = mapper.Map<ImageContent>(dto);

                if (file != null)
                {
                    string path = PathBuilder.BuildRelativePathForImages(file.FileName);
                    await fileStorageService.UploadFile(file.FileData, path);
                    image.Size = file.FileData.Length;
                    image.Path = path;
                }
                await context.AddAsync(image);
                await context.SaveChangesAsync();

                return new ResultMessage<bool>(true);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<bool>> Delete(int imageId)
        {
            try
            {
                ImageContent? imageContent = await context.ImageContents.SingleOrDefaultAsync(x => x.ImageContentId == imageId);
                if (imageContent == null)
                    return new ResultMessage<bool>(OperationStatus.NotFound);

                await fileStorageService.DeleteFile(imageContent.Path);

                context.ImageContents.Remove(imageContent);
                await context.SaveChangesAsync();

                return new ResultMessage<bool>(true);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public ResultMessage<IEnumerable<ImageContentDTO>> GetAll(int skip, int take)
        {
            try
            {
                return new ResultMessage<IEnumerable<ImageContentDTO>>(
                    context.ImageContents.OrderBy(x => x.Name).Skip(skip).Take(take).Select(x => mapper.Map<ImageContentDTO>(x)));
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<IEnumerable<ImageContentDTO>>(status, message);
            }
        }

        public async Task<ResultMessage<ImageContentDTO>> GetImage(int imageId)
        {
            try
            {
                ImageContent? image = await context.ImageContents.SingleOrDefaultAsync(x => x.ImageContentId == imageId);
                if (image == null)
                    return new ResultMessage<ImageContentDTO>(OperationStatus.NotFound);
                return new ResultMessage<ImageContentDTO>(mapper.Map<ImageContentDTO>(image));
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<ImageContentDTO>(status, message);
            }
        }

        public async Task<ResultMessage<BasicFileInfo>> DownloadImage(int imageId)
        {
            try
            {
                ImageContent? image = await context.ImageContents.SingleOrDefaultAsync(x => x.ImageContentId == imageId);
                if (image == null)
                    return new ResultMessage<BasicFileInfo>(OperationStatus.NotFound);
                return await fileStorageService.DownloadFile(image.Path);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<BasicFileInfo>(status, message);
            }
        }
    }
}
