using smart_class.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Core.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task<Course> AddCourseAsync(Course course);
        Task<Course?> UpdateCourseAsync(int id, Course course);
        Task<Course?> DeleteAsync(int id);
    }
}