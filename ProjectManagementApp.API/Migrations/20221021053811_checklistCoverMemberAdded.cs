using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class checklistCoverMemberAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Cards",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cards",
                newName: "CardId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Boards",
                newName: "BoardId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Blocks",
                newName: "BlockId");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAt",
                table: "Cards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChecklistId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Cards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoverId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Cards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelId",
                table: "Cards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Boards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Boards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Blocks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Blocks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cover",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Green = table.Column<int>(type: "int", nullable: false),
                    Yellow = table.Column<int>(type: "int", nullable: false),
                    Orange = table.Column<int>(type: "int", nullable: false),
                    Red = table.Column<int>(type: "int", nullable: false),
                    Purple = table.Column<int>(type: "int", nullable: false),
                    Blue = table.Column<int>(type: "int", nullable: false),
                    Turquoise = table.Column<int>(type: "int", nullable: false),
                    SeaGrass = table.Column<int>(type: "int", nullable: false),
                    Pink = table.Column<int>(type: "int", nullable: false),
                    Black = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cover", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId");
                });

            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklist_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ChecklistId",
                table: "Cards",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CoverId",
                table: "Cards",
                column: "CoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_LabelId",
                table: "Cards",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_MemberId",
                table: "Checklist",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CardId",
                table: "Member",
                column: "CardId");

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
                name: "FK_Cards_Label_LabelId",
                table: "Cards",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Checklist_ChecklistId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Cover_CoverId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Label_LabelId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropTable(
                name: "Cover");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Cards_ChecklistId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CoverId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_LabelId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "AssignedAt",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Blocks");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Cards",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Cards",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Boards",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BlockId",
                table: "Blocks",
                newName: "Id");
        }
    }
}
