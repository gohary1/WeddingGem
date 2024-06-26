using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingGem.Data.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiddingService_Biddings_BiddingId",
                table: "BiddingService");

            migrationBuilder.DropForeignKey(
                name: "FK_BiddingService_Services_ServiceId",
                table: "BiddingService");

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingService_Biddings_BiddingId",
                table: "BiddingService",
                column: "BiddingId",
                principalTable: "Biddings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingService_Services_ServiceId",
                table: "BiddingService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiddingService_Biddings_BiddingId",
                table: "BiddingService");

            migrationBuilder.DropForeignKey(
                name: "FK_BiddingService_Services_ServiceId",
                table: "BiddingService");

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingService_Biddings_BiddingId",
                table: "BiddingService",
                column: "BiddingId",
                principalTable: "Biddings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingService_Services_ServiceId",
                table: "BiddingService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
