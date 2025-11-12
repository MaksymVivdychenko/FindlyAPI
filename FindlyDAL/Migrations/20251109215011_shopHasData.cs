using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FindlyDAL.Migrations
{
    /// <inheritdoc />
    public partial class shopHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
