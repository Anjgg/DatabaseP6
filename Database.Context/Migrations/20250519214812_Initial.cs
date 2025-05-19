using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Context.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemesExploitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemesExploitation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumVersion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produit_SystemeExploitation_Versions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduitId = table.Column<int>(type: "int", nullable: false),
                    SystemeExploitationId = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit_SystemeExploitation_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produit_SystemeExploitation_Versions_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produit_SystemeExploitation_Versions_SystemesExploitation_SystemeExploitationId",
                        column: x => x.SystemeExploitationId,
                        principalTable: "SystemesExploitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produit_SystemeExploitation_Versions_Versions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ClosingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ProduitSystemeExploitationVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Produit_SystemeExploitation_Versions_ProduitSystemeExploitationVersionId",
                        column: x => x.ProduitSystemeExploitationVersionId,
                        principalTable: "Produit_SystemeExploitation_Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produit_SystemeExploitation_Versions_ProduitId",
                table: "Produit_SystemeExploitation_Versions",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_SystemeExploitation_Versions_SystemeExploitationId",
                table: "Produit_SystemeExploitation_Versions",
                column: "SystemeExploitationId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_SystemeExploitation_Versions_VersionId",
                table: "Produit_SystemeExploitation_Versions",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProduitSystemeExploitationVersionId",
                table: "Tickets",
                column: "ProduitSystemeExploitationVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Produit_SystemeExploitation_Versions");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "SystemesExploitation");

            migrationBuilder.DropTable(
                name: "Versions");
        }
    }
}
