using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetoStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class customersaleesquema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Sale",
                schema: "Apptelink",
                newName: "Sale",
                newSchema: "Apptelink");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "Apptelink",
                newName: "Customer",
                newSchema: "Apptelink");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Apptelink");

            migrationBuilder.RenameTable(
                name: "Sale",
                schema: "Apptelink",
                newName: "Sale",
                newSchema: "Apptelink");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "Apptelink",
                newName: "Customer",
                newSchema: "Apptelink");
        }
    }
}
