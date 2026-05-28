# EDW - Electronic Device E-Commerce Website
**Môn học:** PRN222 - Lập trình C# và .NET  
**Nhóm:** PRN222-G2  
**Học kỳ:** Summer 2026  

---

## 📝 Giới thiệu Dự án
Dự án **EDW (Electronic Device Website)** là một hệ thống website thương mại điện tử chuyên kinh doanh các sản phẩm công nghệ (Điện thoại, Laptop, Phụ kiện...). Hệ thống hỗ trợ quản lý sâu sắc thuộc tính sản phẩm công nghệ (RAM, CPU, Dung lượng, Màu sắc) thông qua mô hình Product-Variant, đồng thời tích hợp cổng thanh toán trực tuyến VNPAY.

### Hệ thống gồm 3 phân hệ chính (Presentation):
1. **Admin Dashboard (Blazor Server/WebAssembly):** Dành cho Quản trị viên quản lý danh mục, sản phẩm, cấu hình và tài khoản nhân viên.
2. **Staff Portal (Razor Pages):** Dành cho Nhân viên vận hành xử lý đơn hàng nhanh, quản lý kho và cập nhật trạng thái vận chuyển.
3. **Customer Storefront (ASP.NET Core MVC):** Dành cho Khách hàng xem sản phẩm, giỏ hàng, đặt hàng và thanh toán qua VNPAY.

---

## 🏗️ Kiến trúc Hệ thống (Architectural Pattern)
Dự án áp dụng mô hình kiến trúc **3 lớp (3-Tier Architecture)** chuẩn chỉ, tách biệt mối quan tâm và tuân thủ các nguyên tắc SOLID:

* **Presentation Layer (Web UI):** Chứa các project giao diện (`Admin.Blazor`, `Customer.Mvc`...). Chỉ gọi xuống tầng BLL thông qua Dependency Injection.
* **BLL (Business Logic Layer):** Lớp xử lý nghiệp vụ, tính toán logic hệ thống. Tầng này độc lập hoàn toàn với tầng giao diện.
* **DAL (Data Access Layer):** Lớp chịu trách nhiệm kết nối Cơ sở dữ liệu. Sử dụng Entity Framework Core với Fluent API tách file cấu hình riêng (`IEntityTypeConfiguration`) và tự động chuẩn hóa tên bảng theo định dạng `snake_case`.

---

## 🛠️ Công nghệ Sử dụng (Tech Stack)
* **Backend:** .NET 9, C#
* **ORM:** Entity Framework Core
* **Database:** SQL Server
* **Naming Convention:** `EFCore.NamingConventions` (snake_case)
* **Payment Gateway:** VNPAY Sandbox API

---

## 🚀 Hướng dẫn Cài đặt & Chạy Dự án (Dành cho thành viên nhóm)

### 1. Chuẩn bị môi trường
* Đảm bảo máy đã cài **.NET SDK** mới nhất và **SQL Server**.
* Clone dự án về máy: `git clone <url-repository>`

### 2. Tạo file cấu hình thanh toán VNPAY cá nhân
Do lý do bảo mật, file cấu hình VNPAY đã bị đưa vào `.gitignore`. Mỗi thành viên cần tạo một file tên là **`vnpay.json`** nằm ở thư mục gốc của các project Giao diện (Presentation) với nội dung mẫu như sau:

```json
{
  "VNPAY": {
    "TmnCode": "EXAMPLET7",
    "HashSecret": "ABCDEFGHJKLMNOPQRSTUVWXYZABCDEFG",
    "BaseUrl": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
    "ReturnUrl": "https://localhost:7001/Order/VnpayReturn"
  }
}
```

### 3. Cấu hình Chuỗi kết nối Database
Mở file `appsettings.json` ở project UI và sửa lại `ConnectionStrings` cho khớp với SQL Server trên máy của bạn. Ví dụ:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EDW_ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 4. Khởi tạo Cơ sở dữ liệu (Migrations)
Thực hiện theo các bước sau (Visual Studio hoặc CLI):

1. Mở **Package Manager Console** (Visual Studio) hoặc dùng CLI `dotnet-ef`.
2. Chọn **Default Project** là project chứa `DbContext` (thường là DAL).
3. Đặt project chạy chính (startup) là project UI phù hợp (ví dụ `Admin.Blazor`).

Ví dụ (Package Manager Console):

```powershell
# Trong Visual Studio: chọn Default Project = <Project.DAL>
Update-Database
```

Hoặc dùng CLI `dotnet ef`:

```bash
dotnet ef database update --project src/Project.DAL --startup-project src/Admin.Blazor
```

Chú ý: thay `--project` và `--startup-project` theo cấu trúc thực tế của repository.

## 👥 Thành viên Thực hiện (Project Team)
- Đái Minh Như (Leader) - Email: nhudm.ce190213@gmail.com
- [Thêm tên các thành viên khác vào đây...]

Khi bạn thêm file cấu hình (ví dụ `vnpay.json`) vào repository cục bộ, nhớ **không** commit các file chứa secret lên GitHub — thêm vào `.gitignore` nếu cần.