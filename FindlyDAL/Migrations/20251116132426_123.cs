using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FindlyDAL.Migrations
{
    /// <inheritdoc />
    public partial class _123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "books");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "offers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_users_Login",
                table: "users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Login",
                table: "users");

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

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "offers");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "books",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("00461873-724c-49bf-adde-fe204bce4466"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("11226951-89aa-4611-9346-11b9aba3d52d"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("26e5c51c-37e3-4c38-8e3e-41332eeaea73"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("c043abf2-91e1-4e68-82bd-8d55c88d7457"),
                column: "Description",
                value: null);
        }
    }
}
