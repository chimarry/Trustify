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
    public class ImageContentConfiguration : IEntityTypeConfiguration<ImageContent>
    {
        public void Configure(EntityTypeBuilder<ImageContent> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Path).IsRequired();
            builder.Property(x => x.Size).IsRequired();
            builder.Property(x=>x.UploadedOn).IsRequired();
        }
    }
}
