using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductReviewUserIdToCustomerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_reviews_users_user_id",
                table: "product_reviews");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "product_reviews",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "ix_product_reviews_user_id",
                table: "product_reviews",
                newName: "ix_product_reviews_customer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_reviews_customers_customer_id",
                table: "product_reviews",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_reviews_customers_customer_id",
                table: "product_reviews");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "product_reviews",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_product_reviews_customer_id",
                table: "product_reviews",
                newName: "ix_product_reviews_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_reviews_users_user_id",
                table: "product_reviews",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
