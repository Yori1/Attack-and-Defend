using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class NextCharacterAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatResults_AspNetUsers_UserId",
                table: "CombatResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CombatResults",
                table: "CombatResults");

            migrationBuilder.RenameTable(
                name: "CombatResults",
                newName: "CombatResult");

            migrationBuilder.RenameIndex(
                name: "IX_CombatResults_UserId",
                table: "CombatResult",
                newName: "IX_CombatResult_UserId");

            migrationBuilder.AddColumn<int>(
                name: "IndexCharacterRotatedIn",
                table: "Parties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NextCharacterInPartyId",
                table: "Characters",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CombatResult",
                table: "CombatResult",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_NextCharacterInPartyId",
                table: "Characters",
                column: "NextCharacterInPartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Characters_NextCharacterInPartyId",
                table: "Characters",
                column: "NextCharacterInPartyId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CombatResult_AspNetUsers_UserId",
                table: "CombatResult",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Characters_NextCharacterInPartyId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_CombatResult_AspNetUsers_UserId",
                table: "CombatResult");

            migrationBuilder.DropIndex(
                name: "IX_Characters_NextCharacterInPartyId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CombatResult",
                table: "CombatResult");

            migrationBuilder.DropColumn(
                name: "IndexCharacterRotatedIn",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "NextCharacterInPartyId",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "CombatResult",
                newName: "CombatResults");

            migrationBuilder.RenameIndex(
                name: "IX_CombatResult_UserId",
                table: "CombatResults",
                newName: "IX_CombatResults_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CombatResults",
                table: "CombatResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatResults_AspNetUsers_UserId",
                table: "CombatResults",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
