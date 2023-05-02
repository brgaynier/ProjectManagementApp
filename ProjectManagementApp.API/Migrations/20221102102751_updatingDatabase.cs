using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class updatingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Checklists_ChecklistId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_WorkFlow_WorkFlowId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_ChecklistItem_ChecklistItemId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowItems_WorkFlow_WorkFlowId",
                table: "WorkFlowItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFlow",
                table: "WorkFlow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItem",
                table: "ChecklistItem");

            migrationBuilder.RenameTable(
                name: "WorkFlow",
                newName: "WorkFlows");

            migrationBuilder.RenameTable(
                name: "ChecklistItem",
                newName: "ChecklistItems");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItem_WorkFlowId",
                table: "ChecklistItems",
                newName: "IX_ChecklistItems_WorkFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItem_ChecklistId",
                table: "ChecklistItems",
                newName: "IX_ChecklistItems_ChecklistId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Boards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "WorkFlows",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFlows",
                table: "WorkFlows",
                column: "WorkFlowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItems",
                table: "ChecklistItems",
                column: "ChecklistItemId");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ChangeOrders",
                columns: table => new
                {
                    ChangeOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeOrderNumber = table.Column<int>(type: "int", nullable: false),
                    ChangeOrderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOwed = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeOrders", x => x.ChangeOrderId);
                    table.ForeignKey(
                        name: "FK_ChangeOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_CustomerId",
                table: "Boards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlows_CustomerId",
                table: "WorkFlows",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeOrders_CustomerId",
                table: "ChangeOrders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Customers_CustomerId",
                table: "Boards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_Checklists_ChecklistId",
                table: "ChecklistItems",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_WorkFlows_WorkFlowId",
                table: "ChecklistItems",
                column: "WorkFlowId",
                principalTable: "WorkFlows",
                principalColumn: "WorkFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_ChecklistItems_ChecklistItemId",
                table: "Members",
                column: "ChecklistItemId",
                principalTable: "ChecklistItems",
                principalColumn: "ChecklistItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowItems_WorkFlows_WorkFlowId",
                table: "WorkFlowItems",
                column: "WorkFlowId",
                principalTable: "WorkFlows",
                principalColumn: "WorkFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlows_Customers_CustomerId",
                table: "WorkFlows",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Customers_CustomerId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_Checklists_ChecklistId",
                table: "ChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_WorkFlows_WorkFlowId",
                table: "ChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_ChecklistItems_ChecklistItemId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowItems_WorkFlows_WorkFlowId",
                table: "WorkFlowItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlows_Customers_CustomerId",
                table: "WorkFlows");

            migrationBuilder.DropTable(
                name: "ChangeOrders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Boards_CustomerId",
                table: "Boards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFlows",
                table: "WorkFlows");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlows_CustomerId",
                table: "WorkFlows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItems",
                table: "ChecklistItems");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "WorkFlows");

            migrationBuilder.RenameTable(
                name: "WorkFlows",
                newName: "WorkFlow");

            migrationBuilder.RenameTable(
                name: "ChecklistItems",
                newName: "ChecklistItem");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItems_WorkFlowId",
                table: "ChecklistItem",
                newName: "IX_ChecklistItem_WorkFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItems_ChecklistId",
                table: "ChecklistItem",
                newName: "IX_ChecklistItem_ChecklistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFlow",
                table: "WorkFlow",
                column: "WorkFlowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItem",
                table: "ChecklistItem",
                column: "ChecklistItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Checklists_ChecklistId",
                table: "ChecklistItem",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_WorkFlow_WorkFlowId",
                table: "ChecklistItem",
                column: "WorkFlowId",
                principalTable: "WorkFlow",
                principalColumn: "WorkFlowId");

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
    }
}
