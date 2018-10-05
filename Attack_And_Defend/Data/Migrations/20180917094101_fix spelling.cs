using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class fixspelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttaksPhysical",
                table: "Characters",
                newName: "AttacksPhysical");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttacksPhysical",
                table: "Characters",
                newName: "AttaksPhysical");
        }
    }
}
