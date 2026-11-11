using DAL.Models;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public static class SeedData
    {
        public static void EnsureSeedData(this AppDbContext context)
        {
            // apply any pending migrations (safe for development/test)
            context.Database.Migrate();

            // ==================== Categories (6) ====================
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    Category.Create("Đồng hồ cơ (Automatic)"),
                    Category.Create("Đồng hồ Quartz"),
                    Category.Create("Đồng hồ điện tử"),
                    Category.Create("Smartwatch"),
                    Category.Create("Dây đồng hồ"),
                    Category.Create("Phụ kiện đồng hồ"));
                context.SaveChanges();
            }

            // ==================== Products (12) ====================
            if (!context.Products.Any())
            {
                var catAutomatic = context.Categories.First(c => c.Name == "Đồng hồ cơ (Automatic)");
                var catQuartz = context.Categories.First(c => c.Name == "Đồng hồ Quartz");
                var catDigital = context.Categories.First(c => c.Name == "Đồng hồ điện tử");
                var catSmart = context.Categories.First(c => c.Name == "Smartwatch");
                var catStrap = context.Categories.First(c => c.Name == "Dây đồng hồ");
                var catAccessory = context.Categories.First(c => c.Name == "Phụ kiện đồng hồ");

                var products = new[]
                {
                    Product.Create("Heritage 40 Automatic", "Aurelia", catAutomatic.Id, null),
                    Product.Create("Skeleton 42 Automatic", "Aurelia", catAutomatic.Id, null),
                    Product.Create("Classic 38 Quartz", "Meridian", catQuartz.Id, null),
                    Product.Create("Dress Slim 36", "Meridian", catQuartz.Id, null),
                    Product.Create("Digital Sport DS-1", "Pulse", catDigital.Id, null),
                    Product.Create("Retro LCD R-90", "Pulse", catDigital.Id, null),
                    Product.Create("Nova Watch S1", "Nova", catSmart.Id, null),
                    Product.Create("Nova Watch Pro", "Nova", catSmart.Id, null),
                    Product.Create("Dây da bò Classic", "Aurelia", catStrap.Id, null),
                    Product.Create("Dây Milanese Mesh", "Meridian", catStrap.Id, null),
                    Product.Create("Hộp xoay đồng hồ Winder", "Aurelia", catAccessory.Id, null),
                    Product.Create("Nova Fit Lite", "Nova", catSmart.Id, null),
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // ==================== ProductVariants (18) ====================
            if (!context.ProductVariants.Any())
            {
                int Id(string name) => context.Products.First(p => p.ProductName == name).ProductId;

                // Thông số đồng hồ cơ / quartz: Bộ máy, Chất liệu mặt kính, Chất liệu dây, Đường kính mặt, Chống nước
                KeyValuePair<string, string>[] Traditional(string movement, string crystal, string strap, string diameter, string water) => new[]
                {
                    new KeyValuePair<string, string>("Bộ máy", movement),
                    new KeyValuePair<string, string>("Chất liệu mặt kính", crystal),
                    new KeyValuePair<string, string>("Chất liệu dây", strap),
                    new KeyValuePair<string, string>("Đường kính mặt", diameter),
                    new KeyValuePair<string, string>("Chống nước", water),
                };

                // Thông số smartwatch: Kích thước màn hình, Loại màn hình, Dung lượng pin, Hệ điều hành, Kết nối, Tương thích thiết bị
                KeyValuePair<string, string>[] Smart(string screen, string battery, string connectivity, string compat) => new[]
                {
                    new KeyValuePair<string, string>("Kích thước màn hình", screen),
                    new KeyValuePair<string, string>("Loại màn hình", "AMOLED"),
                    new KeyValuePair<string, string>("Dung lượng pin", battery),
                    new KeyValuePair<string, string>("Hệ điều hành", "NovaOS"),
                    new KeyValuePair<string, string>("Kết nối", connectivity),
                    new KeyValuePair<string, string>("Tương thích thiết bị", compat),
                };

                KeyValuePair<string, string>[] Strap(string material, string width, string buckle) => new[]
                {
                    new KeyValuePair<string, string>("Chất liệu", material),
                    new KeyValuePair<string, string>("Độ rộng dây", width),
                    new KeyValuePair<string, string>("Loại khóa", buckle),
                };

                var variants = new[]
                {
                    // 0-1: Heritage 40 Automatic
                    ProductVariant.Create("Đen", Traditional("Automatic (cơ tự lên dây)", "Sapphire", "Da bê", "40mm", "5ATM"), 8900000, 30, null, Id("Heritage 40 Automatic")),
                    ProductVariant.Create("Xanh navy", Traditional("Automatic (cơ tự lên dây)", "Sapphire", "Thép không gỉ", "40mm", "5ATM"), 9900000, 15, null, Id("Heritage 40 Automatic")),

                    // 2: Skeleton 42 Automatic
                    ProductVariant.Create("Bạc", Traditional("Automatic lộ cơ", "Sapphire", "Thép không gỉ", "42mm", "5ATM"), 14500000, 10, null, Id("Skeleton 42 Automatic")),

                    // 3-4: Classic 38 Quartz
                    ProductVariant.Create("Trắng", Traditional("Quartz Nhật Bản", "Kính khoáng", "Da bò", "38mm", "3ATM"), 2490000, 80, null, Id("Classic 38 Quartz")),
                    ProductVariant.Create("Đen", Traditional("Quartz Nhật Bản", "Kính khoáng", "Thép lưới Milanese", "38mm", "3ATM"), 2890000, 50, null, Id("Classic 38 Quartz")),

                    // 5-6: Dress Slim 36
                    ProductVariant.Create("Vàng hồng", Traditional("Quartz Nhật Bản", "Kính khoáng", "Thép không gỉ", "36mm", "3ATM"), 3290000, 40, null, Id("Dress Slim 36")),
                    ProductVariant.Create("Bạc", Traditional("Quartz Nhật Bản", "Kính khoáng", "Thép không gỉ", "36mm", "3ATM"), 3090000, 25, null, Id("Dress Slim 36")),

                    // 7-8: Digital Sport DS-1
                    ProductVariant.Create("Đen", new[]
                    {
                        new KeyValuePair<string, string>("Loại hiển thị", "LCD"),
                        new KeyValuePair<string, string>("Chất liệu vỏ", "Nhựa resin"),
                        new KeyValuePair<string, string>("Chống nước", "10ATM"),
                        new KeyValuePair<string, string>("Chức năng", "Bấm giờ, báo thức, đèn nền"),
                    }, 890000, 100, null, Id("Digital Sport DS-1")),
                    ProductVariant.Create("Xanh quân đội", new[]
                    {
                        new KeyValuePair<string, string>("Loại hiển thị", "LCD"),
                        new KeyValuePair<string, string>("Chất liệu vỏ", "Nhựa resin"),
                        new KeyValuePair<string, string>("Chống nước", "10ATM"),
                        new KeyValuePair<string, string>("Chức năng", "Bấm giờ, báo thức, đèn nền"),
                    }, 890000, 80, null, Id("Digital Sport DS-1")),

                    // 9: Retro LCD R-90
                    ProductVariant.Create("Bạc", new[]
                    {
                        new KeyValuePair<string, string>("Loại hiển thị", "LCD"),
                        new KeyValuePair<string, string>("Chất liệu vỏ", "Thép không gỉ"),
                        new KeyValuePair<string, string>("Chống nước", "3ATM"),
                        new KeyValuePair<string, string>("Chức năng", "Bấm giờ, báo thức, lịch ngày"),
                    }, 1290000, 200, null, Id("Retro LCD R-90")),

                    // 10: Nova Watch S1
                    ProductVariant.Create("Đen", Smart("1.43 inch", "300mAh", "Bluetooth 5.3, GPS", "Android & iOS"), 4990000, 30, null, Id("Nova Watch S1")),

                    // 11-12: Nova Watch Pro
                    ProductVariant.Create("Bạc", Smart("1.45 inch", "450mAh", "Bluetooth 5.3, GPS, eSIM", "Android & iOS"), 8990000, 20, null, Id("Nova Watch Pro")),
                    ProductVariant.Create("Đen titanium", Smart("1.45 inch", "450mAh", "Bluetooth 5.3, GPS, eSIM", "Android & iOS"), 9990000, 12, null, Id("Nova Watch Pro")),

                    // 13-14: Dây da bò Classic
                    ProductVariant.Create("Nâu", Strap("Da bò thật", "20mm", "Khóa gài thép"), 590000, 150, null, Id("Dây da bò Classic")),
                    ProductVariant.Create("Đen", Strap("Da bò thật", "22mm", "Khóa gài thép"), 650000, 120, null, Id("Dây da bò Classic")),

                    // 15: Dây Milanese Mesh
                    ProductVariant.Create("Bạc", Strap("Thép không gỉ dạng lưới", "20mm", "Khóa từ"), 450000, 90, null, Id("Dây Milanese Mesh")),

                    // 16: Hộp xoay đồng hồ Winder
                    ProductVariant.Create("Đen", new[]
                    {
                        new KeyValuePair<string, string>("Sức chứa", "2 đồng hồ"),
                        new KeyValuePair<string, string>("Chất liệu", "Da PU"),
                        new KeyValuePair<string, string>("Nguồn", "Pin hoặc cáp USB"),
                    }, 1490000, 60, null, Id("Hộp xoay đồng hồ Winder")),

                    // 17: Nova Fit Lite
                    ProductVariant.Create("Xám", Smart("1.3 inch", "250mAh", "Bluetooth 5.2", "Android & iOS"), 2490000, 45, null, Id("Nova Fit Lite")),
                };

                context.ProductVariants.AddRange(variants);
                context.SaveChanges();
            }

            // ==================== Promotions (10) ====================
            if (!context.Promotions.Any())
            {
                var variants = context.ProductVariants.ToList();
                var now = DateTime.UtcNow;

                var promotions = new List<Promotion>();

                // 1. Active - Percentage Discount (Flash Sale)
                promotions.Add(new Promotion().Create(
                    "Active - Percentage Discount (Flash Sale)",
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
                        "Active - Fixed Amount (Seasonal Sale)",
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
                    "Upcoming - Percentage (Pre-order)",
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
                        "Upcoming - Fixed Amount (Early Bird)",
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
                    "Expired - Percentage (Past Promotion)",
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
                        "Expired - Fixed Amount (Past Promotion)",
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
                        "Active - No time limit (Always on sale)",
                        PromotionTypeEnum.FixedAmount,
                        salePrice: Math.Round(variant7.Price * 0.88m, 2),
                        maxReservedStock: 30,
                        isLimitedTime: false,
                        startAt: null,
                        endAt: null
                    ));
                }

                // 8. Disabled promotion
                var promoDisabled = new Promotion().Create(
                    "Disabled promotion",
                    PromotionTypeEnum.Percentage,
                    percentage: 25,
                    maxReservedStock: 10,
                    isLimitedTime: true,
                    startAt: now.AddMinutes(-10),
                    endAt: now.AddHours(2)
                );
                promoDisabled.IsDisabled = true;
                promotions.Add(promoDisabled);

                // 9. Active - Percentage, no time limit (Clearance Sale)
                promotions.Add(new Promotion().Create(
                    "Active - Percentage (Clearance Sale)",
                    PromotionTypeEnum.Percentage,
                    percentage: 10,
                    maxReservedStock: 50,
                    isLimitedTime: false,
                    startAt: null,
                    endAt: null
                ));

                // 10. Upcoming - Fixed Amount (Holiday Prep)
                var variant16 = variants.ElementAtOrDefault(16);
                if (variant16 != null)
                {
                    promotions.Add(new Promotion().Create(
                        "Upcoming - Fixed Amount (Holiday Prep)",
                        PromotionTypeEnum.FixedAmount,
                        salePrice: Math.Round(variant16.Price * 0.92m, 2),
                        maxReservedStock: 20,
                        isLimitedTime: true,
                        startAt: now.AddDays(1),
                        endAt: now.AddDays(10)
                    ));
                }

                context.Promotions.AddRange(promotions);
                context.SaveChanges();
            }

            // ==================== Staffs (10) ====================
            if (!context.Staffs.Any())
            {
                var staffPassword = BCrypt.Net.BCrypt.HashPassword("Staff@123");
                var now = DateTime.UtcNow;

                var staffs = new[]
                {
                    new Staff { FullName = "Nguyễn Văn An",    Email = "an.nguyen@timezone.vn",    PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Trần Thị Bình",    Email = "binh.tran@timezone.vn",    PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Lê Hoàng Cường",   Email = "cuong.le@timezone.vn",     PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Phạm Thị Dung",    Email = "dung.pham@timezone.vn",    PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Hoàng Văn Em",     Email = "em.hoang@timezone.vn",     PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = false, IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Vũ Thị Giang",     Email = "giang.vu@timezone.vn",     PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Đặng Văn Hùng",    Email = "hung.dang@timezone.vn",    PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Bùi Thị Kim",      Email = "kim.bui@timezone.vn",      PasswordHash = staffPassword, Role = RoleEnum.Admin, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Ngô Văn Long",     Email = "long.ngo@timezone.vn",     PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Staff { FullName = "Đỗ Thị Mai",       Email = "mai.do@timezone.vn",       PasswordHash = staffPassword, Role = RoleEnum.Staff, IsActive = true,  IsDeleted = false, CreatedAt = now },
                };

                context.Staffs.AddRange(staffs);
                context.SaveChanges();
            }

            // ==================== Customers (10) ====================
            if (!context.Customers.Any())
            {
                var customerPassword = BCrypt.Net.BCrypt.HashPassword("Customer@123");
                var now = DateTime.UtcNow;

                var customers = new[]
                {
                    new Customer { FullName = "Phan Minh Anh",     Email = "anh.phan@gmail.com",     PhoneNumber = "0901000001", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 120,  IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Trịnh Thu Ba",      Email = "ba.trinh@gmail.com",     PhoneNumber = "0901000002", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 45,   IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Lý Gia Bảo",        Email = "bao.ly@gmail.com",       PhoneNumber = "0901000003", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 0,    IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Đinh Thị Cẩm",      Email = "cam.dinh@gmail.com",     PhoneNumber = "0901000004", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 300,  IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Huỳnh Văn Đạt",     Email = "dat.huynh@gmail.com",    PhoneNumber = "0901000005", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 85,   IsActive = false, IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Mai Thị Hoa",       Email = "hoa.mai@gmail.com",      PhoneNumber = "0901000006", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 210,  IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Vương Đức Huy",     Email = "huy.vuong@gmail.com",    PhoneNumber = "0901000007", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 15,   IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Tô Ngọc Lan",       Email = "lan.to@gmail.com",       PhoneNumber = "0901000008", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 500,  IsActive = true,  IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Châu Bảo Long",     Email = "long.chau@gmail.com",    PhoneNumber = "0901000009", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 60,   IsActive = false, IsDeleted = false, CreatedAt = now },
                    new Customer { FullName = "Kiều Thị Ngọc",     Email = "ngoc.kieu@gmail.com",    PhoneNumber = "0901000010", PasswordHash = customerPassword, Role = RoleEnum.Customer, Points = 175,  IsActive = true,  IsDeleted = false, CreatedAt = now },
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            // ==================== Orders + OrderItems (10 orders, 15 items) ====================
            if (!context.Orders.Any())
            {
                var customers = context.Customers.OrderBy(c => c.CustomerId).ToList();
                var variants = context.ProductVariants.OrderBy(v => v.CreatedAt).ToList();
                var statuses = new[] { "Pending", "Processing", "Completed", "Cancelled" };

                var orders = new List<Order>();
                for (int i = 0; i < 10; i++)
                {
                    var customer = customers[i % customers.Count];
                    var status = statuses[i % statuses.Length];
                    orders.Add(Order.Create(customer.CustomerId, null, 0m, status));
                }
                context.Orders.AddRange(orders);
                context.SaveChanges();

                var orderItems = new List<OrderItem>
                {
                    OrderItem.Create(orders[0].OrderId, variants[0].ProductVariantId, 1, variants[0].Price),
                    OrderItem.Create(orders[0].OrderId, variants[7].ProductVariantId, 2, variants[7].Price),

                    OrderItem.Create(orders[1].OrderId, variants[3].ProductVariantId, 1, variants[3].Price),

                    OrderItem.Create(orders[2].OrderId, variants[1].ProductVariantId, 1, variants[1].Price),
                    OrderItem.Create(orders[2].OrderId, variants[9].ProductVariantId, 3, variants[9].Price),

                    OrderItem.Create(orders[3].OrderId, variants[5].ProductVariantId, 1, variants[5].Price),

                    OrderItem.Create(orders[4].OrderId, variants[10].ProductVariantId, 1, variants[10].Price),
                    OrderItem.Create(orders[4].OrderId, variants[16].ProductVariantId, 2, variants[16].Price),

                    OrderItem.Create(orders[5].OrderId, variants[2].ProductVariantId, 1, variants[2].Price),

                    OrderItem.Create(orders[6].OrderId, variants[11].ProductVariantId, 1, variants[11].Price),
                    OrderItem.Create(orders[6].OrderId, variants[8].ProductVariantId, 1, variants[8].Price),

                    OrderItem.Create(orders[7].OrderId, variants[13].ProductVariantId, 1, variants[13].Price),

                    OrderItem.Create(orders[8].OrderId, variants[4].ProductVariantId, 2, variants[4].Price),
                    OrderItem.Create(orders[8].OrderId, variants[9].ProductVariantId, 1, variants[9].Price),

                    OrderItem.Create(orders[9].OrderId, variants[17].ProductVariantId, 1, variants[17].Price),
                };

                context.OrderItems.AddRange(orderItems);
                context.SaveChanges();

                // Recalculate totals based on order items just created
                foreach (var order in orders)
                {
                    var total = orderItems
                        .Where(oi => oi.OrderId == order.OrderId)
                        .Sum(oi => oi.Quantity * oi.PriceAtPurchase);
                    order.Update(null, total, order.Status);
                }
                context.SaveChanges();
            }

            // ==================== ProductReviews (10) ====================
            if (!context.ProductReviews.Any())
            {
                var customers = context.Customers.OrderBy(c => c.CustomerId).ToList();
                var products = context.Products.OrderBy(p => p.ProductId).ToList();

                var reviews = new[]
                {
                    ProductReview.Create(customers[0].CustomerId, products[0].ProductId, 5, "Bộ máy chạy chuẩn, mặt kính sapphire rất đẹp, đeo thoải mái cả ngày."),
                    ProductReview.Create(customers[1].CustomerId, products[4].ProductId, 4, "Đồng hồ bền, chống nước tốt nhưng đèn nền hơi yếu."),
                    ProductReview.Create(customers[2].CustomerId, products[7].ProductId, 5, "Pin dùng được cả tuần, theo dõi GPS chính xác."),
                    ProductReview.Create(customers[3].CustomerId, products[1].ProductId, 3, "Lộ cơ rất ấn tượng nhưng giá hơi cao."),
                    ProductReview.Create(customers[4].CustomerId, products[10].ProductId, 4, "Hộp xoay chạy êm, đủ cho hai chiếc đồng hồ."),
                    ProductReview.Create(customers[5].CustomerId, products[5].ProductId, 5, "Kiểu dáng retro đẹp, giao hàng nhanh."),
                    ProductReview.Create(customers[6].CustomerId, products[8].ProductId, 2, "Dây da hơi cứng lúc mới đeo, cần vài ngày để mềm."),
                    ProductReview.Create(customers[7].CustomerId, products[2].ProductId, 5, "Mặt số 38mm gọn gàng, hợp cổ tay nhỏ, rất đáng mua."),
                    ProductReview.Create(customers[8].CustomerId, products[6].ProductId, 4, "Smartwatch nhỏ gọn, thông báo về điện thoại ổn định."),
                    ProductReview.Create(customers[9].CustomerId, products[11].ProductId, 3, "Ổn trong tầm giá, pin nhỉnh hơn mong đợi nhưng ít chế độ thể thao."),
                };

                context.ProductReviews.AddRange(reviews);
                context.SaveChanges();
            }

            // ==================== CartItems (10) ====================
            if (!context.CartItems.Any())
            {
                var customers = context.Customers.OrderBy(c => c.CustomerId).ToList();
                var variants = context.ProductVariants.OrderBy(v => v.CreatedAt).ToList();

                var cartItems = new[]
                {
                    CartItem.Create(customers[0].CustomerId, variants[2].ProductVariantId, 1),
                    CartItem.Create(customers[0].CustomerId, variants[9].ProductVariantId, 2),
                    CartItem.Create(customers[1].CustomerId, variants[5].ProductVariantId, 1),
                    CartItem.Create(customers[2].CustomerId, variants[16].ProductVariantId, 3),
                    CartItem.Create(customers[3].CustomerId, variants[1].ProductVariantId, 1),
                    CartItem.Create(customers[4].CustomerId, variants[7].ProductVariantId, 2),
                    CartItem.Create(customers[5].CustomerId, variants[13].ProductVariantId, 1),
                    CartItem.Create(customers[6].CustomerId, variants[10].ProductVariantId, 1),
                    CartItem.Create(customers[7].CustomerId, variants[15].ProductVariantId, 1),
                    CartItem.Create(customers[8].CustomerId, variants[17].ProductVariantId, 2),
                };

                context.CartItems.AddRange(cartItems);
                context.SaveChanges();
            }
        }
    }
}
