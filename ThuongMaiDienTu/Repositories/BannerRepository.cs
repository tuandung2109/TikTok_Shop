using System.Collections.Generic;
using System.Linq;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public class BannerRepository : IBannerRepository
    {
        private readonly DbContextApp _context;

        public BannerRepository(DbContextApp context)
        {
            _context = context;
        }

        public List<Banner> GetAllBanners()
        {
            return _context.Banners.ToList();
        }
    }
}
