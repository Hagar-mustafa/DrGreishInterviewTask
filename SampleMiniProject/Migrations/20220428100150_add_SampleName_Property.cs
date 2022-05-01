using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleMiniProject.Migrations
{
    public partial class add_SampleName_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SampleName",
                table: "ReceivedSamples",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SampleName",
                table: "ReceivedSamples");
        }
    }
}
