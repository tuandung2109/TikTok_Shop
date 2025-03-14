
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories;

public interface ISanPhamRepository : IRepository<SanPham>
{
    Task<IEnumerable<SanPham>> GetSanPhamsByDanhMucId(int danhMucId);
}
