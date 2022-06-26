using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nolan.HK.Migrations.Migrations
{
    public partial class c3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Userid",
                table: "TimeSheet",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_Userid",
                table: "TimeSheet",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheet_User_Userid",
                table: "TimeSheet",
                column: "Userid",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheet_User_Userid",
                table: "TimeSheet");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheet_Userid",
                table: "TimeSheet");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "TimeSheet");
        }
    }
}
