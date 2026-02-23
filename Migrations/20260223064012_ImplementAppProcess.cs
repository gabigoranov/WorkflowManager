using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowManager.Migrations
{
    /// <inheritdoc />
    public partial class ImplementAppProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppProcess_Directory",
                table: "Processes",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArgumentDirectory",
                table: "Processes",
                type: "TEXT",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppProcess_Directory",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ArgumentDirectory",
                table: "Processes");
        }
    }
}
