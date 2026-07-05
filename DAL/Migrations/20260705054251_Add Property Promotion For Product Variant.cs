using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyPromotionForProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "promotion_id",
                table: "product_variants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_variants_promotion_id",
                table: "product_variants",
                column: "promotion_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_variants_promotions_promotion_id",
                table: "product_variants",
                column: "promotion_id",
                principalTable: "promotions",
                principalColumn: "promotion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_variants_promotions_promotion_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "ix_product_variants_promotion_id",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "promotion_id",
                table: "product_variants");
        }
    }
}
