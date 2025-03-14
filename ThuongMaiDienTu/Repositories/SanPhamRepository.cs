using System;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories;

public class SanPhamRepository : Repository<SanPham>, ISanPhamRepository
{
    private readonly ApplicationDbContext _context;
    public SanPhamRepository(ApplicationDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<SanPham>> GetSanPhamsByDanhMucId(int danhMucId)
    {
        return await _context.SanPham.Where(s => s.DanhMucId == danhMucId).ToListAsync();
    }
    public async Task<IEnumerable<SanPham>> GetAll()
    {
        return await _context.SanPham.ToListAsync();
    }
}

