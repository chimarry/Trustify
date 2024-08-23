using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesCore.Services
{
    public interface ISectionsService
    {
        Task<ResultMessage<bool>> AddSection(SectionDTO section);

        Task<ResultMessage<bool>> DeleteSection(long sectionId);

        ResultMessage<IEnumerable<BasicSectionDTO>> GetSections();

        ResultMessage<IEnumerable<BasicSectionDTO>> FilterSections(string[] roles);

        Task<ResultMessage<SectionDTO>> GetDetails(long sectionId);

        ResultMessage<IEnumerable<RoleDTO>> GetRoles();
    }
}
