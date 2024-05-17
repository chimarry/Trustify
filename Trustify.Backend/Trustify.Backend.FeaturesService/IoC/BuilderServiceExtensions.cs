using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustify.Backend.Features.Providers.MSSQL;
using Trustify.Backend.Features.Providers.PostgreSQL;
using Trustify.Backend.FeaturesCore.Database.Entities;
using Trustify.Backend.FeaturesService.Models;

namespace Trustify.Backend.FeaturesService.IoC
{
    public static class BuilderServiceExtensions
    {
        /// Configure database context with appropriate database driver (in this case Sql Server .NET driver is used).  
        /// It is neccessary that connection string is also available in the configuration.
        /// /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration">Configuration object that contains information about database connection string.</param>
        public static void ConfigureDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ApiSettingsKeys.DatabaseConnectionStringKey) ?? throw new ArgumentNullException();
            string databaseProvider = configuration.GetSection(ApiSettingsKeys.ConnectionStringsKey).GetValue<string>(ApiSettingsKeys.DatabaseProviderKey) ?? throw new ArgumentNullException();

            serviceCollection.AddDbContext<TrustifyDbContext>(optionsAction: options =>
            {
                switch ((DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), databaseProvider))
                {
                    case DatabaseProvider.SqlServer:
                        options.UseSqlServer(connectionString,
                                            x => x.MigrationsAssembly("Trustify.Backend.Features.Providers.MSSQL"))
                        .ReplaceService<IMigrationsSqlGenerator, SqlServerMigrationsGenerator>(); break;
                    case DatabaseProvider.PostgreSql:
                        options.UseNpgsql(connectionString,
                                            x => x.MigrationsAssembly("Trustify.Backend.Features.Providers.PostgreSQL"))
                               .ReplaceService<IMigrationsSqlGenerator, PostgreSqlMigrationsGenerator>(); break;
                    default: throw new Exception("Not supported");
                }
            }
            , contextLifetime: ServiceLifetime.Scoped
            , optionsLifetime: ServiceLifetime.Scoped);
        }
    }
}
