using System;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         optionsBuilder.UseSqlServer(@"Server=HONGANHS\HONGANHS;Database=dbTTS;User ID=sa;Password=1234;Encrypt=False;TrustServerCertificate=True");
    //     }

    // }
    public DbSet<NguoiDung> NguoiDung { get; set; }
    public DbSet<VaiTro> VaiTro { get; set; }
    public DbSet<CuaHang> CuaHang { get; set; }
    public DbSet<DanhMuc> DanhMuc { get; set; }
    public DbSet<SanPham> SanPham { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Nếu tên bảng trong DB có dạng snake_case, cần đặt lại tên cho phù hợp
        modelBuilder.Entity<SanPham>().ToTable("san_pham");
        modelBuilder.Entity<DanhMuc>().ToTable("danh_muc");
        modelBuilder.Entity<CuaHang>().ToTable("cua_hang");
        modelBuilder.Entity<NguoiDung>().ToTable("nguoi_dung");
        modelBuilder.Entity<VaiTro>().ToTable("vai_tro");
    }

}
