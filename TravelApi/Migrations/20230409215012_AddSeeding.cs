using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApi.Migrations
{
    public partial class AddSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Destination",
                columns: new[] { "DestinationId", "City", "Country", "Rating", "Review" },
                values: new object[,]
                {
                    { 1, "Tokyo", "Japan", 5, "" },
                    { 2, "Mumbai", "India", 5, "" },
                    { 3, "Paris", "France", 5, "" },
                    { 4, "London", "England", 5, "" },
                    { 5, "Sydney", "Australia", 5, "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 5);
        }
    }
}
