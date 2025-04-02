

### **Thêm `DbContext` vào DI Container trong `Program.cs`**
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=your_server;Database=your_db;Trusted_Connection=True;"));
```
🔹 Điều này giúp **ASP.NET Core tự động tạo và quản lý `DbContext`** mỗi khi cần.

### **Inject `DbContext` vào Controller**
```csharp
public class SanPhamController : Controller
{
    private readonly ApplicationDbContext _context;

    public SanPhamController(ApplicationDbContext context)
    {
        _context = context; // Inject DbContext vào Controller
    }

    public IActionResult Index()
    {
        var sanPhams = _context.SanPhams.ToList();
        return View(sanPhams);
    }
}
```
➡ **ASP.NET Core tự động cung cấp `ApplicationDbContext` khi khởi tạo `SanPhamController`**.

### **Inject vào Repository (Dùng Repository Pattern)**
```csharp
public class SanPhamRepository : ISanPhamRepository
{
    private readonly ApplicationDbContext _context;

    public SanPhamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<SanPham> GetAll()
    {
        return _context.SanPhams.ToList();
    }
}
```
🚀 **Ưu điểm của DI:**
✔ **Tăng tính tái sử dụng & dễ bảo trì**
✔ **Dễ kiểm thử unit test**
✔ **Quản lý vòng đời đối tượng hiệu quả**

## Tin Vắn
DBContext ánh xạ đến bảng trong database bằng DbSet<> 
Thao tác CRUD: 
**lấy ds:**
```
 <tên DBConext>.<TenDBSet>.ToList(); 
 ```
**Thêm:** 
```
<tên DBConext>.<TenDBSet>.Add(đối tượng);
<ten DBContext>.SaveChanges(); 
```
**Sửa:** 
```
var dtg = <tên DBConext>.<TenDBSet>.Find(Pk);
dtg.<thuộc tính> = xxx; 
<tên DBContext>.Update<dtg>; 
<tenDBContect>.SaveChanges(); 
```
**Xóa:**
``` 
var dtg = <tên DBConext>.<TenDBSet>.Find(Pk);
<tenDBContext>.<tenDBSet>.Remove(dtg); 
<tenDBContect>.SaveChanges(); 
```

## **Các Truy Vấn LINQ Cơ Bản trong Entity Framework Core**
### **🔹 Lấy tất cả dữ liệu**
```csharp
var sanPhams = _context.SanPhams.ToList();  // SELECT * FROM SanPhams
```
### **🔹 Lọc dữ liệu (WHERE)**
```csharp
var sanPhams = _context.SanPhams.Where(s => s.Gia > 10000000).ToList();  
// SELECT * FROM SanPhams WHERE Gia > 10000000
```
### **🔹 Lấy một phần tử (FirstOrDefault)**
```csharp
var sanPham = _context.SanPhams.FirstOrDefault(s => s.Id == 1);  
// SELECT TOP 1 * FROM SanPhams WHERE Id = 1
```
### **🔹 Chọn các cột cụ thể (Select)**
```csharp
var tenSanPhams = _context.SanPhams.Select(s => new { s.Ten, s.Gia }).ToList();  
// SELECT Ten, Gia FROM SanPhams
```
### **🔹 Sắp xếp (OrderBy, OrderByDescending)**
```csharp
var sanPhams = _context.SanPhams.OrderBy(s => s.Gia).ToList();  
// SELECT * FROM SanPhams ORDER BY Gia ASC
```
### **🔹 Gộp nhóm (GroupBy)**
```csharp
var sanPhamTheoDanhMuc = _context.SanPhams.GroupBy(s => s.DanhMucId)
    .Select(g => new { DanhMucId = g.Key, SoLuong = g.Count() })
    .ToList();
// SELECT DanhMucId, COUNT(*) AS SoLuong FROM SanPhams GROUP BY DanhMucId
```
### **🔹 Join bảng (Include hoặc LINQ Join)**
```csharp
var sanPhams = _context.SanPhams.Include(s => s.DanhMuc).ToList();
// SELECT * FROM SanPhams INNER JOIN DanhMucs ON SanPhams.DanhMucId = DanhMucs.Id
```
### **🔹 Kiểm tra tồn tại (Any)**
```csharp
bool coSanPham = _context.SanPhams.Any(s => s.Gia > 5000000);  
// SELECT CASE WHEN EXISTS (SELECT * FROM SanPhams WHERE Gia > 5000000) THEN 1 ELSE 0 END
```
### **🔹 Đếm số lượng bản ghi (Count)**
```csharp
int soSanPham = _context.SanPhams.Count();  
// SELECT COUNT(*) FROM SanPhams
```

