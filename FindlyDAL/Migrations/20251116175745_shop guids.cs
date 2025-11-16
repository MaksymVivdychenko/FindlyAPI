using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FindlyDAL.Migrations
{
    /// <inheritdoc />
    public partial class shopguids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_authors_books_books_bookId",
                table: "authors_books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_Cover_CoverId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_Publishers_PublisherId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_offers_books_BookId",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedOffers_users_UserId",
                table: "UserLikedOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("49a4b032-abe2-46c1-b45a-8fac28493ce1"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("6b0953f6-b37a-49cf-abc1-cd591de9e381"));

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_users_Login",
                table: "Users",
                newName: "IX_Users_Login");

            migrationBuilder.RenameIndex(
                name: "IX_books_PublisherId",
                table: "Books",
                newName: "IX_Books_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_books_CoverId",
                table: "Books",
                newName: "IX_Books_CoverId");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN_13",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "ParserType", "PriceNodePath", "ShopImageUrl" },
                values: new object[,]
                {
                    { new Guid("071e893b-bcda-4c3e-8721-d7205c348db5"), "ksd.ua", "JsonLd", "//script[@type='application/ld+json']", "" },
                    { new Guid("d985a44b-477e-476d-9387-b816c21190d0"), "Yakaboo.ua", "JsonLd", "//script[@data-vmid=\"ProductJsonLd\"]", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN_13",
                table: "Books",
                column: "ISBN_13",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_authors_books_Books_bookId",
                table: "authors_books",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cover_CoverId",
                table: "Books",
                column: "CoverId",
                principalTable: "Cover",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_offers_Books_BookId",
                table: "offers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedOffers_Users_UserId",
                table: "UserLikedOffers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_authors_books_Books_bookId",
                table: "authors_books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cover_CoverId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_offers_Books_BookId",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedOffers_Users_UserId",
                table: "UserLikedOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ISBN_13",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("071e893b-bcda-4c3e-8721-d7205c348db5"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("d985a44b-477e-476d-9387-b816c21190d0"));

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Login",
                table: "users",
                newName: "IX_users_Login");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PublisherId",
                table: "books",
                newName: "IX_books_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CoverId",
                table: "books",
                newName: "IX_books_CoverId");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN_13",
                table: "books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "ParserType", "PriceNodePath", "ShopImageUrl" },
                values: new object[,]
                {
                    { new Guid("49a4b032-abe2-46c1-b45a-8fac28493ce1"), "ksd.ua", "JsonLd", "//script[@type='application/ld+json']", "" },
                    { new Guid("6b0953f6-b37a-49cf-abc1-cd591de9e381"), "Yakaboo.ua", "JsonLd", "//script[@data-vmid=\"ProductJsonLd\"]", "" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_authors_books_books_bookId",
                table: "authors_books",
                column: "bookId",
                principalTable: "books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_Cover_CoverId",
                table: "books",
                column: "CoverId",
                principalTable: "Cover",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_Publishers_PublisherId",
                table: "books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_offers_books_BookId",
                table: "offers",
                column: "BookId",
                principalTable: "books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedOffers_users_UserId",
                table: "UserLikedOffers",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
