namespace ThuongMaiDienTu.Repositories
{
    public interface IRepository<T> where T : class
    {
        //<T>: Đây là tham số kiểu generic, 
        // cho phép interface làm việc với nhiều loại entity khác nhau.
        // T : class: Điều này có nghĩa là T phải là một kiểu tham chiếu (reference type) - class
        IQueryable<T> GetAllQueryable(); // - trả về một ds các đối tượng kiểu T 
        IEnumerable<T> GetAll(); // - trả về một ds các đối tượng kiểu T được tải về bộ nhớ
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
