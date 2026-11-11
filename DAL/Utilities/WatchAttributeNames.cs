namespace DAL.Utilities
{
    /// <summary>
    /// Danh sách tên thông số gợi ý cho từng nhóm đồng hồ (hiển thị dạng gợi ý trong form nhập).
    /// Đây chỉ là gợi ý: nhân viên vẫn có thể gõ tên thông số mới mà không cần sửa cấu trúc bảng.
    /// </summary>
    public static class WatchAttributeNames
    {
        public const string Movement = "Bộ máy";
        public const string Crystal = "Chất liệu mặt kính";
        public const string StrapMaterial = "Chất liệu dây";
        public const string CaseDiameter = "Đường kính mặt";
        public const string WaterResistance = "Chống nước";

        public const string ScreenSize = "Kích thước màn hình";
        public const string ScreenType = "Loại màn hình";
        public const string BatteryCapacity = "Dung lượng pin";
        public const string OperatingSystem = "Hệ điều hành";
        public const string Connectivity = "Kết nối";
        public const string Compatibility = "Tương thích thiết bị";

        /// <summary>Gợi ý cho đồng hồ cơ / quartz / điện tử.</summary>
        public static readonly string[] Traditional =
        {
            Movement, Crystal, StrapMaterial, CaseDiameter, WaterResistance,
            "Chất liệu vỏ", "Độ dày thân", "Độ rộng dây", "Dự trữ năng lượng"
        };

        /// <summary>Gợi ý cho smartwatch.</summary>
        public static readonly string[] Smart =
        {
            ScreenSize, ScreenType, BatteryCapacity, OperatingSystem, Connectivity, Compatibility,
            WaterResistance, StrapMaterial, "Cảm biến"
        };

        /// <summary>Tất cả gợi ý (không trùng) để dùng cho thẻ datalist.</summary>
        public static IEnumerable<string> All => Traditional.Concat(Smart).Distinct();
    }
}
