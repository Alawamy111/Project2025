using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project2025.Migrations
{
    /// <inheritdoc />
    public partial class initialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    ownerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ownerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwners", x => x.ownerId);
                });

            migrationBuilder.CreateTable(
                name: "Chalets",
                columns: table => new
                {
                    ChaletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    area = table.Column<double>(type: "float", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyOwnerownerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chalets", x => x.ChaletId);
                    table.ForeignKey(
                        name: "FK_Chalets_PropertyOwners_PropertyOwnerownerId",
                        column: x => x.PropertyOwnerownerId,
                        principalTable: "PropertyOwners",
                        principalColumn: "ownerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chalets_PropertyOwnerownerId",
                table: "Chalets",
                column: "PropertyOwnerownerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chalets");

            migrationBuilder.DropTable(
                name: "PropertyOwners");
        }
    }
}
