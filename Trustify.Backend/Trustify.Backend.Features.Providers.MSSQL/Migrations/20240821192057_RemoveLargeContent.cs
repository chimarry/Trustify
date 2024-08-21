using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trustify.Backend.Features.Providers.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLargeContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Sections_SectionId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "LargeContents");

            migrationBuilder.DropIndex(
                name: "IX_Roles_SectionId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "RoleSection",
                columns: table => new
                {
                    RolesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionsSectionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSection", x => new { x.RolesId, x.SectionsSectionId });
                    table.ForeignKey(
                        name: "FK_RoleSection_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleSection_Sections_SectionsSectionId",
                        column: x => x.SectionsSectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleSection_SectionsSectionId",
                table: "RoleSection",
                column: "SectionsSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleSection");

            migrationBuilder.AddColumn<long>(
                name: "SectionId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LargeContents",
                columns: table => new
                {
                    LargeContentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LargeContents", x => x.LargeContentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_SectionId",
                table: "Roles",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Sections_SectionId",
                table: "Roles",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId");
        }
    }
}
