using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiOgrenmeProjesi.Migrations
{
    /// <inheritdoc />
    public partial class createTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_c",
                table: "c");

            migrationBuilder.RenameTable(
                name: "c",
                newName: "customerEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customerEntities",
                table: "customerEntities",
                column: "CustId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_customerEntities",
                table: "customerEntities");

            migrationBuilder.RenameTable(
                name: "customerEntities",
                newName: "c");

            migrationBuilder.AddPrimaryKey(
                name: "PK_c",
                table: "c",
                column: "CustId");
        }
    }
}
