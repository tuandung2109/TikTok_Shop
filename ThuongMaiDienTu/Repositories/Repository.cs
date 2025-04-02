using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;

namespace ThuongMaiDienTu.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContextApp _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContextApp context) //contrucstor 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAllQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id); //tìm trong bộ nhớ cache của dbContext trước, nếu không có thì mới truy vấn vào db
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList(); //thực thi truy vấn ngay lập tức và tải tất cả kết quả vào bộ nhớ 
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
