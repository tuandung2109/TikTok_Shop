
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories;

public interface ISanPhamRepository : IRepository<SanPham>
{
    List<SanPham> GetAllSanPhams();
    List<SanPham> TimKiemSanPham(string tuKhoa);

    public interface ISanPhamRepository
    {
        List<SanPham> GetAllSanPhams();
        IEnumerable<SanPham> GetSanPhamsByDanhMucId(int danhMucId); // Lọc theo danh mục

        List<SanPham> TimKiemSanPham(string tuKhoa); // ✨ Hàm tìm kiếm sản phẩm
    }
    //    IEnumerable<SanPham> GetSanPhamsByDanhMucId(int danhMucId);
    //    string? GetAllSanPhams();
}
