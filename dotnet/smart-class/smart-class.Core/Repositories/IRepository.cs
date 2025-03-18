using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T t);
        Task<T> UpdateAsync(T t);
        Task<T?> DeleteAsync(T t);
    }
}