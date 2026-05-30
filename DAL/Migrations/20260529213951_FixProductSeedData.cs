using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "product_variants",
                keyColumn: "product_variant_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "product_variants",
                keyColumn: "product_variant_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "product_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "product_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "created_at", "is_deleted", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 29, 21, 36, 21, 479, DateTimeKind.Utc).AddTicks(6600), false, "Electronics", null },
                    { 2, new DateTime(2026, 5, 29, 21, 36, 21, 479, DateTimeKind.Utc).AddTicks(6746), false, "Books", null },
                    { 3, new DateTime(2026, 5, 29, 21, 36, 21, 479, DateTimeKind.Utc).AddTicks(6747), false, "Clothing", null }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "product_id", "brand", "category_id", "created_at", "is_deleted", "product_name", "updated_at" },
                values: new object[,]
                {
                    { 1, "Dell", 1, new DateTime(2026, 5, 29, 21, 36, 21, 484, DateTimeKind.Utc).AddTicks(2767), false, "Laptop Dell XPS 13", null },
                    { 2, "Samsung", 2, new DateTime(2026, 5, 29, 21, 36, 21, 484, DateTimeKind.Utc).AddTicks(2900), false, "Smartphone Samsung Galaxy S21", null }
                });

            migrationBuilder.InsertData(
                table: "product_variants",
                columns: new[] { "product_variant_id", "color", "cpu", "created_at", "image_url", "is_deleted", "price", "product_id", "ram", "screen_size", "sku", "stock_quantity", "storage", "updated_at" },
                values: new object[,]
                {
                    { 1, "Black", "Intel Core i7", new DateTime(2026, 5, 29, 21, 36, 21, 487, DateTimeKind.Utc).AddTicks(7215), "https://example.com/images/laptop-001.jpg", false, 999.99m, 1, "16GB", "15.6 inch", "LAPTOP-001", 50, "512GB SSD", null },
                    { 2, "Silver", "AMD Ryzen 7", new DateTime(2026, 5, 29, 21, 36, 21, 487, DateTimeKind.Utc).AddTicks(7353), "https://example.com/images/laptop-002.jpg", false, 899.99m, 1, "16GB", "14 inch", "LAPTOP-002", 30, "1TB SSD", null }
                });
        }
    }
}
