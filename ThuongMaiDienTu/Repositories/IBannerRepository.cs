using System.Collections.Generic;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public interface IBannerRepository
    {
        List<Banner> GetAllBanners();
    }
}
