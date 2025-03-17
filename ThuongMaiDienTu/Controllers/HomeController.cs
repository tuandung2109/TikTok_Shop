using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.Models;
using System.Collections.Generic;

namespace ThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;

        public HomeController(ISanPhamRepository sanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository;
        }

        public IActionResult TrangChu()
        {
            List<SanPham> sanPhams = _sanPhamRepository.GetAllSanPhams();
            return View(sanPhams);
        }

        [HttpGet]
        public IActionResult TimKiem(string tuKhoa)
        {
            List<SanPham> ketQua = _sanPhamRepository.TimKiemSanPham(tuKhoa);
            ViewBag.TuKhoa = tuKhoa; // Lưu từ khóa để hiển thị lại trên giao diện
            return View(ketQua);
        }
        // ✅ Đưa ChiTietSanPham vào trong class
        public IActionResult ChiTietSanPham(int id)
        {
            var sanPham = _sanPhamRepository.GetById(id); // ⚙️ Dùng đúng tên biến
            if (sanPham == null)
                return NotFound();
            return View(sanPham); // View ChiTietSanPham.cshtml
        }
    }
}
