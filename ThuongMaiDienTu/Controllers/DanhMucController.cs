using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly IDanhMucRepository _danhMucRepository;

        public DanhMucController(IDanhMucRepository danhMucRepository)
        {
            _danhMucRepository = danhMucRepository;
        }

        // GET: DanhMuc
        public ActionResult Index()
        {
            var danhMucs = _danhMucRepository.GetAllDanhMucs();
            return View(danhMucs);
        }

        //GET: DanhMuc/Details/5
        // public async Task<ActionResult> Details(int id)
        // {
        //     var danhMucs = await _danhMucRepository.GetDanhMucByIdAsync(id);
        //     if (danhMucs == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(danhMucs);
        // }

        // GET: DanhMuc/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: DanhMuc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten_Danh_Muc")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                await _danhMucRepository.AddDanhMucAsync(danhMuc);
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }

        // GET: DanhMuc/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }
        // POST: DanhMuc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten_Danh_Muc")] DanhMuc danhMuc)
        {
            if (id != danhMuc.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _danhMucRepository.UpdateDanhMucAsync(danhMuc);
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }

        //GET: DanhMuc/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        //POST: DanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _danhMucRepository.DeleteDanhMucAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //GET: DanhMuc/UpdateTrangThai/5
        public async Task<ActionResult> UpdateTrangThai(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }
        //POST: DanhMuc/UpdateTrangThai/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTrangThaiCF(int id)
        {
            await _danhMucRepository.UpdateTrangThai(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: DanhMuc/ToggleStatus/5
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            await _danhMucRepository.UpdateTrangThai(id);
            
            // Hiển thị thông báo thành công
            string message = danhMuc.Trang_Thai ? 
                $"Đã khóa danh mục {danhMuc.Ten_Danh_Muc}" : 
                $"Đã mở khóa danh mục {danhMuc.Ten_Danh_Muc}";
            
            TempData["StatusMessage"] = message;
            
            return RedirectToAction(nameof(Index));
        }
    }
}
