using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.Database.Entities;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesCore.Services
{
    public class SectionsService(TrustifyDbContext context, IMapper mapper, IExceptionHandler exceptionHandler) : ISectionsService
    {
        private readonly TrustifyDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly IExceptionHandler handler = exceptionHandler;

        public async Task<ResultMessage<bool>> AddSection(SectionDTO section)
        {
            try
            {
                Section sectionEntity = mapper.Map<Section>(section);
                if (section.ImageContents?.Length != 0)
                    sectionEntity.ImageContents = await context.ImageContents.Where(x => section.ImageContents.Any(y => y == x.ImageContentId)).ToListAsync();
                if (section.TextualContents?.Length != 0)
                    sectionEntity.TextualContents = await context.TextualContents.Where(x => section.TextualContents.Any(y => y == x.TextualContentId)).ToListAsync();
                if (section.Roles?.Length != 0)
                    sectionEntity.Roles = await context.Roles.Where(x => section.Roles.Any(y => y == x.RoleId)).ToListAsync();

                await context.Sections.AddAsync(sectionEntity);
                await context.SaveChangesAsync();

                return new ResultMessage<bool>(true);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<bool>> DeleteSection(long sectionId)
        {
            try
            {
                Section? section = await context.Sections.SingleOrDefaultAsync(x => x.SectionId == sectionId);
                if (section == null)
                    return new ResultMessage<bool>(OperationStatus.NotFound);

                context.Sections.Remove(section);
                await context.SaveChangesAsync();
                return new ResultMessage<bool>(true);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<bool>(status, message);
            }
        }

        public async Task<ResultMessage<SectionDTO>> GetDetails(long sectionId)
        {
            try
            {
                Section? section = await context.Sections.AsNoTracking()
                    .Include(x => x.TextualContents)
                    .Include(x => x.ImageContents)
                    .Include(x => x.Roles)
                    .SingleOrDefaultAsync(x => x.SectionId == sectionId);
                if (section == null)
                    return new ResultMessage<SectionDTO>(OperationStatus.NotFound);

                return new ResultMessage<SectionDTO>(mapper.Map<SectionDTO>(section));
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<SectionDTO>(status, message);
            }
        }

        public ResultMessage<IEnumerable<RoleDTO>> GetRoles()
        {
            try
            {
                var list = context.Roles.OrderBy(x => x.Name).Select(x => mapper.Map<RoleDTO>(x));
                return new ResultMessage<IEnumerable<RoleDTO>>(list);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<IEnumerable<RoleDTO>>(status, message);
            }
        }

        public ResultMessage<IEnumerable<BasicSectionDTO>> FilterSections(string[] roles)
        {
            try
            {
                var list = context.Sections.AsNoTracking()
                                  .Include(x => x.Roles);
                var result = list.Where(x => x.Roles.Select(x => x.Name)
                                                    .Intersect(roles)
                                                    .Count() != 0)
                                  .Select(x => mapper.Map<BasicSectionDTO>(x));
                return new ResultMessage<IEnumerable<BasicSectionDTO>>(result);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<IEnumerable<BasicSectionDTO>>(status, message);
            }
        }

        public ResultMessage<IEnumerable<BasicSectionDTO>> GetSections()
        {
            try
            {
                var list = context.Sections.AsNoTracking().Select(x => mapper.Map<BasicSectionDTO>(x));
                return new ResultMessage<IEnumerable<BasicSectionDTO>>(list);
            }
            catch (Exception ex)
            {
                (OperationStatus status, string message) = handler.HandleException(ex);
                return new ResultMessage<IEnumerable<BasicSectionDTO>>(status, message);
            }
        }
    }
}
