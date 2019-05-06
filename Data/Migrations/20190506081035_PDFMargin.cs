using Microsoft.EntityFrameworkCore.Migrations;

namespace Pdfinary.Data.Migrations
{
    public partial class PDFMargin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarginBottom",
                table: "Templates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarginLeft",
                table: "Templates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarginRight",
                table: "Templates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarginTop",
                table: "Templates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarginBottom",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "MarginLeft",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "MarginRight",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "MarginTop",
                table: "Templates");
        }
    }
}
