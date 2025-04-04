
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.ViewModels;

public interface INguoiDungRepository : IRepository<NguoiDung>
{
    IEnumerable<NguoiDung> GetNguoiDungByVaiTro(int vaiTroId);
    NguoiDung GetNguoiDungWithVaiTro(int id);
    CuaHang GetCuaHangByNguoiDungId(int nguoiDungId);
    bool ToggleTrangThai(int id);
    bool ChangeVaiTro(int id, int vaiTroId);

    IEnumerable<NguoiDungViewModel> GetAllWithDetails();
    NguoiDungViewModel GetNguoiDungViewModelById(int id); 
}
