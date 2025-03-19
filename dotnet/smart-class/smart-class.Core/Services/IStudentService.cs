using smart_class.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Core.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(Student student);
        Task<Student?> UpdateStudentAsync(int id, Student student);
        Task<Student?> DeleteAsync(int id);
    }
}