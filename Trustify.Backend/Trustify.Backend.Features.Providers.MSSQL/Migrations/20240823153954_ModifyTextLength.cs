using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trustify.Backend.Features.Providers.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTextLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "TextualContents",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2047)",
                oldMaxLength: 2047);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "TextualContents",
                type: "nvarchar(2047)",
                maxLength: 2047,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10000);
        }
    }
}
