using smart_class.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Core.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<Teacher> AddTeacherAsync(Teacher teacher);
        Task<Teacher?> UpdateTeacherAsync(int id, Teacher teacher);
        Task<Teacher?> DeleteAsync(int id);
    }
}