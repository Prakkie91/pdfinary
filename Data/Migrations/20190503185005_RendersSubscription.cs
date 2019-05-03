using Microsoft.EntityFrameworkCore.Migrations;

namespace Pdfinary.Data.Migrations
{
    public partial class RendersSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Renders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Renders_SubscriptionId",
                table: "Renders",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Renders_Subscriptions_SubscriptionId",
                table: "Renders",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renders_Subscriptions_SubscriptionId",
                table: "Renders");

            migrationBuilder.DropIndex(
                name: "IX_Renders_SubscriptionId",
                table: "Renders");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Renders");
        }
    }
}
