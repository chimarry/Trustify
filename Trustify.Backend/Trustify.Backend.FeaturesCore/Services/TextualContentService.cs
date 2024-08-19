using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.Database.Entities;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesCore.Services
{
    public class TextualContentService(TrustifyDbContext context, IMapper mapper, IExceptionHandler handler) : ITextualContentService
    {
        private readonly TrustifyDbContext context = context;
        private readonly IExceptionHandler handler = handler;
        private readonly IMapper mapper = mapper;

        public async Task<ResultMessage<bool>> AddTextualContent(TextualContentDTO content)
        {
            try
            {
                var entity = mapper.Map<TextualContent>(content);
                await context.AddAsync(entity);
                await context.SaveChangesAsync();

                return new ResultMessage<bool>(true);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<bool>> DeleteTextualContent(long textualContentId)
        {
            try
            {
                TextualContent? entity = await context.TextualContents.SingleOrDefaultAsync(x => x.TextualContentId == textualContentId);
                if (entity == null)
                    return new ResultMessage<bool>(false, OperationStatus.NotFound);

                context.Remove(entity);
                await context.SaveChangesAsync();

                return new ResultMessage<bool>(true);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public ResultMessage<IEnumerable<TextualContentDTO>> GetAllTextualContent(int skip, int take)
        {
            try
            {
                return new ResultMessage<IEnumerable<TextualContentDTO>>(
                       context.TextualContents.Skip(skip).Take(take).Select(x => mapper.Map<TextualContentDTO>(x)));

            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<IEnumerable<TextualContentDTO>>(status, message);
            }
        }

        public async Task<ResultMessage<TextualContentDTO>> GetTextualContent(long textualContentId)
        {
            TextualContent? entity = await context.TextualContents.SingleOrDefaultAsync(x => x.TextualContentId == textualContentId);
            if (entity == null)
                return new ResultMessage<TextualContentDTO>(null, OperationStatus.NotFound);

            return new ResultMessage<TextualContentDTO>(mapper.Map<TextualContentDTO>(entity));
        }
    }
}
