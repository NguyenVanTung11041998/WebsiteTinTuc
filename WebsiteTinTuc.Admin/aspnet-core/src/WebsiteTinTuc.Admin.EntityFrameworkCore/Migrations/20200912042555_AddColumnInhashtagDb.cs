using Microsoft.EntityFrameworkCore.Migrations;

namespace WebsiteTinTuc.Admin.Migrations
{
    public partial class AddColumnInhashtagDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHot",
                table: "Hashtags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_IsHot",
                table: "Hashtags",
                column: "IsHot"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHot",
                table: "Hashtags");
        }
    }
}
