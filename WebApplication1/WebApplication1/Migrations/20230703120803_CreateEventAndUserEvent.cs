using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHorizonBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateEventAndUserEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Event",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Event",
                newName: "Name");
        }
    }
}
