using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nolan.HK.Migrations.Migrations
{
    public partial class c2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "TimeSheet",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_ProjectID",
                table: "TimeSheet",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheet_Project_ProjectID",
                table: "TimeSheet",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheet_Project_ProjectID",
                table: "TimeSheet");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheet_ProjectID",
                table: "TimeSheet");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "TimeSheet");
        }
    }
}
