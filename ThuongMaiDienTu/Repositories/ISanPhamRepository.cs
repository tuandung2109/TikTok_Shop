
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories;

public interface ISanPhamRepository : IRepository<SanPham>
{
    IEnumerable<SanPham> GetSanPhamsByDanhMucId(int danhMucId);

    List<SanPham> GetAllSanPhams();
    List<SanPham> TimKiemSanPham(string tuKhoa);

    // ✅ Thêm phương thức lấy sản phẩm kèm đánh giá
    SanPham GetSanPhamWithDanhGia(int id);

    List<SanPham> GetSanPhamByDanhMuc(int danhMucId);

}
