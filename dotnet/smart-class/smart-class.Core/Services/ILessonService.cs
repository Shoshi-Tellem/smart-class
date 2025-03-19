using smart_class.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Core.Services
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetLessonsAsync();
        Task<Lesson?> GetLessonByIdAsync(int id);
        Task<Lesson> AddLessonAsync(Lesson lesson);
        Task<Lesson?> UpdateLessonAsync(int id, Lesson lesson);
        Task<Lesson?> DeleteAsync(int id);
    }
}