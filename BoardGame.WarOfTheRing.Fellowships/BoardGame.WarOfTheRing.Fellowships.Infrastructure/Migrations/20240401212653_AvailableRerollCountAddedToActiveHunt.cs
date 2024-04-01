using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AvailableRerollCountAddedToActiveHunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveHunt_AvailableReRollCount",
                table: "Huntings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveHunt_AvailableReRollCount",
                table: "Huntings");
        }
    }
}
