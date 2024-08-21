using Microsoft.EntityFrameworkCore;
using Trustify.Backend.FeaturesCore.Database.Configuration;

namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public partial class TrustifyDbContext : DbContext
    {
        public virtual DbSet<ImageContent> ImageContents { get; set; }

        public virtual DbSet<TextualContent> TextualContents { get; set; }

        public virtual DbSet<Section> Sections { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public TrustifyDbContext(DbContextOptions options) : base(options)
        {
        }

        public TrustifyDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                throw new Exception("Not configured");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new ImageContentConfiguration());
            modelBuilder.ApplyConfiguration(new TextualContentConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
