using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValorantAPIApp.Migrations
{
    public partial class AddedRewardMissionAndRelationWithTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MissionUuid",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Reward",
                table: "Missions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_MissionUuid",
                table: "Teams",
                column: "MissionUuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Missions_MissionUuid",
                table: "Teams",
                column: "MissionUuid",
                principalTable: "Missions",
                principalColumn: "Uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Missions_MissionUuid",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_MissionUuid",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MissionUuid",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Reward",
                table: "Missions");
        }
    }
}
