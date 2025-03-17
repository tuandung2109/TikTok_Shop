using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace ThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly IBannerRepository _bannerRepository;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly ILogger<HomeController> _logger;



        public HomeController(ISanPhamRepository sanPhamRepository, IBannerRepository bannerRepository, IDanhMucRepository danhMucRepository, ILogger<HomeController> logger)
        {
            _sanPhamRepository = sanPhamRepository;
            _bannerRepository = bannerRepository;
            _danhMucRepository = danhMucRepository;
            _logger = logger;
            _sanPhamRepository = sanPhamRepository;
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

        [HttpGet]
        public IActionResult TimKiem(string tuKhoa)
        {
            List<SanPham> ketQua = _sanPhamRepository.TimKiemSanPham(tuKhoa);
            ViewBag.TuKhoa = tuKhoa; // Lưu từ khóa để hiển thị lại trên giao diện
            return View(ketQua);
        }

        public IActionResult ChiTietSanPham(int id)
        {
            var sanPham = _sanPhamRepository.GetById(id); // ⚙️ Dùng đúng tên biến
            if (sanPham == null)
                return NotFound();
            return View(sanPham); // View ChiTietSanPham.cshtml
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
