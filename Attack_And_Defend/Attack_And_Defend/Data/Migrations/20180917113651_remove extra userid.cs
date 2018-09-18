using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class removeextrauserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userID",
                table: "Parties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userID",
                table: "Parties",
                nullable: true);
        }
    }
}
