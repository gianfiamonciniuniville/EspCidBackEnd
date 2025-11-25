using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspCid.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Report",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Report",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Report",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { -26.304400000000001, -48.847700000000003 });

            migrationBuilder.UpdateData(
                table: "Report",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { -26.300000000000001, -48.850000000000001 });

            migrationBuilder.UpdateData(
                table: "Report",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { -26.309999999999999, -48.840000000000003 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Report");
        }
    }
}
