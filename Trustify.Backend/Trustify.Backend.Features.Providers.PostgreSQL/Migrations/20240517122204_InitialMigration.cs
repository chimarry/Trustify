﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Trustify.Backend.Features.Providers.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageContents",
                columns: table => new
                {
                    ImageContentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Size = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageContents", x => x.ImageContentId);
                });

            migrationBuilder.CreateTable(
                name: "LargeContents",
                columns: table => new
                {
                    LargeContentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Size = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LargeContents", x => x.LargeContentId);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1023)", maxLength: 1023, nullable: true),
                    IsConfidential = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "TextualContents",
                columns: table => new
                {
                    TextualContentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "character varying(2047)", maxLength: 2047, nullable: false),
                    Lenght = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextualContents", x => x.TextualContentId);
                });

            migrationBuilder.CreateTable(
                name: "ImageContentSection",
                columns: table => new
                {
                    ImageContentsImageContentId = table.Column<long>(type: "bigint", nullable: false),
                    SectionsSectionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageContentSection", x => new { x.ImageContentsImageContentId, x.SectionsSectionId });
                    table.ForeignKey(
                        name: "FK_ImageContentSection_ImageContents_ImageContentsImageContent~",
                        column: x => x.ImageContentsImageContentId,
                        principalTable: "ImageContents",
                        principalColumn: "ImageContentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageContentSection_Sections_SectionsSectionId",
                        column: x => x.SectionsSectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionTextualContent",
                columns: table => new
                {
                    SectionsSectionId = table.Column<long>(type: "bigint", nullable: false),
                    TextualContentsTextualContentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionTextualContent", x => new { x.SectionsSectionId, x.TextualContentsTextualContentId });
                    table.ForeignKey(
                        name: "FK_SectionTextualContent_Sections_SectionsSectionId",
                        column: x => x.SectionsSectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionTextualContent_TextualContents_TextualContentsTextua~",
                        column: x => x.TextualContentsTextualContentId,
                        principalTable: "TextualContents",
                        principalColumn: "TextualContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageContentSection_SectionsSectionId",
                table: "ImageContentSection",
                column: "SectionsSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionTextualContent_TextualContentsTextualContentId",
                table: "SectionTextualContent",
                column: "TextualContentsTextualContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageContentSection");

            migrationBuilder.DropTable(
                name: "LargeContents");

            migrationBuilder.DropTable(
                name: "SectionTextualContent");

            migrationBuilder.DropTable(
                name: "ImageContents");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "TextualContents");
        }
    }
}
