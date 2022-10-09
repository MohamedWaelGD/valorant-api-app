using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValorantAPIApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DurationInMin = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    MaxTeams = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "WeaponSkins",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponSkins", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentLoadouts",
                columns: table => new
                {
                    AgentUuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    WeaponUuid = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentLoadouts", x => new { x.PlayerId, x.AgentUuid });
                    table.ForeignKey(
                        name: "FK_AgentLoadouts_Agents_AgentUuid",
                        column: x => x.AgentUuid,
                        principalTable: "Agents",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentLoadouts_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentLoadouts_Weapons_WeaponUuid",
                        column: x => x.WeaponUuid,
                        principalTable: "Weapons",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerWeapons",
                columns: table => new
                {
                    WeaponUuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    WeaponSkinUuid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerWeapons", x => new { x.PlayerId, x.WeaponUuid });
                    table.ForeignKey(
                        name: "FK_PlayerWeapons_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerWeapons_Weapons_WeaponUuid",
                        column: x => x.WeaponUuid,
                        principalTable: "Weapons",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerWeapons_WeaponSkins_WeaponSkinUuid",
                        column: x => x.WeaponSkinUuid,
                        principalTable: "WeaponSkins",
                        principalColumn: "Uuid");
                });

            migrationBuilder.CreateTable(
                name: "PlayerWeaponSkin",
                columns: table => new
                {
                    EquippedWeaponSkinsUuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerWeaponSkin", x => new { x.EquippedWeaponSkinsUuid, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_PlayerWeaponSkin_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerWeaponSkin_WeaponSkins_EquippedWeaponSkinsUuid",
                        column: x => x.EquippedWeaponSkinsUuid,
                        principalTable: "WeaponSkins",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentLoadouts_AgentUuid",
                table: "AgentLoadouts",
                column: "AgentUuid");

            migrationBuilder.CreateIndex(
                name: "IX_AgentLoadouts_WeaponUuid",
                table: "AgentLoadouts",
                column: "WeaponUuid");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerWeapons_WeaponSkinUuid",
                table: "PlayerWeapons",
                column: "WeaponSkinUuid");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerWeapons_WeaponUuid",
                table: "PlayerWeapons",
                column: "WeaponUuid");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerWeaponSkin_PlayersId",
                table: "PlayerWeaponSkin",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PlayerId",
                table: "Teams",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentLoadouts");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "PlayerWeapons");

            migrationBuilder.DropTable(
                name: "PlayerWeaponSkin");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "WeaponSkins");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
