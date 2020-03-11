using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.Migrations
{
    public partial class create_event_table_new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventName = table.Column<string>(nullable: true),
                    TimeEvent = table.Column<DateTime>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
