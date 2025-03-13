using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Models;
using System.Threading.Tasks;
using ThuongMaiDienTu.Data;

namespace ThuongMaiDienTu.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }
    public async Task<T> GetById(object id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public async Task Delete(object id)
    {
        T entity = await GetById(id);
        _dbSet.Remove(entity);
    }
    public async Task Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

}
