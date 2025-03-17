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
}
