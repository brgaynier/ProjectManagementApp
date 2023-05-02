using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkFlowId",
                table: "WorkFlowItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChecklistItemId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Checklists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkFlow",
                columns: table => new
                {
                    WorkFlowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkFlowName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlow", x => x.WorkFlowId);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItem",
                columns: table => new
                {
                    ChecklistItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChecklistId = table.Column<int>(type: "int", nullable: true),
                    WorkFlowId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItem", x => x.ChecklistItemId);
                    table.ForeignKey(
                        name: "FK_ChecklistItem_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "ChecklistId");
                    table.ForeignKey(
                        name: "FK_ChecklistItem_WorkFlow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "WorkFlow",
                        principalColumn: "WorkFlowId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowItems_WorkFlowId",
                table: "WorkFlowItems",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ChecklistItemId",
                table: "Members",
                column: "ChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_CardId",
                table: "Checklists",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_ChecklistId",
                table: "ChecklistItem",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_WorkFlowId",
                table: "ChecklistItem",
                column: "WorkFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_Cards_CardId",
                table: "Checklists",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_ChecklistItem_ChecklistItemId",
                table: "Members",
                column: "ChecklistItemId",
                principalTable: "ChecklistItem",
                principalColumn: "ChecklistItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowItems_WorkFlow_WorkFlowId",
                table: "WorkFlowItems",
                column: "WorkFlowId",
                principalTable: "WorkFlow",
                principalColumn: "WorkFlowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_Cards_CardId",
                table: "Checklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_ChecklistItem_ChecklistItemId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowItems_WorkFlow_WorkFlowId",
                table: "WorkFlowItems");

            migrationBuilder.DropTable(
                name: "ChecklistItem");

            migrationBuilder.DropTable(
                name: "WorkFlow");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowItems_WorkFlowId",
                table: "WorkFlowItems");

            migrationBuilder.DropIndex(
                name: "IX_Members_ChecklistItemId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_CardId",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "WorkFlowId",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "ChecklistItemId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Checklists");
        }
    }
}
