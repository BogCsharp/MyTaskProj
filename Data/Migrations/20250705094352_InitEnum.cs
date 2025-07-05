using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "IsComplited",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplited",
                table: "Tasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Tasks",
                type: "bit",
                nullable: true);
        }
    }
}
