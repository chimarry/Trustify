using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Update;

namespace Trustify.Backend.Features.Providers.MSSQL
{
    public class SqlServerMigrationsGenerator : SqlServerMigrationsSqlGenerator
    {
        public SqlServerMigrationsGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer) : base(dependencies, commandBatchPreparer)
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
