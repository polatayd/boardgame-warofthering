using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeadersAddedToArmyValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nation_Leaders",
                columns: table => new
                {
                    ArmyNationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NationName = table.Column<string>(type: "text", nullable: true),
                    IsNazgul = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nation_Leaders", x => new { x.ArmyNationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Nation_Leaders_Nation_ArmyNationId",
                        column: x => x.ArmyNationId,
                        principalTable: "Nation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region_Leaders",
                columns: table => new
                {
                    ArmyRegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NationName = table.Column<string>(type: "text", nullable: true),
                    IsNazgul = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region_Leaders", x => new { x.ArmyRegionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Region_Leaders_Region_ArmyRegionId",
                        column: x => x.ArmyRegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nation_Leaders");

            migrationBuilder.DropTable(
                name: "Region_Leaders");
        }
    }
}
