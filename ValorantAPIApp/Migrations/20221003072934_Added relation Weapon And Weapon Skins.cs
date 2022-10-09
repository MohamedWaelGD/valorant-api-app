using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValorantAPIApp.Migrations
{
    public partial class AddedrelationWeaponAndWeaponSkins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeaponUuid",
                table: "WeaponSkins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeaponSkins_WeaponUuid",
                table: "WeaponSkins",
                column: "WeaponUuid");

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponSkins_Weapons_WeaponUuid",
                table: "WeaponSkins",
                column: "WeaponUuid",
                principalTable: "Weapons",
                principalColumn: "Uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeaponSkins_Weapons_WeaponUuid",
                table: "WeaponSkins");

            migrationBuilder.DropIndex(
                name: "IX_WeaponSkins_WeaponUuid",
                table: "WeaponSkins");

            migrationBuilder.DropColumn(
                name: "WeaponUuid",
                table: "WeaponSkins");
        }
    }
}
