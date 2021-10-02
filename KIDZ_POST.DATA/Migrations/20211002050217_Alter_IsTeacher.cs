using Microsoft.EntityFrameworkCore.Migrations;

namespace KIDZ_POST.DATA.Migrations
{
    public partial class Alter_IsTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTeacher",
                schema: "Security",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTeacher",
                schema: "Security",
                table: "User");
        }
    }
}
