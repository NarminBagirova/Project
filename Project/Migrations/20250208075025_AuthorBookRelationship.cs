using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class AuthorBookRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorBookId",
                table: "AuthorBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_AuthorBookId",
                table: "AuthorBooks",
                column: "AuthorBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_AuthorBooks_AuthorBookId",
                table: "AuthorBooks",
                column: "AuthorBookId",
                principalTable: "AuthorBooks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_AuthorBooks_AuthorBookId",
                table: "AuthorBooks");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBooks_AuthorBookId",
                table: "AuthorBooks");

            migrationBuilder.DropColumn(
                name: "AuthorBookId",
                table: "AuthorBooks");
        }
    }
}
