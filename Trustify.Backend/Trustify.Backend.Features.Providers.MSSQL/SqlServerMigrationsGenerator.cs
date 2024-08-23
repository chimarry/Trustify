using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Update;
using Trustify.Backend.FeaturesCore.Database.MigrationOperations;

namespace Trustify.Backend.Features.Providers.MSSQL
{
    public class SqlServerMigrationsGenerator : SqlServerMigrationsSqlGenerator
    {
        public SqlServerMigrationsGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer) : base(dependencies, commandBatchPreparer)
        {
        }

        protected override void Generate(
              MigrationOperation operation,
              IModel? model,
              MigrationCommandListBuilder builder)
        {
            if (operation is ConfigureDatabaseMigrationOperation createDatabaseOperation)
            {
                GenerateConfigureDatabase(createDatabaseOperation, builder);
            }
            else if (operation is GrantPermissionsMigrationOperation grantPermissionOperation)
            {
                GenerateGrantPermission(grantPermissionOperation, builder);
            }
            else if (operation is RevokePermissionsMigrationOperation revokePermissionsOperation)
            {
                GenerateRevokePermissions(revokePermissionsOperation, builder);
            }
            else
            {
                base.Generate(operation, model, builder);
            }
        }

        private static void GenerateConfigureDatabase(ConfigureDatabaseMigrationOperation operation, MigrationCommandListBuilder builder)
        {
            if (operation != null)
                builder.AppendLines(StreamUtil.GetManifestResourceString("ConfigureDatabase.sql"))
                       .EndCommand(suppressTransaction: true);
        }

        private static void GenerateGrantPermission(GrantPermissionsMigrationOperation operation, MigrationCommandListBuilder builder)
        {
            if (operation != null)
                builder.AppendLines(StreamUtil.GetManifestResourceString("GrantPermissions.sql"))
                       .EndCommand(suppressTransaction: true);
        }

        private static void GenerateRevokePermissions(RevokePermissionsMigrationOperation operation, MigrationCommandListBuilder builder)
        {
            if (operation != null)
                builder.AppendLines(StreamUtil.GetManifestResourceString("RevokePermissions.sql"))
                       .EndCommand(suppressTransaction: true);
        }
    }
}
