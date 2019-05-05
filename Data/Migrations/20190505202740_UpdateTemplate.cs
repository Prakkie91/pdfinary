using Microsoft.EntityFrameworkCore.Migrations;

namespace Pdfinary.Data.Migrations
{
    public partial class UpdateTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmulateScreenMedia",
                table: "Templates",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLandscape",
                table: "Templates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PageFormat",
                table: "Templates",
                nullable: false,
                defaultValue: "A4");

            migrationBuilder.AddColumn<double>(
                name: "Scale",
                table: "Templates",
                nullable: false,
                defaultValue: 0.7);

            migrationBuilder.AddColumn<bool>(
                name: "ScrollPage",
                table: "Templates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmulateScreenMedia",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "IsLandscape",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "PageFormat",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Scale",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ScrollPage",
                table: "Templates");
        }
    }
}
