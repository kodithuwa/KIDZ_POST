using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KIDZ_POST.DATA.Migrations
{
    public partial class Create_MessageNMessageUser_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Media");

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_User_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMessage",
                schema: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    ViewedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessage_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Media",
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMessage_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_CreatedById",
                schema: "Media",
                table: "Message",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_MessageId",
                schema: "Media",
                table: "UserMessage",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_UserId",
                schema: "Media",
                table: "UserMessage",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMessage",
                schema: "Media");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "Media");
        }
    }
}
