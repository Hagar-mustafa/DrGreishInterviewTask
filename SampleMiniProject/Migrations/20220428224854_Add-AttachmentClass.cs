using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleMiniProject.Migrations
{
    public partial class AddAttachmentClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "ReceivedSamples");

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedSampleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_ReceivedSamples_ReceivedSampleID",
                        column: x => x.ReceivedSampleID,
                        principalTable: "ReceivedSamples",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ReceivedSampleID",
                table: "Attachment",
                column: "ReceivedSampleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "ReceivedSamples",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
