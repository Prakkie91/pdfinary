using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HtmlToPdf.Data.Migrations
{
    public partial class UpdateTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumberOfRequests",
                table: "SubscriptionTypes",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ApiRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ApiRequests");

            migrationBuilder.AlterColumn<double>(
                name: "NumberOfRequests",
                table: "SubscriptionTypes",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
