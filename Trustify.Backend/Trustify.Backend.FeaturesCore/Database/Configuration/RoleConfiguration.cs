using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Trustify.Backend.FeaturesCore.Database.Entities;

namespace Trustify.Backend.FeaturesCore.Database.Configuration
{
    /// <summary>
    /// Configure table construction using FluentApi. Specify what 
    /// attributes does the entity have and what are the constraints.
    /// </summary> 
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
