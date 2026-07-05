using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Cart_Item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    cart_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    product_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_items", x => x.cart_item_id);
                    table.ForeignKey(
                        name: "fk_cart_items_product_variants_product_variant_id",
                        column: x => x.product_variant_id,
                        principalTable: "product_variants",
                        principalColumn: "product_variant_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_product_variant_id",
                table: "cart_items",
                column: "product_variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");
        }
    }
}
