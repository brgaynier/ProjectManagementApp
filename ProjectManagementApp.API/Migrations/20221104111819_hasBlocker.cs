using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class hasBlocker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Members_MemberId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_MemberId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Cards");

            migrationBuilder.AddColumn<bool>(
                name: "HasBlocker",
                table: "WorkFlowItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBlocker",
                table: "Checklists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBlocker",
                table: "ChecklistItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBlocker",
                table: "Cards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBlocker",
                table: "Blocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Members_CardId",
                table: "Members",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Cards_CardId",
                table: "Members",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Cards_CardId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_CardId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "HasBlocker",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "HasBlocker",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "HasBlocker",
                table: "ChecklistItems");

            migrationBuilder.DropColumn(
                name: "HasBlocker",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "HasBlocker",
                table: "Blocks");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_MemberId",
                table: "Cards",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Members_MemberId",
                table: "Cards",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId");
        }
    }
}
