using System;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories;

public class SanPhamRepository : Repository<SanPham> , ISanPhamRepository
{
    private readonly DbContextApp _context;
    public SanPhamRepository(DbContextApp context) : base(context)
    {
        _context = context;
    }
    public IEnumerable<SanPham> GetSanPhamsByDanhMucId(int danhMucId)
    {
        return _context.SanPhams.Where(s => s.Id_Danh_Muc == danhMucId).ToList();
    }

    public List<SanPham> GetAllSanPhams() // Phải khớp với ISanPhamRepository
    {
        return _context.SanPhams.ToList();
    }

    public List<SanPham> TimKiemSanPham(string tuKhoa)
    {
        return _context.SanPhams
            .Where(s => s.Ten_San_Pham.Contains(tuKhoa) || s.Mo_Ta.Contains(tuKhoa))
            .ToList();
    }

    // ✅ Cập nhật để lấy sản phẩm kèm đánh giá
    public SanPham GetSanPhamWithDanhGia(int id)
    {
        return _context.SanPhams
            .Include(sp => sp.DanhGias) // Lấy danh sách đánh giá
            .ThenInclude(dg => dg.NguoiDung) // Lấy thông tin người đánh giá
            .FirstOrDefault(sp => sp.Id == id);
    }

    public List<SanPham> GetSanPhamByDanhMuc(int danhMucId)
    {
        return _context.SanPhams.Where(sp => sp.Id_Danh_Muc == danhMucId).ToList();
    }
}

