using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public interface IStoreRepository : IRepository<CuaHang>
    {
        int GetIdStoreByIdUser(int id);
    }
}
