using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspCid.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhotoReportContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Photo");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Photo",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Photo");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
