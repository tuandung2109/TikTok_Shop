using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Data
{
    public class DbContextApp : DbContext
    {
        public DbContextApp(DbContextOptions<DbContextApp> options) : base(options) { }

        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<CuaHang> CuaHangs { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<DonHang> DonHangs { get; set;}
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<TrangThaiDonHang> TrangThaiDonHangs{ get; set; }
        public DbSet<TrangThaiThanhToan> TrangThaiThanhToans{ get; set; }
        public DbSet<TrangThaiVanChuyen> TrangThaiVanChuyens { get; set; }
        public DbSet<VanChuyen> VanChuyens{ get; set; }

    }
}
