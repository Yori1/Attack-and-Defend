using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class added_party_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userID",
                table: "Parties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userID",
                table: "Parties");
        }
    }
}
