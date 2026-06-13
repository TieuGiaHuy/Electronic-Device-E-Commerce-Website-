using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_TotalAmount_NonNegative",
                table: "orders",
                sql: "[total_amount] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "order_items",
                sql: "[quantity] > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_TotalAmount_NonNegative",
                table: "orders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "order_items");
        }
    }
}
