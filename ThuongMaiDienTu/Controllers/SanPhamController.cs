using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.Models;
using System.Threading.Tasks;

namespace ThuongMaiDienTu.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        public SanPhamController(ISanPhamRepository sanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository ?? throw new ArgumentNullException(nameof(sanPhamRepository));
        }
    
        // GET: SanPhamController
        public async Task<IActionResult> Index()
        {
            var sanPhams = await _sanPhamRepository.GetAll();
            return View(sanPhams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                await _sanPhamRepository.Add(sanPham);
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var sanPham = await _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                await _sanPhamRepository.Update(sanPham);
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var sanPham = await _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _sanPhamRepository.GetById(id);
            await _sanPhamRepository.Delete(sanPham);
            return RedirectToAction("Index");
        }
    }
}
