using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Helpers;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class DonHangController : Controller
    {
        private readonly IDonHangRepository _repository;
        private readonly DbContextApp _context;
        public DonHangController(IDonHangRepository repository, DbContextApp context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Index()
        {
            var donHangs = _repository.GetAll();
            return View(donHangs);
        }
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            DonHang donHang = new DonHang();
            donHang.Id_Nguoi_Mua = userId;
            donHang.Tong_Tien = CartHelper.GetCartPrice(HttpContext.Session);
            donHang.Trang_Thai_Id = 1;
            donHang.Ngay_Tao = DateTime.Now;

            _repository.Add(donHang);

            var listSanPham = CartHelper.GetCart(HttpContext.Session);
            _repository.AddListSanPham(listSanPham, donHang.Id);

            return View();
        }

        public IActionResult Details(int id)
        {
            var chiTietDonHangs = _context.ChiTietDonHangs
                .Where(ct => ct.Id_Don_Hang == id)
                .Include(ct => ct.SanPham)
                .ToList();

            return View(chiTietDonHangs);
        }
    }
}
