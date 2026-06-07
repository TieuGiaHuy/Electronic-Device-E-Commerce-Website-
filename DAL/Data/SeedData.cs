using System;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public static class SeedData
    {
        public static void EnsureSeedData(this AppDbContext context)
        {
            // apply any pending migrations (safe for development/test)
            context.Database.Migrate();

            // Categories
            if (!context.Categories.Any())
            {
                var laptops = Category.Create("Laptops");
                var smartphones = Category.Create("Smartphones");
                context.Categories.AddRange(laptops, smartphones);
                context.SaveChanges();
            }

            // Products
            if (!context.Products.Any())
            {
                var catLaptop = context.Categories.First(c => c.Name == "Laptops");
                var catPhone = context.Categories.First(c => c.Name == "Smartphones");

                var p1 = Product.Create(
                    productName: "XtremeBook 14",
                    brand: "ElectroNexus",
                    categoryId: catLaptop.Id,
                    sku: "XB14-STD",
                    color: "Black",
                    cpu: "Intel i5",
                    ram: "8GB",
                    storage: "256GB",
                    screenSize: "14\"",
                    price: 999.99m,
                    stockQuantity: 50,
                    imageUrl: null
                );

                var p2 = Product.Create(
                    productName: "PocketPhone Z",
                    brand: "ElectroNexus",
                    categoryId: catPhone.Id,
                    sku: "PPZ-128",
                    color: "Silver",
                    cpu: "ARM Octa",
                    ram: "6GB",
                    storage: "128GB",
                    screenSize: "6.1\"",
                    price: 699.99m,
                    stockQuantity: 120,
                    imageUrl: null
                );

                context.Products.AddRange(p1, p2);
                context.SaveChanges();
            }

            // ProductVariants
            if (!context.ProductVariants.Any())
            {
                var prod1 = context.Products.First(p => p.ProductName == "XtremeBook 14");
                var prod2 = context.Products.First(p => p.ProductName == "PocketPhone Z");

                var v1 = ProductVariant.Create(
                    productCode: "XB14-STD-BLK",
                    color: "Black",
                    cpu: "Intel i5",
                    ram: "8GB",
                    storage: "256GB",
                    screenSize: "14\"",
                    price: 999.99m,
                    stockQuantity: 30,
                    imageUrl: null,
                    productId: prod1.ProductId
                );

                var v2 = ProductVariant.Create(
                    productCode: "PPZ-128-SLV",
                    color: "Silver",
                    cpu: "ARM Octa",
                    ram: "6GB",
                    storage: "128GB",
                    screenSize: "6.1\"",
                    price: 699.99m,
                    stockQuantity: 80,
                    imageUrl: null,
                    productId: prod2.ProductId
                );

                context.ProductVariants.AddRange(v1, v2);
                context.SaveChanges();
            }

            // Promotions
            if (!context.Promotions.Any())
            {
                var variant1 = context.ProductVariants.First();
                var variant2 = context.ProductVariants.Skip(1).FirstOrDefault() ?? variant1;

                var now = DateTime.UtcNow;

                var promo1 = Promotion.Create(
                    productVariantId: variant1.ProductVariantId,
                    salePrice: Math.Round(variant1.Price * 0.85m, 2),
                    maxReservedStock: 5,
                    startAt: now.AddMinutes(-30),
                    endAt: now.AddHours(2)
                );

                var promo2 = Promotion.Create(
                    productVariantId: variant2.ProductVariantId,
                    salePrice: Math.Round(variant2.Price * 0.9m, 2),
                    maxReservedStock: 10,
                    startAt: now.AddHours(1),
                    endAt: now.AddHours(5)
                );

                context.Promotions.AddRange(promo1, promo2);
                context.SaveChanges();
            }
        }
    }
}
