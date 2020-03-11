using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.Migrations
{
    public partial class add_column_applicant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CV",
                table: "Applicant",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "Applicant");
        }
    }
}
