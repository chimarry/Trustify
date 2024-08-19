using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesCore.Services
{
    public interface ITextualContentService
    {
        Task<ResultMessage<bool>> AddTextualContent(TextualContentDTO content);

        Task<ResultMessage<bool>> DeleteTextualContent(long textualContentId);

        ResultMessage<IEnumerable<TextualContentDTO>> GetAllTextualContent(int skip, int take);

        Task<ResultMessage<TextualContentDTO>> GetTextualContent(long textualContentId);
    }
}
