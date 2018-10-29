using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class party : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobNumber",
                table: "Characters");

            migrationBuilder.AddColumn<bool>(
                name: "AttacksPhysical",
                table: "Characters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanUseSkill",
                table: "Characters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Characters",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttacksPhysical",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "CanUseSkill",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "JobNumber",
                table: "Characters",
                nullable: false,
                defaultValue: 0);
        }
    }
}
