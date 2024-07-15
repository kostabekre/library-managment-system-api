using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class BookAmount_BookRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BooksAmount",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksAmount", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BooksAmount_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooksRating",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksRating", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BooksRating_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksAmount");

            migrationBuilder.DropTable(
                name: "BooksRating");
        }
    }
}
