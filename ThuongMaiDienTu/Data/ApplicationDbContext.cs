using System;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<NguoiDung> NguoiDung { get; set; }
    public DbSet<VaiTro> VaiTro { get; set; }
    public DbSet<CuaHang> CuaHang { get; set; }
    public DbSet<DanhMuc> DanhMuc { get; set; }
    public DbSet<SanPham> SanPham { get; set; }

}
