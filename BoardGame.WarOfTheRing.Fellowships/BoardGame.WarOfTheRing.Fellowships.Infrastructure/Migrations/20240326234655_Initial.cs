using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fellowships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    HuntingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CorruptionCounter_Value = table.Column<int>(type: "integer", nullable: false),
                    ProgressCounter_IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    ProgressCounter_Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fellowships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Huntings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FellowshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    HuntBox_NumberOfCharacterResultDice = table.Column<int>(type: "integer", nullable: false),
                    HuntBox_NumberOfEyeResultDice = table.Column<int>(type: "integer", nullable: false),
                    HuntPool = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huntings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huntings_Fellowships_FellowshipId",
                        column: x => x.FellowshipId,
                        principalTable: "Fellowships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fellowships_GameId",
                table: "Fellowships",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Huntings_FellowshipId",
                table: "Huntings",
                column: "FellowshipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Huntings_GameId",
                table: "Huntings",
                column: "GameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Huntings");

            migrationBuilder.DropTable(
                name: "Fellowships");
        }
    }
}
