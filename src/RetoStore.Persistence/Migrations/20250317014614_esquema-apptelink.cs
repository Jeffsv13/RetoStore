using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetoStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class esquemaapptelink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Apptelink");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genre",
                newSchema: "Apptelink");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Event",
                newSchema: "Apptelink");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Title",
                schema: "Apptelink",
                table: "Event",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Event_Title",
                schema: "Apptelink",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Genre",
                schema: "Apptelink",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Event",
                schema: "Apptelink",
                newName: "Event");
        }
    }
}
