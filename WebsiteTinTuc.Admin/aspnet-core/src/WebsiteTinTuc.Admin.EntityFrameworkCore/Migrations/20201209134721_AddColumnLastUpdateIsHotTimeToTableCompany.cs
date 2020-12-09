using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebsiteTinTuc.Admin.Migrations
{
    public partial class AddColumnLastUpdateIsHotTimeToTableCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateIsHotTime",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdateIsHotTime",
                table: "Companies");
        }
    }
}
