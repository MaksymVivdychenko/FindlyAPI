using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FindlyDAL.Migrations
{
    /// <inheritdoc />
    public partial class withoudseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("07a4509b-9e0d-4cff-9ebb-8a7936f2dc4a"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("08334271-bab4-40ad-8a32-13fe4fae62e4"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("5e75281c-448f-4488-b5ff-2caa6c70e7b1"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("83347044-2fad-4b0c-b0fd-3972a0febb38"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("ad0d2eed-2928-4dbb-83b3-5cefbc149b91"));

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

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "ParserType", "PriceNodePath", "ShopImageUrl" },
                values: new object[,]
                {
                    { new Guid("49a4b032-abe2-46c1-b45a-8fac28493ce1"), "ksd.ua", "JsonLd", "//script[@type='application/ld+json']", "" },
                    { new Guid("6b0953f6-b37a-49cf-abc1-cd591de9e381"), "Yakaboo.ua", "JsonLd", "//script[@data-vmid=\"ProductJsonLd\"]", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("49a4b032-abe2-46c1-b45a-8fac28493ce1"));

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: new Guid("6b0953f6-b37a-49cf-abc1-cd591de9e381"));

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
                    { new Guid("08334271-bab4-40ad-8a32-13fe4fae62e4"), "balka-book.com", "JsonLd", "//script[@type='application/ld+json']", "" },
                    { new Guid("5e75281c-448f-4488-b5ff-2caa6c70e7b1"), "Yakaboo.ua", "JsonLd", "//script[@data-vmid=\"ProductJsonLd\"]", "" },
                    { new Guid("83347044-2fad-4b0c-b0fd-3972a0febb38"), "book24.ua", "Node", "//div[@class='product-main']//span[@class='price_value']", "" },
                    { new Guid("ad0d2eed-2928-4dbb-83b3-5cefbc149b91"), "ksd.ua", "JsonLd", "//script[@type='application/ld+json']", "" }
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "Id", "CoverId", "ISBN_13", "ImageUrl", "PublisherId", "Title" },
                values: new object[,]
                {
                    { new Guid("00461873-724c-49bf-adde-fe204bce4466"), new Guid("2a9dbeec-0c49-41e5-b947-dcd3d5b6f717"), "978-617-7910-08-3", null, new Guid("0a20441e-d50a-4da4-b560-a91c4aa69080"), "1984" },
                    { new Guid("11226951-89aa-4611-9346-11b9aba3d52d"), new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"), "978-617-12-7689-5", null, new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"), "Дюна" },
                    { new Guid("26e5c51c-37e3-4c38-8e3e-41332eeaea73"), new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"), "978-966-993-391-1", null, new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"), "1984" },
                    { new Guid("c043abf2-91e1-4e68-82bd-8d55c88d7457"), new Guid("a9819fd8-65c7-4925-af38-a29c030280f6"), "978-617-12-8351-0", null, new Guid("42e470a1-702a-4994-a7f9-0b7283cc41d6"), "Відьмак. Останнє бажання. Книга 1" }
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
    }
}
