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
        private readonly INguoiDungRepository _nguoiDungRepository; // Thêm repository này

        public DonHangController(IDonHangRepository repository, DbContextApp context, INguoiDungRepository nguoiDungRepository)
        {
            _repository = repository;
            _context = context;
            _nguoiDungRepository = nguoiDungRepository; // Khởi tạo trong constructor
        }

        //public IActionResult Index()
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    var isSeller = HttpContext.Session.GetInt32("IsSeller");
        //    ViewBag.IsSeller = isSeller;

        //    IEnumerable<DonHang> donHangs;
        //    if (isSeller.HasValue && isSeller.Value == 1)
        //    {
        //        donHangs = _repository.GetAllAndInfor(userId);
        //    }
        //    else
        //    {
        //        donHangs = _repository.GetAllAndInfor();
        //    }

        //    return View(donHangs);
        //}


        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var vaiTroId = HttpContext.Session.GetInt32("VaiTroId");

            // Truyền VaiTroId vào ViewBag để sử dụng trong view
            ViewBag.VaiTroId = vaiTroId.HasValue ? vaiTroId.Value : 0;

            if (userId.HasValue && vaiTroId.HasValue && vaiTroId == 1) // Chỉ kiểm tra với người mua
            {
                var nguoiDung = _nguoiDungRepository.GetNguoiDungWithVaiTro(userId.Value);
                if (nguoiDung != null && nguoiDung.Trang_Thai == false)
                {
                    ViewBag.IsLocked = true;
                    return View(new List<DonHang>());
                }
            }

            var isSeller = HttpContext.Session.GetInt32("IsSeller") == 1;
            IEnumerable<DonHang> donHangs;
            if (isSeller)
            {
                donHangs = _context.DonHangs
                    .Include(dh => dh.ChiTietDonHangs).ThenInclude(ct => ct.SanPham).ThenInclude(sp => sp.CuaHang)
                    .Where(dh => dh.ChiTietDonHangs != null && dh.ChiTietDonHangs.Any(ct => ct.SanPham.CuaHang.Id_Nguoi_Ban == userId.Value))
                    .Include(dh => dh.TrangThaiDonHang)
                    .Include(dh => dh.VanChuyens).ThenInclude(vc => vc.TrangThaiVanChuyen)
                    .Include(dh => dh.ThanhToans).ThenInclude(tt => tt.TrangThaiThanhToan)
                    .ToList();
            }
            else
            {
                donHangs = _context.DonHangs
                    .Where(dh => dh.Id_Nguoi_Mua == userId)
                    .Include(dh => dh.TrangThaiDonHang)
                    .Include(dh => dh.VanChuyens).ThenInclude(vc => vc.TrangThaiVanChuyen)
                    .Include(dh => dh.ThanhToans).ThenInclude(tt => tt.TrangThaiThanhToan)
                    .ToList();
            }

            ViewBag.IsSeller = isSeller ? 1 : 0;
            ViewBag.IsLocked = false;
            return View(donHangs);
        }

        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var listSanPham = CartHelper.GetCart(HttpContext.Session);

            foreach (var sanPham in listSanPham)
            {
                // Tạo một đơn hàng mới cho mỗi sản phẩm
                DonHang donHang = new DonHang
                {
                    Id_Nguoi_Mua = userId,
                    Tong_Tien = (decimal)sanPham.Gia_Khuyen_Mai * sanPham.So_Luong_Ton,
                    Trang_Thai_Id = 1,
                    Ngay_Tao = DateTime.Now
                };

                _repository.Add(donHang);

                // Thêm sản phẩm vào chi tiết đơn hàng
                var chiTietDonHang = new ChiTietDonHang
                {
                    Id_Don_Hang = donHang.Id,
                    Id_San_Pham = sanPham.Id,
                    So_Luong = sanPham.So_Luong_Ton,
                    Gia = sanPham.Gia_Khuyen_Mai
                };
                _context.ChiTietDonHangs.Add(chiTietDonHang);

                // Thêm thông tin vận chuyển
                var vanChuyen = new VanChuyen
                {
                    Id_Don_Hang = donHang.Id,
                    Trang_Thai_Id = 1,
                    Ngay_Cap_Nhat = DateTime.Now
                };
                _repository.AddVanChuyen(vanChuyen);

                // Thêm thông tin thanh toán
                var thanhToan = new ThanhToan
                {
                    Id_Don_Hang = donHang.Id,
                    Phuong_Thuc_Id = 1,
                    Trang_Thai_Id = 1,
                    Ngay_Tao = DateTime.Now
                };
                _repository.AddThanhToan(thanhToan);

                // Lưu thay đổi vào database
                _context.SaveChanges();
            }

            // Xóa giỏ hàng sau khi tạo đơn hàng
            //CartHelper.SaveCart(HttpContext.Session, new List<SanPham>());
            //HttpContext.Session.SetInt32("cart_count", 0);

            return RedirectToAction("Index", "DonHang");
        }



        public IActionResult Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var isSeller = HttpContext.Session.GetInt32("IsSeller");
            ViewBag.IsSeller = isSeller; // Thêm dòng này để truyền IsSeller vào view

            var chiTietDonHangs = _context.ChiTietDonHangs
                .Where(ct => ct.Id_Don_Hang == id)
                .Include(ct => ct.SanPham)
                .ToList();

            return View(chiTietDonHangs);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] int orderId)
        {
            var donHang = _repository.GetById(orderId);

            var chitietDonHang = _context.ChiTietDonHangs.Where(ct => ct.Id_Don_Hang == orderId);
            _context.ChiTietDonHangs.RemoveRange(chitietDonHang);

            var vanChuyens = _context.VanChuyens.Where(vc => vc.Id_Don_Hang == orderId);
            _context.VanChuyens.RemoveRange(vanChuyens);

            var thanhToans = _context.ThanhToans.Where(tt => tt.Id_Don_Hang == orderId);
            _context.ThanhToans.RemoveRange(thanhToans);

            _context.DonHangs.Remove(donHang);

            _context.SaveChanges();
            return Json(new { message = "Xóa Thành Công" });
        }

        [HttpPost]
        public IActionResult UpdateStatus([FromBody] int id)
        {
            var donHang = _repository.GetById(id);
            if (donHang == null)
            {
                return NotFound();
            }

            donHang.Trang_Thai_Id = 2;
            _repository.Update(donHang);

            return Json(new { message = "Cập nhật trạng thái thành công" });
        }

        // Lấy danh sách phương thức thanh toán
        [HttpGet]
        public IActionResult GetPaymentMethods()
        {
            var paymentMethods = _context.PhuongThucThanhToans
                .Select(pt => new { pt.Id, pt.Mo_Ta })
                .ToList();
            return Json(paymentMethods);
        }

        // Xử lý thanh toán
        [HttpPost]
        public IActionResult ProcessPayment([FromBody] PaymentRequest request)
        {
            var thanhToan = _context.ThanhToans
                .FirstOrDefault(tt => tt.Id_Don_Hang == request.OrderId);

            if (thanhToan == null)
            {
                return Json(new { success = false, message = "Không tìm thấy thông tin thanh toán" });
            }

            thanhToan.Phuong_Thuc_Id = request.PhuongThucId;
            thanhToan.Trang_Thai_Id = 2; // Hoàn tất
            thanhToan.Ngay_Tao = DateTime.Now;

            _context.ThanhToans.Update(thanhToan);
            _context.SaveChanges();

            return Json(new { success = true, message = "Thanh toán thành công" });
        }
    }

    // Class để nhận dữ liệu từ request
    public class PaymentRequest
    {
        public int OrderId { get; set; }
        public int PhuongThucId { get; set; }
    }
}