using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustify.Backend.Features.Providers.PostgreSQL
{
    public class PostgreSqlMigrationsGenerator : NpgsqlMigrationsSqlGenerator
    {
        public PostgreSqlMigrationsGenerator(MigrationsSqlGeneratorDependencies dependencies, INpgsqlSingletonOptions npgsqlSingletonOptions) : base(dependencies, npgsqlSingletonOptions)
        {
        }

        /// <summary>
        /// Depending on type of the migration operation, 
        /// this method generates appropriate SQL code.
        /// </summary>
        /// <param name="operation">Migration operation</param>
        /// <param name="model"></param>
        /// <param name="builder"></param>
        protected override void Generate(
            MigrationOperation operation,
            IModel? model,
            MigrationCommandListBuilder builder)
        {
            base.Generate(operation, model, builder);
        }
    }
}
