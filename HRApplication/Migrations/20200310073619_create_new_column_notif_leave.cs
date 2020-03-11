using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.Migrations
{
    public partial class create_new_column_notif_leave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    LeaveTime = table.Column<DateTime>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    sender_id = table.Column<string>(nullable: true),
                    role_id = table.Column<int>(nullable: false),
                    read_at = table.Column<DateTime>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequest");

            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
