using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValorantAPIApp.Migrations
{
    public partial class AddedRelationTeamAndAgentsAddedTeamSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "maxTeamSize",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AgentTeam",
                columns: table => new
                {
                    AgentsUuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTeam", x => new { x.AgentsUuid, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_AgentTeam_Agents_AgentsUuid",
                        column: x => x.AgentsUuid,
                        principalTable: "Agents",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentTeam_TeamsId",
                table: "AgentTeam",
                column: "TeamsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentTeam");

            migrationBuilder.DropColumn(
                name: "maxTeamSize",
                table: "Teams");
        }
    }
}
