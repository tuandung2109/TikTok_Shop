// 1.Input: Mật khẩu và salt.  2.Salt: Tạo một salt ngẫu nhiên.  3.Cost Factor: Sử dụng số vòng băm (cost factor), mặc định là 10 hoặc 12 vòng băm, tùy thuộc vào mức độ bảo mật yêu cầu. 4.Băm: Áp dụng hàm băm qua nhiều vòng lặp, mỗi lần vòng lặp sử dụng đầu ra của lần băm trước. 5.Output: Kết quả là một giá trị băm (hash) chứa salt, cost factor, và băm mật khẩu.
// $2a$: Phân loại Bcrypt (phiên bản của thuật toán).    12: Cost factor(ở đây là 2^12 vòng lặp).   eKn2jTKGqA3f0GfV5QF8t.: Salt.   xvR5vO / q52LRzLs3YP4FIE9p5UykFOm: Băm mật khẩu.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;
using BCrypt.Net; // Thêm dòng này

namespace ThuongMaiDienTu.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<NguoiDung> _repository;
        private readonly DbContextApp _context;
        public AccountController(IRepository<NguoiDung> repository, DbContextApp context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var nguoiDung = _repository.GetAll().Where(nd => nd.So_Dien_Thoai == username || nd.Email == username);

            if (nguoiDung.Any(nd => nd.Mat_Khau == password))
            {
                var user = nguoiDung.First(nd => nd.Mat_Khau == password);
                var userId = user.Id;
                HttpContext.Session.SetInt32("UserId", userId);
                HttpContext.Session.SetInt32("VaiTroId", user.Vai_Tro_Id); // Đảm bảo lưu VaiTroId

                var checkSeller = user.Vai_Tro_Id == 2;
                var checkAdmin = user.Vai_Tro_Id == 3;

                if (checkSeller)
                {
                    HttpContext.Session.SetInt32("IsSeller", 1);
                    if (!_context.CuaHangs.Any(ch => ch.Id_Nguoi_Ban == userId))
                    {
                        return RedirectToAction("Create", "Store");
                    }
                    else
                    {
                        var cuaHang = _context.CuaHangs.FirstOrDefault(ch => ch.Id_Nguoi_Ban == userId);
                        HttpContext.Session.SetInt32("StoreId", cuaHang.Id);
                    }
                }

                if (checkAdmin)
                {
                    HttpContext.Session.SetInt32("IsAdmin", 1); // Đảm bảo lưu IsAdmin
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu sai!";
            return View();
        }
        [HttpPost]
        public IActionResult Register([FromBody] NguoiDung nguoiDung)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ: " + string.Join(", ", errors) });
                }

                // Kiểm tra email đã tồn tại
                if (_context.NguoiDungs.Any(u => u.Email == nguoiDung.Email))
                {
                    return Json(new { success = false, message = "Email đã được sử dụng" });
                }

                // Kiểm tra số điện thoại đã tồn tại
                if (_context.NguoiDungs.Any(u => u.So_Dien_Thoai == nguoiDung.So_Dien_Thoai))
                {
                    return Json(new { success = false, message = "Số điện thoại đã được sử dụng" });
                }

                // Kiểm tra Vai_Tro_Id hợp lệ
                if (!_context.VaiTros.Any(v => v.Id == nguoiDung.Vai_Tro_Id))
                {
                    return Json(new { success = false, message = "Vai trò không hợp lệ" });
                }

                // Lưu mật khẩu trực tiếp (không mã hóa)
                nguoiDung.Mat_Khau = nguoiDung.Mat_Khau;

                // Gán các giá trị mặc định
                nguoiDung.Ngay_Tao = DateTime.Now;
                nguoiDung.Trang_Thai = true;

                _repository.Add(nguoiDung);

                if (nguoiDung.Vai_Tro_Id == 2)
                {
                    return Json(new { success = true, message = "Đăng ký thành công!", seller = true });
                }

                return Json(new { success = true, message = "Đăng ký thành công!", seller = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra: " + ex.Message });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            // Lấy UserId từ Session
            int? userId = HttpContext.Session.GetInt32("UserId");

            // Lấy thông tin người dùng từ database
            var user = _context.NguoiDungs.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound(); // Nếu không tìm thấy người dùng
            }
            
            var checkStore = _context.CuaHangs.Count(u => u.Id_Nguoi_Ban == userId);

            if (user.Vai_Tro_Id == 2 && checkStore == 0)
            {
                HttpContext.Session.SetInt32("SellerId", user.Id);
                ViewBag.NoStore = true;
            }

            return View(user);
        }

        public IActionResult EditProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var nguoiDung = _context.NguoiDungs.Find(userId);
            return View(nguoiDung);
        }

        [HttpPatch]
        public IActionResult EditProfile([FromBody] NguoiDung nguoiDung)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            var user = _context.NguoiDungs.FirstOrDefault(u => u.Id == userId);

            // Kiểm tra email đã tồn tại
            if (_context.NguoiDungs.Where(u => u.Email != nguoiDung.Email).Any(u => u.Email == nguoiDung.Email))
            {
                return Json(new { success = false, message = "Email đã được sử dụng" });
            }

            user.Ho_Ten = nguoiDung.Ho_Ten;
            user.Email = nguoiDung.Email;
            user.So_Dien_Thoai = nguoiDung.So_Dien_Thoai;

            _repository.Update(user);
            return Json(new { success = true, message = "Sửa thông tin thành công!" });
        }
    }
}
