using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class changeCardEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowItems_Cards_CardId",
                table: "WorkFlowItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowItems_CardId",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "WorkFlowItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "WorkFlowItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowItems_CardId",
                table: "WorkFlowItems",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowItems_Cards_CardId",
                table: "WorkFlowItems",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId");
        }
    }
}
