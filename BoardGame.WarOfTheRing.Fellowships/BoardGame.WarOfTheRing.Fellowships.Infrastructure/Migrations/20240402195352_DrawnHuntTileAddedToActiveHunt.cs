using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DrawnHuntTileAddedToActiveHunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveHunt_DrawnHuntTile_HasDieIcon",
                table: "Huntings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveHunt_DrawnHuntTile_HasEyeIcon",
                table: "Huntings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveHunt_DrawnHuntTile_HasRevealIcon",
                table: "Huntings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveHunt_DrawnHuntTile_HasStopIcon",
                table: "Huntings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ActiveHunt_DrawnHuntTile_HuntDamage",
                table: "Huntings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveHunt_DrawnHuntTile_HasDieIcon",
                table: "Huntings");

            migrationBuilder.DropColumn(
                name: "ActiveHunt_DrawnHuntTile_HasEyeIcon",
                table: "Huntings");

            migrationBuilder.DropColumn(
                name: "ActiveHunt_DrawnHuntTile_HasRevealIcon",
                table: "Huntings");

            migrationBuilder.DropColumn(
                name: "ActiveHunt_DrawnHuntTile_HasStopIcon",
                table: "Huntings");

            migrationBuilder.DropColumn(
                name: "ActiveHunt_DrawnHuntTile_HuntDamage",
                table: "Huntings");
        }
    }
}
