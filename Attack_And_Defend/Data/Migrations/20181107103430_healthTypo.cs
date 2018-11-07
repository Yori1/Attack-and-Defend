using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class healthTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Health",
                table: "Characters",
                newName: "RemainingHealth");

            migrationBuilder.AddColumn<int>(
                name: "MaximumHealth",
                table: "Characters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumHealth",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "RemainingHealth",
                table: "Characters",
                newName: "Health");
        }
    }
}
