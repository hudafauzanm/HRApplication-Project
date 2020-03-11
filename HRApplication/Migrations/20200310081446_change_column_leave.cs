using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.Migrations
{
    public partial class change_column_leave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Read_at",
                table: "LeaveRequest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read_at",
                table: "LeaveRequest");
        }
    }
}
