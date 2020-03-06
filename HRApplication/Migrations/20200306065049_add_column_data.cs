using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.Migrations
{
    public partial class add_column_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Birthplace",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Employee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "_emergencyContact",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Applicant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Applicant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Birthplace",
                table: "Applicant",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Applicant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "_emergencyContact",
                table: "Applicant",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Birthplace",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "_emergencyContact",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Birthplace",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "_emergencyContact",
                table: "Applicant");
        }
    }
}
