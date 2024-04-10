using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsAtWar = table.Column<bool>(type: "boolean", nullable: false),
                    MapId = table.Column<Guid>(type: "uuid", nullable: false),
                    BelongsTo_Name = table.Column<string>(type: "text", nullable: true),
                    Reinforcements = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nation_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    InBorderOfId = table.Column<Guid>(type: "uuid", nullable: true),
                    MapId = table.Column<Guid>(type: "uuid", nullable: false),
                    Terrain_HasFortification = table.Column<bool>(type: "boolean", nullable: false),
                    Terrain_IsEmpty = table.Column<bool>(type: "boolean", nullable: false),
                    Terrain_Settlement_Type = table.Column<string>(type: "text", nullable: true),
                    Terrain_Settlement_VictoryPoint = table.Column<int>(type: "integer", nullable: false),
                    Terrain_Settlement_ControlledBy_Name = table.Column<string>(type: "text", nullable: true),
                    Army = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_Nation_InBorderOfId",
                        column: x => x.InBorderOfId,
                        principalTable: "Nation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegionNeighborRegions",
                columns: table => new
                {
                    NeighborRegionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionNeighborRegions", x => new { x.NeighborRegionsId, x.RegionId });
                    table.ForeignKey(
                        name: "FK_RegionNeighborRegions_Region_NeighborRegionsId",
                        column: x => x.NeighborRegionsId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionNeighborRegions_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nation_MapId",
                table: "Nation",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_InBorderOfId",
                table: "Region",
                column: "InBorderOfId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_MapId",
                table: "Region",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionNeighborRegions_RegionId",
                table: "RegionNeighborRegions",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionNeighborRegions");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Nation");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
