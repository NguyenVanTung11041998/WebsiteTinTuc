using Microsoft.EntityFrameworkCore.Migrations;

namespace WebsiteTinTuc.Admin.Migrations
{
    public partial class AddColumnInCompanyDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHot",
                table: "Companies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHot",
                table: "Companies");
        }
    }
}
