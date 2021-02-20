using Microsoft.EntityFrameworkCore.Migrations;

namespace CostApp.Migrations
{
    public partial class AppUserEditNoteLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "AspNetUsers",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3000,
                oldNullable: true);
        }
    }
}
