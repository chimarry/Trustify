using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Trustify.Backend.FeaturesCore.Database.MigrationOperations;

namespace Trustify.Backend.FeaturesCore.Database.Util
{
    public static class MigrationBuilderExtensionMethods
    {
        /// <summary>
        /// Adds migration operation that will configure database after creation. 
        /// </summary>
        /// <param name="migrationBuilder">Migration builder object</param>
        /// <returns></returns>
        public static OperationBuilder<ConfigureDatabaseMigrationOperation> ConfigureDatabase(
            this MigrationBuilder migrationBuilder)
        {
            var operation = new ConfigureDatabaseMigrationOperation();
            migrationBuilder.Operations.Add(operation);

            return new OperationBuilder<ConfigureDatabaseMigrationOperation>(operation);
        }
    }
}
