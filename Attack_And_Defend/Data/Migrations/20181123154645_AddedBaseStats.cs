using Microsoft.EntityFrameworkCore.Migrations;

namespace Attack_And_Defend.Data.Migrations
{
    public partial class AddedBaseStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "AttacksPhysical",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "CanUseSkill",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "RemainingHealth",
                table: "Characters",
                newName: "BasePhysicalDefense");

            migrationBuilder.RenameColumn(
                name: "PhysicalDefense",
                table: "Characters",
                newName: "BaseMaximumHealth");

            migrationBuilder.RenameColumn(
                name: "MaximumHealth",
                table: "Characters",
                newName: "BaseMagicDefense");

            migrationBuilder.RenameColumn(
                name: "MagicDefense",
                table: "Characters",
                newName: "BaseAttack");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasePhysicalDefense",
                table: "Characters",
                newName: "RemainingHealth");

            migrationBuilder.RenameColumn(
                name: "BaseMaximumHealth",
                table: "Characters",
                newName: "PhysicalDefense");

            migrationBuilder.RenameColumn(
                name: "BaseMagicDefense",
                table: "Characters",
                newName: "MaximumHealth");

            migrationBuilder.RenameColumn(
                name: "BaseAttack",
                table: "Characters",
                newName: "MagicDefense");

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
