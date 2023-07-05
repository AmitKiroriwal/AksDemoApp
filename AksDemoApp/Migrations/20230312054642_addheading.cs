using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AksDemoApp.Migrations
{
    public partial class addheading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfHeading",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfHeading",
                table: "Products");
        }
    }
}
