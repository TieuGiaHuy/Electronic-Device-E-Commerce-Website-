using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemakePromotionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_promotions_product_variants_product_variant_id",
                table: "promotions");

            migrationBuilder.DropIndex(
                name: "ix_promotions_product_variant_id",
                table: "promotions");

            migrationBuilder.DropColumn(
                name: "product_variant_id",
                table: "promotions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_at",
                table: "promotions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "sale_price",
                table: "promotions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "max_reserved_stock",
                table: "promotions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "is_disabled",
                table: "promotions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "promotions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_at",
                table: "promotions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "promotions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "promotion_id",
                table: "promotions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "is_limited_time",
                table: "promotions",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_reserved_stock",
                table: "promotions",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "promotions",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "percentage",
                table: "promotions",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "promotion_type",
                table: "promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "threshold_price",
                table: "promotions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_promotions_end_at",
                table: "promotions",
                column: "end_at");

            migrationBuilder.CreateIndex(
                name: "ix_promotions_is_deleted",
                table: "promotions",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_promotions_is_disabled",
                table: "promotions",
                column: "is_disabled");

            migrationBuilder.CreateIndex(
                name: "ix_promotions_promotion_type",
                table: "promotions",
                column: "promotion_type");

            migrationBuilder.CreateIndex(
                name: "ix_promotions_start_at",
                table: "promotions",
                column: "start_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_promotions_end_at",
                table: "promotions");

            migrationBuilder.DropIndex(
                name: "ix_promotions_is_deleted",
                table: "promotions");

            migrationBuilder.DropIndex(
                name: "ix_promotions_is_disabled",
                table: "promotions");

            migrationBuilder.DropIndex(
                name: "ix_promotions_promotion_type",
                table: "promotions");

            migrationBuilder.DropIndex(
                name: "ix_promotions_start_at",
                table: "promotions");

            migrationBuilder.DropColumn(
                name: "is_limited_time",
                table: "promotions");

            migrationBuilder.DropColumn(
                name: "is_reserved_stock",
                table: "promotions");

            migrationBuilder.DropColumn(
                name: "name",
                table: "promotions");

            migrationBuilder.DropColumn(
                name: "percentage",
                table: "promotions");

            migrationBuilder.DropColumn(
                name: "promotion_type",
                table: "promotions");

            migrationBuilder.DropColumn(
                name: "threshold_price",
                table: "promotions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_at",
                table: "promotions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sale_price",
                table: "promotions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "max_reserved_stock",
                table: "promotions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_disabled",
                table: "promotions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "promotions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_at",
                table: "promotions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "promotions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "promotion_id",
                table: "promotions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "product_variant_id",
                table: "promotions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_promotions_product_variant_id",
                table: "promotions",
                column: "product_variant_id");

            migrationBuilder.AddForeignKey(
                name: "fk_promotions_product_variants_product_variant_id",
                table: "promotions",
                column: "product_variant_id",
                principalTable: "product_variants",
                principalColumn: "product_variant_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
