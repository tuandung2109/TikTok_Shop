using System.Collections.Generic;
using System.Linq;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

public class DanhMucRepository : IDanhMucRepository
{
    private readonly DbContextApp _context;

    public DanhMucRepository(DbContextApp context)
    {
        _context = context;
    }

    public List<DanhMuc> GetAllDanhMucs()
    {
        return _context.DanhMucs.ToList();
    }

    public async Task<DanhMuc?> GetDanhMucByIdAsync(int id)
    {
        return await _context.DanhMucs.FindAsync(id);
    }
    public async Task AddDanhMucAsync(DanhMuc danhMuc)
    {
        await _context.DanhMucs.AddAsync(danhMuc);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateDanhMucAsync(DanhMuc danhMuc)
    {
        _context.DanhMucs.Update(danhMuc);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteDanhMucAsync(int id)
    {
        var danhMuc = await GetDanhMucByIdAsync(id);
        if (danhMuc != null)
        {
            danhMuc.Trang_Thai = false;
            await _context.SaveChangesAsync();
        }
    }
    public async Task UpdateTrangThai(int id)
    {
        var danhMuc = await GetDanhMucByIdAsync(id);
        if (danhMuc != null)
        {
            danhMuc.Trang_Thai = !danhMuc.Trang_Thai;
            await _context.SaveChangesAsync();
        }
    }

}
