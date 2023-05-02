using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateCardProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Cards_CardId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_CardId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Members");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Members",
                type: "int",
                nullable: true);

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
    }
}
