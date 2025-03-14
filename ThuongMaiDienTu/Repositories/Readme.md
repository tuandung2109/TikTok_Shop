# Repository Pattern trong ThuongMaiDienTu

## Giới thiệu
Dự án sử dụng **Repository Pattern** để quản lý truy vấn dữ liệu từ database thông qua Entity Framework Core. Điều này giúp tách biệt logic truy cập dữ liệu với business logic, giúp code dễ bảo trì và mở rộng.

## Cấu trúc Repository

Dự án có hai lớp Repository chính:

1. **IRepository<T>**: Interface chung cho tất cả các repository.
2. **Repository<T>**: Cài đặt chung cho các repository thao tác với database.
3. **ISanPhamRepository**: Interface riêng cho bảng `san_pham`.
4. **SanPhamRepository**: Cài đặt repository dành riêng cho `san_pham`.

### Interface `IRepository<T>`
```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(int id);
}
```

### Cài đặt `Repository<T>`
```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

    public async Task<T?> GetById(int id) => await _dbSet.FindAsync(id);

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
```

### Interface `ISanPhamRepository`
```csharp
public interface ISanPhamRepository : IRepository<SanPham>
{
    Task<IEnumerable<SanPham>> GetByCategory(int danhMucId);
}
```

### Cài đặt `SanPhamRepository`
```csharp
public class SanPhamRepository : Repository<SanPham>, ISanPhamRepository
{
    public SanPhamRepository(ApplicationDbContext context) : base(context) {}

    public async Task<IEnumerable<SanPham>> GetByCategory(int danhMucId)
    {
        return await _dbSet.Where(sp => sp.DanhMucId == danhMucId).ToListAsync();
    }
}
```

## Cấu hình Repository trong `Program.cs`
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();
```

## Cách sử dụng Repository trong Controller
Ví dụ: `SanPhamController`
```csharp
public class SanPhamController : Controller
{
    private readonly ISanPhamRepository _sanPhamRepository;

    public SanPhamController(ISanPhamRepository sanPhamRepository)
    {
        _sanPhamRepository = sanPhamRepository;
    }

    public async Task<IActionResult> Index()
    {
        var sanPhams = await _sanPhamRepository.GetAll();
        return View(sanPhams);
    }
}
```

## Lợi ích của Repository Pattern
- **Tách biệt logic dữ liệu**: Controller không trực tiếp truy vấn database.
- **Dễ dàng mở rộng**: Thêm chức năng mới mà không ảnh hưởng đến code cũ.
- **Dễ dàng kiểm thử**: Có thể mock repository khi test.

## Tổng kết
Repository Pattern giúp code rõ ràng, dễ bảo trì và mở rộng. Nếu có góp ý hoặc cần bổ sung, vui lòng cập nhật file này! 🚀
