using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public interface IDonHangRepository : IRepository<DonHang>
    {
        void AddListSanPham(List<SanPham> list, int idDonHang);
    }
}
