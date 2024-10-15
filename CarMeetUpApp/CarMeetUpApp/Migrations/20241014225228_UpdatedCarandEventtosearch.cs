using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMeetUpApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCarandEventtosearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CarId",
                table: "Events",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Cars_CarId",
                table: "Events",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Cars_CarId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CarId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Events");
        }
    }
}
