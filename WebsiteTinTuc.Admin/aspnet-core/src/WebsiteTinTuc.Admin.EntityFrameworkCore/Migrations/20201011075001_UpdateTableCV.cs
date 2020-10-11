using Microsoft.EntityFrameworkCore.Migrations;

namespace WebsiteTinTuc.Admin.Migrations
{
    public partial class UpdateTableCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVs_AbpUsers_UserId",
                table: "CVs");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "CVs",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CVs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Portfolio",
                table: "CVs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CVs",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CVs_AbpUsers_UserId",
                table: "CVs",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVs_AbpUsers_UserId",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "Portfolio",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CVs");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "CVs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CVs_AbpUsers_UserId",
                table: "CVs",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
