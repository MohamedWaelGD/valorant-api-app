using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValorantAPIApp.Migrations
{
    public partial class AddedTimeForTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MissionEndTime",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionEndTime",
                table: "Teams");
        }
    }
}
