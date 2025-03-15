using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        
        public SanPhamController(ISanPhamRepository sanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository;
        }
    
        // GET: SanPhamController
        public IActionResult Index()
        {
            var sanPhams =  _sanPhamRepository.GetAll();
            return View(sanPhams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                 _sanPhamRepository.Add(sanPham);
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }
        public IActionResult Edit(int id)
        {
            var sanPham =  _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }
        [HttpPost]
        public IActionResult Edit(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                 _sanPhamRepository.Update(sanPham);
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }
        public IActionResult Delete(int id)
        {
            var sanPham =  _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _sanPhamRepository.Delete(id);
            return RedirectToAction("Index");
        }

        // Thêm luống 2 : 
        public IActionResult ChiTiet(int id)
        {
            var sanPham = _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }
    }
}
