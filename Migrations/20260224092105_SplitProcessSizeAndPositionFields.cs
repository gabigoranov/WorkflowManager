using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowManager.Migrations
{
    /// <inheritdoc />
    public partial class SplitProcessSizeAndPositionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Processes");

            migrationBuilder.AddColumn<int>(
                name: "CoordX",
                table: "Processes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoordY",
                table: "Processes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Processes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Processes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordX",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "CoordY",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Processes");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Processes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Processes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
