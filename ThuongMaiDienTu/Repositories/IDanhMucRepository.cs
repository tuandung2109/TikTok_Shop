using System.Collections.Generic;
using ThuongMaiDienTu.Models;

public interface IDanhMucRepository
{
    List<DanhMuc> GetAllDanhMucs();
    Task<DanhMuc?> GetDanhMucByIdAsync(int id);
    Task AddDanhMucAsync(DanhMuc danhMuc);
    Task UpdateDanhMucAsync(DanhMuc danhMuc);
    Task DeleteDanhMucAsync(int id);
    Task UpdateTrangThai(int id);
}
