using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public class StoreRepository : Repository<CuaHang>, IStoreRepository
    {
        private readonly DbContextApp _context;
        public StoreRepository(DbContextApp context) : base(context)
        {
            _context = context;
        }
        public int GetIdStoreByIdUser(int id)
        {
            return _context.CuaHangs.Where(ch => ch.Id_Nguoi_Ban == id).FirstOrDefault().Id; 
        }
    }
}
