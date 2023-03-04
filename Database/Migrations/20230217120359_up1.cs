using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class up1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StopId",
                table: "Stops",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RideId",
                table: "Rides",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Locations",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stops",
                newName: "StopId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rides",
                newName: "RideId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Locations",
                newName: "id");
        }
    }
}
