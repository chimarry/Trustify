using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.FeaturesCore.Database.Entities;

namespace Trustify.Backend.FeaturesCore.Database.Configuration
{
    /// <summary>
    /// Configure table construction using FluentApi. Specify what 
    /// attributes does the entity have and what are the constraints.
    /// </summary> 
    public class SectionTextualContentConfiguration : IEntityTypeConfiguration<SectionTextualContent>
    {
        public void Configure(EntityTypeBuilder<Entities.SectionTextualContent> builder)
        {
            throw new NotImplementedException();
        }
    }
}
