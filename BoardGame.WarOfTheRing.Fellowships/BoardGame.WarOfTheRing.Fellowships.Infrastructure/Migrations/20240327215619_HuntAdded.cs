using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HuntAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveHunt_State_Value",
                table: "Huntings",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveHunt_State_Value",
                table: "Huntings");
        }
    }
}
