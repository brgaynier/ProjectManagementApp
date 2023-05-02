using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class quickUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Checklists_ChecklistId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_ChecklistId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "WorkFLowItemId",
                table: "WorkFlowItems",
                newName: "WorkFlowItemId");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "WorkFlowItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "WorkFlowItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WorkFlowItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WorkFlowItemId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Boards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowItems_CardId",
                table: "WorkFlowItems",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_WorkFlowItemId",
                table: "Members",
                column: "WorkFlowItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_WorkFlowItems_WorkFlowItemId",
                table: "Members",
                column: "WorkFlowItemId",
                principalTable: "WorkFlowItems",
                principalColumn: "WorkFlowItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowItems_Cards_CardId",
                table: "WorkFlowItems",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_WorkFlowItems_WorkFlowItemId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowItems_Cards_CardId",
                table: "WorkFlowItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowItems_CardId",
                table: "WorkFlowItems");

            migrationBuilder.DropIndex(
                name: "IX_Members_WorkFlowItemId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "WorkFlowItemId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "WorkFlowItemId",
                table: "WorkFlowItems",
                newName: "WorkFLowItemId");

            migrationBuilder.AddColumn<int>(
                name: "ChecklistId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ChecklistId",
                table: "Cards",
                column: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Checklists_ChecklistId",
                table: "Cards",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "ChecklistId");
        }
    }
}
