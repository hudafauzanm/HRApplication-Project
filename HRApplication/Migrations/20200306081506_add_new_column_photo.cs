using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.Migrations
{
    public partial class add_new_column_photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Applicant",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Applicant");
        }
    }
}
