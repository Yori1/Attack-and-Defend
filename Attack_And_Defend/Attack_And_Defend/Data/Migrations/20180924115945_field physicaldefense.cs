using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class fieldphysicaldefense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_AspNetUsers_ApplicationUserId",
                table: "Parties");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Parties",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PhysicalDefense",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_AspNetUsers_ApplicationUserId",
                table: "Parties",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_AspNetUsers_ApplicationUserId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "PhysicalDefense",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Parties",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_AspNetUsers_ApplicationUserId",
                table: "Parties",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
