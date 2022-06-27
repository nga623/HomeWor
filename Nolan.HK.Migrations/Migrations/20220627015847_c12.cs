using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nolan.HK.Migrations.Migrations
{
    public partial class c12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    UserTypeEnum = table.Column<int>(type: "integer", nullable: false),
                    UserType = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalCount = table.Column<int>(type: "integer", nullable: false),
                    ApproveTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApproveStatusEnum = table.Column<int>(type: "integer", nullable: false),
                    ApproveStatus = table.Column<string>(type: "text", nullable: true),
                    ProjectID = table.Column<Guid>(type: "uuid", nullable: false),
                    Userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheet_User_Userid",
                        column: x => x.Userid,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    Userid = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Weekday = table.Column<string>(type: "text", nullable: true),
                    TimesheetCount = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ProjectID = table.Column<Guid>(type: "uuid", nullable: false, comment: "项目id"),
                    TimesheetID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetail_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetail_TimeSheet_TimesheetID",
                        column: x => x.TimesheetID,
                        principalTable: "TimeSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetail_User_Userid",
                        column: x => x.Userid,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_ProjectID",
                table: "TimeSheet",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_Userid",
                table: "TimeSheet",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetail_ProjectID",
                table: "TimeSheetDetail",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetail_TimesheetID",
                table: "TimeSheetDetail",
                column: "TimesheetID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetail_Userid",
                table: "TimeSheetDetail",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSheetDetail");

            migrationBuilder.DropTable(
                name: "TimeSheet");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
