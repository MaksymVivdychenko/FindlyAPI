using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindlyDAL.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offers_Books_BookId",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "FK_offers_Shops_ShopId",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedOffers_offers_OfferId",
                table: "UserLikedOffers");

            migrationBuilder.DropTable(
                name: "authors_books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_offers",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "ParserType",
                table: "Shops");

            migrationBuilder.RenameTable(
                name: "offers",
                newName: "Offers");

            migrationBuilder.RenameColumn(
                name: "PriceNodePath",
                table: "Shops",
                newName: "JsonLdPath");

            migrationBuilder.RenameIndex(
                name: "IX_offers_ShopId",
                table: "Offers",
                newName: "IX_Offers_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_offers_BookId",
                table: "Offers",
                newName: "IX_Offers_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "authorsBooks",
                columns: table => new
                {
                    authorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorsBooks", x => new { x.authorId, x.bookId });
                    table.ForeignKey(
                        name: "FK_authorsBooks_Authors_authorId",
                        column: x => x.authorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_authorsBooks_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_authorsBooks_bookId",
                table: "authorsBooks",
                column: "bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Books_BookId",
                table: "Offers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Shops_ShopId",
                table: "Offers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedOffers_Offers_OfferId",
                table: "UserLikedOffers",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Books_BookId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Shops_ShopId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedOffers_Offers_OfferId",
                table: "UserLikedOffers");

            migrationBuilder.DropTable(
                name: "authorsBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "offers");

            migrationBuilder.RenameColumn(
                name: "JsonLdPath",
                table: "Shops",
                newName: "PriceNodePath");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_ShopId",
                table: "offers",
                newName: "IX_offers_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_BookId",
                table: "offers",
                newName: "IX_offers_BookId");

            migrationBuilder.AddColumn<string>(
                name: "ParserType",
                table: "Shops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_offers",
                table: "offers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "authors_books",
                columns: table => new
                {
                    authorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors_books", x => new { x.authorId, x.bookId });
                    table.ForeignKey(
                        name: "FK_authors_books_Authors_authorId",
                        column: x => x.authorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_authors_books_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("071e893b-bcda-4c3e-8721-d7205c348db5"),
                column: "ParserType",
                value: "JsonLd");

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("d985a44b-477e-476d-9387-b816c21190d0"),
                column: "ParserType",
                value: "JsonLd");

            migrationBuilder.CreateIndex(
                name: "IX_authors_books_bookId",
                table: "authors_books",
                column: "bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_offers_Books_BookId",
                table: "offers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_offers_Shops_ShopId",
                table: "offers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedOffers_offers_OfferId",
                table: "UserLikedOffers",
                column: "OfferId",
                principalTable: "offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
