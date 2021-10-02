using Microsoft.EntityFrameworkCore.Migrations;

namespace KIDZ_POST.DATA.Migrations
{
    public partial class Add_TeacherId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                schema: "Security",
                table: "User",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                schema: "Security",
                table: "User");
        }
    }
}
