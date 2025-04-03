using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class StoreController : Controller
    {
        private IRepository<CuaHang> _repository;
        private DbContextApp _context;

        public StoreController(IRepository<CuaHang> repository, DbContextApp context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.SellerId = HttpContext.Session.GetInt32("UserId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CuaHang cuaHang)
        {
            if (ModelState.IsValid) {
                _repository.Add(cuaHang);
                var ch = _repository.GetAll().ToList().FirstOrDefault(ch=> ch.Id_Nguoi_Ban == cuaHang.Id_Nguoi_Ban);
                HttpContext.Session.SetInt32("StoreId", ch.Id);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Edit()
        {
            var storeId = (int)HttpContext.Session.GetInt32("StoreId");
            return View(_repository.GetById(storeId));
        }

        [HttpPatch]
        public IActionResult Edit([FromBody]CuaHang cuaHangSua)
        {
            ModelState.Clear();
            TryValidateModel(cuaHangSua, nameof(cuaHangSua.Ten_Cua_Hang));
            TryValidateModel(cuaHangSua, nameof(cuaHangSua.Mo_Ta));
            if (ModelState.IsValid) {
                var storeId = (int)HttpContext.Session.GetInt32("StoreId");
                var cuaHang = _repository.GetById(storeId);
                cuaHang.Ten_Cua_Hang = cuaHangSua.Ten_Cua_Hang;
                cuaHang.Mo_Ta = cuaHangSua.Mo_Ta;

                _repository.Update(cuaHang);
                return Json(new { success = true, message = "Cập nhật thành công!" });
            }
            
            return Json(new { success = false, message = "Cập nhật thất bại" });
        }

        public IActionResult Profile()
        {
            // Lấy UserId từ Session
            int? userId = HttpContext.Session.GetInt32("UserId");

            // Lấy thông tin người dùng từ database
            var store = _repository.GetAll().Where(ch => ch.Id_Nguoi_Ban == userId).FirstOrDefault();
            if (store == null)
            {
                return NotFound(); // Nếu không tìm thấy người dùng
            }

            return View(store);
        }
    }
}
