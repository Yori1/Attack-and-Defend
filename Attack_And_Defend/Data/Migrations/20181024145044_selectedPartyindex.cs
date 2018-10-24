using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class selectedPartyindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fainted",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "SelectedPartyIndex",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedPartyIndex",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Fainted",
                table: "Characters",
                nullable: false,
                defaultValue: false);
        }
    }
}
