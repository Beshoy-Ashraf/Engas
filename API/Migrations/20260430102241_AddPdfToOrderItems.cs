using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddPdfToOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PdfData",
                table: "OrderItems",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfFileName",
                table: "OrderItems",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfData",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PdfFileName",
                table: "OrderItems");
        }
    }
}
