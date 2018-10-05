using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class addedjobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttacksPhysical",
                table: "Characters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AttacksPhysical",
                table: "Characters",
                nullable: false,
                defaultValue: false);
        }
    }
}
