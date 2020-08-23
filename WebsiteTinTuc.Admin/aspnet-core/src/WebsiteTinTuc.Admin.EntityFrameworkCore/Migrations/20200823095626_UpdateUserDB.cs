using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebsiteTinTuc.Admin.Migrations
{
    public partial class UpdateUserDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_Agencies_AgencyId",
                table: "Hashtags");

            migrationBuilder.DropIndex(
                name: "IX_Hashtags_AgencyId",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Hashtags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agencies",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgencyUrl",
                table: "Agencies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgencyUrl",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AbpUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AgencyId",
                table: "Hashtags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agencies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_AgencyId",
                table: "Hashtags",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_Agencies_AgencyId",
                table: "Hashtags",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
