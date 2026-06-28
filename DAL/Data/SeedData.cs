using DAL.Models;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;

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
                var accessories = Category.Create("Accessories");
                var tablets = Category.Create("Tablets");
                context.Categories.AddRange(laptops, smartphones, accessories, tablets);
                context.SaveChanges();
            }

            // Products
            if (!context.Products.Any())
            {
                var catLaptop = context.Categories.First(c => c.Name == "Laptops");
                var catPhone = context.Categories.First(c => c.Name == "Smartphones");
                var catAccessory = context.Categories.First(c => c.Name == "Accessories");
                var catTablet = context.Categories.First(c => c.Name == "Tablets");

                var products = new[]
                {
                    Product.Create("XtremeBook 14", "ElectroNexus", catLaptop.Id, null),
                    Product.Create("XtremeBook 16", "ElectroNexus", catLaptop.Id, null),
                    Product.Create("PocketPhone Z", "ElectroNexus", catPhone.Id, null),
                    Product.Create("PocketPhone Pro", "ElectroNexus", catPhone.Id, null),
                    Product.Create("SoundBuds Pro", "AudioTech", catAccessory.Id, null),
                    Product.Create("PowerBank 20000", "ChargeMaster", catAccessory.Id, null),
                    Product.Create("TabPro 11", "ElectroNexus", catTablet.Id, null),
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // ProductVariants
            if (!context.ProductVariants.Any())
            {
                var prod1 = context.Products.First(p => p.ProductName == "XtremeBook 14");
                var prod2 = context.Products.First(p => p.ProductName == "XtremeBook 16");
                var prod3 = context.Products.First(p => p.ProductName == "PocketPhone Z");
                var prod4 = context.Products.First(p => p.ProductName == "PocketPhone Pro");
                var prod5 = context.Products.First(p => p.ProductName == "SoundBuds Pro");
                var prod6 = context.Products.First(p => p.ProductName == "PowerBank 20000");
                var prod7 = context.Products.First(p => p.ProductName == "TabPro 11");

                var variants = new[]
                {
                    ProductVariant.Create("XB14-STD-BLK", "Black", "Intel i5", "8GB", "256GB", "14\"", 999.99m, 30, null, prod1.ProductId),
                    ProductVariant.Create("XB14-PRO-SLV", "Silver", "Intel i7", "16GB", "512GB", "14\"", 1299.99m, 15, null, prod1.ProductId),

                    ProductVariant.Create("XB16-PRO-BLK", "Black", "Intel i9", "32GB", "1TB", "16\"", 1899.99m, 10, null, prod2.ProductId),

                    ProductVariant.Create("PPZ-128-SLV", "Silver", "ARM Octa", "6GB", "128GB", "6.1\"", 699.99m, 80, null, prod3.ProductId),
                    ProductVariant.Create("PPZ-256-BLK", "Black", "ARM Octa", "8GB", "256GB", "6.1\"", 799.99m, 50, null, prod3.ProductId),

                    ProductVariant.Create("PPP-256-BLK", "Black", "ARM Octa Pro", "8GB", "256GB", "6.7\"", 999.99m, 40, null, prod4.ProductId),
                    ProductVariant.Create("PPP-512-SLV", "Silver", "ARM Octa Pro", "12GB", "512GB", "6.7\"", 1199.99m, 25, null, prod4.ProductId),

                    ProductVariant.Create("SBP-BLK", "Black", null, null, null, null, 149.99m, 100, null, prod5.ProductId),
                    ProductVariant.Create("SBP-WHT", "White", null, null, null, null, 149.99m, 80, null, prod5.ProductId),

                    ProductVariant.Create("PB20-BLK", "Black", null, null, null, null, 59.99m, 200, null, prod6.ProductId),

                    ProductVariant.Create("TP11-SLV", "Silver", "ARM Pro", "8GB", "128GB", "11\"", 499.99m, 30, null, prod7.ProductId),
                };

                context.ProductVariants.AddRange(variants);
                context.SaveChanges();
            }

            // Promotions
            if (!context.Promotions.Any())
            {
                var variants = context.ProductVariants.ToList();
                var now = DateTime.UtcNow;

                var promotions = new List<Promotion>();

                // 1. Active - Percentage Discount (Flash Sale)
                promotions.Add(new Promotion().Create(
                    PromotionTypeEnum.Percentage,
                    percentage: 20,
                    maxReservedStock: 10,
                    isLimitedTime: true,
                    startAt: now.AddMinutes(-60),
                    endAt: now.AddHours(3)
                ));

                // 2. Active - Fixed Amount (Seasonal Sale)
                var variant2 = variants.ElementAtOrDefault(1);
                if (variant2 != null)
                {
                    promotions.Add(new Promotion().Create(
                        PromotionTypeEnum.FixedAmount,
                        salePrice: Math.Round(variant2.Price * 0.85m, 2),
                        maxReservedStock: 5,
                        isLimitedTime: true,
                        startAt: now.AddMinutes(-30),
                        endAt: now.AddHours(5)
                    ));
                }

                // 3. Upcoming - Percentage (Pre-order)
                promotions.Add(new Promotion().Create(
                    PromotionTypeEnum.Percentage,
                    percentage: 15,
                    maxReservedStock: 20,
                    isLimitedTime: true,
                    startAt: now.AddHours(2),
                    endAt: now.AddHours(24)
                ));

                // 4. Upcoming - Fixed Amount (Early Bird)
                var variant4 = variants.ElementAtOrDefault(3);
                if (variant4 != null)
                {
                    promotions.Add(new Promotion().Create(
                        PromotionTypeEnum.FixedAmount,
                        salePrice: Math.Round(variant4.Price * 0.9m, 2),
                        maxReservedStock: 15,
                        isLimitedTime: true,
                        startAt: now.AddHours(4),
                        endAt: now.AddHours(12)
                    ));
                }

                // 5. Expired - Percentage (Past Promotion)
                promotions.Add(new Promotion().Create(
                    PromotionTypeEnum.Percentage,
                    percentage: 30,
                    maxReservedStock: 10,
                    isLimitedTime: true,
                    startAt: now.AddDays(-3),
                    endAt: now.AddDays(-1)
                ));

                // 6. Expired - Fixed Amount (Past Promotion)
                var variant6 = variants.ElementAtOrDefault(5);
                if (variant6 != null)
                {
                    promotions.Add(new Promotion().Create(
                        PromotionTypeEnum.FixedAmount,
                        salePrice: Math.Round(variant6.Price * 0.75m, 2),
                        maxReservedStock: 8,
                        isLimitedTime: true,
                        startAt: now.AddDays(-5),
                        endAt: now.AddDays(-2)
                    ));
                }

                // 7. Active - No time limit (Always on sale)
                var variant7 = variants.ElementAtOrDefault(6);
                if (variant7 != null)
                {
                    promotions.Add(new Promotion().Create(
                        PromotionTypeEnum.FixedAmount,
                        salePrice: Math.Round(variant7.Price * 0.88m, 2),
                        maxReservedStock: 30,
                        isLimitedTime: false,
                        startAt: null,
                        endAt: null
                    ));
                }

                // 11. Disabled promotion
                var promoDisabled = new Promotion().Create(
                    PromotionTypeEnum.Percentage,
                    percentage: 25,
                    maxReservedStock: 10,
                    isLimitedTime: true,
                    startAt: now.AddMinutes(-10),
                    endAt: now.AddHours(2)
                );
                promoDisabled.IsDisabled = true;
                promotions.Add(promoDisabled);

                context.Promotions.AddRange(promotions);
                context.SaveChanges();

            }
        }
    }
}