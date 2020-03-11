using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.Migrations
{
    public partial class change_column_leavereq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "LeaveRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "LeaveRequest");
        }
    }
}
