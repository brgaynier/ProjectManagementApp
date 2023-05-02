using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedWorkFlowItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Checklist_ChecklistId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Cover_CoverId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Checklist_Member_MemberId",
                table: "Checklist");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Cards_CardId",
                table: "Member");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cover",
                table: "Cover");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checklist",
                table: "Checklist");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameTable(
                name: "Cover",
                newName: "Covers");

            migrationBuilder.RenameTable(
                name: "Checklist",
                newName: "Checklists");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Members",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_CardId",
                table: "Members",
                newName: "IX_Members_CardId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Covers",
                newName: "CoverId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Checklists",
                newName: "ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_Checklist_MemberId",
                table: "Checklists",
                newName: "IX_Checklists_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Covers",
                table: "Covers",
                column: "CoverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checklists",
                table: "Checklists",
                column: "ChecklistId");

            migrationBuilder.CreateTable(
                name: "WorkFlowItems",
                columns: table => new
                {
                    WorkFLowItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowItems", x => x.WorkFLowItemId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Checklists_ChecklistId",
                table: "Cards",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Covers_CoverId",
                table: "Cards",
                column: "CoverId",
                principalTable: "Covers",
                principalColumn: "CoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_Members_MemberId",
                table: "Checklists",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId");

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
                name: "FK_Cards_Checklists_ChecklistId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Covers_CoverId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_Members_MemberId",
                table: "Checklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Cards_CardId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "WorkFlowItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Covers",
                table: "Covers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checklists",
                table: "Checklists");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameTable(
                name: "Covers",
                newName: "Cover");

            migrationBuilder.RenameTable(
                name: "Checklists",
                newName: "Checklist");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Member",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Members_CardId",
                table: "Member",
                newName: "IX_Member_CardId");

            migrationBuilder.RenameColumn(
                name: "CoverId",
                table: "Cover",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ChecklistId",
                table: "Checklist",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Checklists_MemberId",
                table: "Checklist",
                newName: "IX_Checklist_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cover",
                table: "Cover",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checklist",
                table: "Checklist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Checklist_ChecklistId",
                table: "Cards",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Cover_CoverId",
                table: "Cards",
                column: "CoverId",
                principalTable: "Cover",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklist_Member_MemberId",
                table: "Checklist",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Cards_CardId",
                table: "Member",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId");
        }
    }
}
