using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InfoDriverRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DriverId",
                table: "Requests",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_DriverId",
                table: "Requests",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_DriverId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_DriverId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Requests");
        }
    }
}
