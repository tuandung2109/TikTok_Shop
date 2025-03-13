using System;
using System.Linq.Expressions;

namespace ThuongMaiDienTu.Repositories;
// <T> là một kiểu dữ liệu tổng quát, có thể là Product, Order, User...
// where T : class đảm bảo T phải là một class, không phải kiểu int, string...
public interface IRepository<T> where T : class
{
    // Lấy tất cả dữ liệu
    Task<IEnumerable<T>> GetAll();
    // Lấy dữ liệu theo điều kiện
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression);
    // Lấy dữ liệu theo id
    Task<T> GetById(object id);
    // Thêm dữ liệu
    Task Add(T entity);
    // Xóa dữ liệu
    Task Delete(object id);
    // Cập nhật dữ liệu
    Task Update(T entity);
    // Lưu thay đổi
    Task Save();
}
