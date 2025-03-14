
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories;

public interface ISanPhamRepository : IRepository<SanPham>
{
    IEnumerable<SanPham> GetSanPhamsByDanhMucId(int danhMucId);
}
