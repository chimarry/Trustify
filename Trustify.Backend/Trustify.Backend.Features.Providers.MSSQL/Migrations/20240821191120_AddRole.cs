using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trustify.Backend.Features.Providers.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfidential",
                table: "Sections");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "TextualContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SectionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_SectionId",
                table: "Roles",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "TextualContents");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfidential",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
