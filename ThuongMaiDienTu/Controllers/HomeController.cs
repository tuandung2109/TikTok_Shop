using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly IBannerRepository _bannerRepository;
        private readonly IDanhMucRepository _danhMucRepository;

        public HomeController(ISanPhamRepository sanPhamRepository, IBannerRepository bannerRepository , IDanhMucRepository danhMucRepository)
        {
            _sanPhamRepository = sanPhamRepository;
            _bannerRepository = bannerRepository;
            _danhMucRepository = danhMucRepository;
        }

        public IActionResult Index(int? danhMucId)
        {
            List<SanPham> sanPhams = _sanPhamRepository.GetAllSanPhams();

            if (danhMucId.HasValue && danhMucId > 0)
            {
                sanPhams = _sanPhamRepository.GetSanPhamByDanhMuc(danhMucId.Value);
            }
            else
            {
                sanPhams = _sanPhamRepository.GetAllSanPhams();
            }

            List<DanhMuc> danhMucs = _danhMucRepository.GetAllDanhMucs();
            List<Banner> banners = _bannerRepository.GetAllBanners();

            ViewBag.DanhMucs = danhMucs;

            ViewBag.Banners = banners; // Gửi dữ liệu banner sang View

            return View(sanPhams);
        }
        public IActionResult TimKiem(string tuKhoa)
        {
            List<SanPham> ketQua = _sanPhamRepository.TimKiemSanPham(tuKhoa);
            ViewBag.TuKhoa = tuKhoa; // Lưu từ khóa để hiển thị lại trên giao diện
            return View(ketQua);

        }

        //[HttpGet]
        //public IActionResult GetSanPhamsByDanhMuc(int danhMucId)
        //{
        //    List<SanPham> sanPhams;

        //    if (danhMucId > 0)
        //    {
        //        sanPhams = _sanPhamRepository.GetSanPhamByDanhMuc(danhMucId);
        //    }
        //    else
        //    {
        //        sanPhams = _sanPhamRepository.GetAllSanPhams();
        //    }

        //    return Json(sanPhams);
        //}
    }
}
