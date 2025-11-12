using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FindlyDAL.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Languages_LanguageId",
                table: "books");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_books_LanguageId",
                table: "books");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("0adccd8c-8b5c-4d7a-8022-07a64f6a5475"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("475d94f1-6a93-4856-ab54-f6bcd8b1968e"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("8bebdcbf-e371-4506-aff1-6b7e424df6fe"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("c3395966-f529-49a7-a43e-d0d6e6d19218"));

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "books");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN_13",
                table: "books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("391d1568-480f-4164-a10f-2c38a7391858"), "Френк Герберт" },
                    { new Guid("7c69abe6-7604-4804-8d45-d5bdde4410d9"), "Анджей Сапковський" },
                    { new Guid("a259a396-b950-4d63-8ce9-8b83fa187a8e"), "Джордж Орвелл" }
                });

            migrationBuilder.InsertData(
                table: "Cover",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2a9dbeec-0c49-41e5-b947-dcd3d5b6f717"), "М'яка" },
                    { new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"), "Тверда" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("07a4509b-9e0d-4cff-9ebb-8a7936f2dc4a"), "КСД" },
                    { new Guid("0a20441e-d50a-4da4-b560-a91c4aa69080"), "BookChef" },
                    { new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"), "Видавництво Жупанського" }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "ParserType", "PriceNodePath", "ShopImageUrl" },
                values: new object[,]
                {
                    { new Guid("8ab36ada-201e-4d5d-8ac5-2654e6569c64"), "book24.ua", "Node", "//div[@class='product-main']//span[@class='price_value']", "" },
                    { new Guid("ba191c76-2816-43a1-94f1-45f2f9769a70"), "Yakaboo.ua", "JsonLd", "//script[@data-vmid=\"ProductJsonLd\"]", "" },
                    { new Guid("d63a589d-82e7-4dff-914e-7c64baefb06d"), "balka-book.com", "JsonLd", "//script[@type='application/ld+json']", "" },
                    { new Guid("ff6e7610-55b3-41f3-9b90-a375394cbe95"), "ksd.ua", "JsonLd", "//script[@type='application/ld+json']", "" }
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "Id", "CoverId", "Description", "ISBN_13", "ImageUrl", "PublisherId", "Title" },
                values: new object[,]
                {
                    { new Guid("00461873-724c-49bf-adde-fe204bce4466"), new Guid("2a9dbeec-0c49-41e5-b947-dcd3d5b6f717"), null, "978-617-7910-08-3", null, new Guid("0a20441e-d50a-4da4-b560-a91c4aa69080"), "1984" },
                    { new Guid("11226951-89aa-4611-9346-11b9aba3d52d"), new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"), null, "978-617-12-7689-5", null, new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"), "Дюна" },
                    { new Guid("26e5c51c-37e3-4c38-8e3e-41332eeaea73"), new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"), null, "978-966-993-391-1", null, new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"), "1984" },
                    { new Guid("c043abf2-91e1-4e68-82bd-8d55c88d7457"), new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"), null, "978-617-12-8351-0", null, new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"), "Відьмак. Останнє бажання. Книга 1" }
                });

            migrationBuilder.InsertData(
                table: "authors_books",
                columns: new[] { "authorId", "bookId" },
                values: new object[,]
                {
                    { new Guid("391d1568-480f-4164-a10f-2c38a7391858"), new Guid("11226951-89aa-4611-9346-11b9aba3d52d") },
                    { new Guid("7c69abe6-7604-4804-8d45-d5bdde4410d9"), new Guid("c043abf2-91e1-4e68-82bd-8d55c88d7457") },
                    { new Guid("a259a396-b950-4d63-8ce9-8b83fa187a8e"), new Guid("00461873-724c-49bf-adde-fe204bce4466") },
                    { new Guid("a259a396-b950-4d63-8ce9-8b83fa187a8e"), new Guid("26e5c51c-37e3-4c38-8e3e-41332eeaea73") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("07a4509b-9e0d-4cff-9ebb-8a7936f2dc4a"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("8ab36ada-201e-4d5d-8ac5-2654e6569c64"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("ba191c76-2816-43a1-94f1-45f2f9769a70"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("d63a589d-82e7-4dff-914e-7c64baefb06d"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("ff6e7610-55b3-41f3-9b90-a375394cbe95"));

            migrationBuilder.DeleteData(
                table: "authors_books",
                keyColumns: new[] { "authorId", "bookId" },
                keyValues: new object[] { new Guid("391d1568-480f-4164-a10f-2c38a7391858"), new Guid("11226951-89aa-4611-9346-11b9aba3d52d") });

            migrationBuilder.DeleteData(
                table: "authors_books",
                keyColumns: new[] { "authorId", "bookId" },
                keyValues: new object[] { new Guid("7c69abe6-7604-4804-8d45-d5bdde4410d9"), new Guid("c043abf2-91e1-4e68-82bd-8d55c88d7457") });

            migrationBuilder.DeleteData(
                table: "authors_books",
                keyColumns: new[] { "authorId", "bookId" },
                keyValues: new object[] { new Guid("a259a396-b950-4d63-8ce9-8b83fa187a8e"), new Guid("00461873-724c-49bf-adde-fe204bce4466") });

            migrationBuilder.DeleteData(
                table: "authors_books",
                keyColumns: new[] { "authorId", "bookId" },
                keyValues: new object[] { new Guid("a259a396-b950-4d63-8ce9-8b83fa187a8e"), new Guid("26e5c51c-37e3-4c38-8e3e-41332eeaea73") });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("391d1568-480f-4164-a10f-2c38a7391858"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7c69abe6-7604-4804-8d45-d5bdde4410d9"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a259a396-b950-4d63-8ce9-8b83fa187a8e"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("00461873-724c-49bf-adde-fe204bce4466"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("11226951-89aa-4611-9346-11b9aba3d52d"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("26e5c51c-37e3-4c38-8e3e-41332eeaea73"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("c043abf2-91e1-4e68-82bd-8d55c88d7457"));

            migrationBuilder.DeleteData(
                table: "Cover",
                keyColumn: "Id",
                keyValue: new Guid("2a9dbeec-0c49-41e5-b947-dcd3d5b6f717"));

            migrationBuilder.DeleteData(
                table: "Cover",
                keyColumn: "Id",
                keyValue: new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("0a20441e-d50a-4da4-b560-a91c4aa69080"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"));

            migrationBuilder.AlterColumn<string>(
                name: "ISBN_13",
                table: "books",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "ParserType", "PriceNodePath", "ShopImageUrl" },
                values: new object[,]
                {
                    { new Guid("0adccd8c-8b5c-4d7a-8022-07a64f6a5475"), "Yakaboo.ua", "JsonLd", "//script[@data-vmid=\"ProductJsonLd\"]", "" },
                    { new Guid("475d94f1-6a93-4856-ab54-f6bcd8b1968e"), "book24.ua", "Node", "//div[@class='product-main']//span[@class='price_value']", "" },
                    { new Guid("8bebdcbf-e371-4506-aff1-6b7e424df6fe"), "ksd.ua", "JsonLd", "//script[@type='application/ld+json']", "" },
                    { new Guid("c3395966-f529-49a7-a43e-d0d6e6d19218"), "balka-book.com", "JsonLd", "//script[@type='application/ld+json']", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_LanguageId",
                table: "books",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Languages_LanguageId",
                table: "books",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
