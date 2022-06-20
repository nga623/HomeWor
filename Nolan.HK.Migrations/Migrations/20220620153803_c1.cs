using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nolan.HK.Migrations.Migrations
{
    public partial class c1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSheet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalCount = table.Column<int>(type: "integer", nullable: false),
                    ApproveTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApproveStatusEnum = table.Column<int>(type: "integer", nullable: false),
                    ApproveStatus = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    Userid = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimesheetCount = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ProjectID = table.Column<Guid>(type: "uuid", nullable: false, comment: "项目id"),
                    TimesheetID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetDetail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSheet");

            migrationBuilder.DropTable(
                name: "TimeSheetDetail");
        }
    }
}
