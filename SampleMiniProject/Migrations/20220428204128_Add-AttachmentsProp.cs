using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleMiniProject.Migrations
{
    public partial class AddAttachmentsProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "ReceivedSamples",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "ReceivedSamples");
        }
    }
}
