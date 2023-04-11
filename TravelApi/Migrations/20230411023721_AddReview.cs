using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApi.Migrations
{
    public partial class AddReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Destination");

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Author = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Review_DestinationId",
                table: "Review",
                column: "DestinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Destination",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 1,
                column: "Review",
                value: "");

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 2,
                column: "Review",
                value: "");

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 3,
                column: "Review",
                value: "");

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 4,
                column: "Review",
                value: "");

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "DestinationId",
                keyValue: 5,
                column: "Review",
                value: "");
        }
    }
}
