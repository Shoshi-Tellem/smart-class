using Microsoft.EntityFrameworkCore;
using smart_class.Core.Repositories;

namespace smart_class.Data.Repositories
{
    public class Repository<T>(DataContext context) : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T t)
        {
            await _dbSet.AddAsync(t);
            return t;
        }

        public async Task<T> UpdateAsync(T t)
        {
            _dbSet.Update(t);
            return t;
        }

        public async Task<T?> DeleteAsync(T t)
        {
            _dbSet.Remove(t);
            return t;
        }
    }
}