using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;

namespace smart_class.Service
{
    public class LessonService : ILessonService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IRepository<Lesson> _lessonRepository;

        public LessonService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _lessonRepository = repositoryManager.LessonRepository;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            return await _lessonRepository.GetAsync();
        }

        public async Task<Lesson?> GetLessonByIdAsync(int id)
        {
            return await _lessonRepository.GetByIdAsync(id);
        }

        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
            Lesson addedLesson = await _lessonRepository.AddAsync(lesson);
            await _repositoryManager.SaveAsync();
            return addedLesson;
        }

        public async Task<Lesson?> UpdateLessonAsync(int id, Lesson lesson)
        {
            Lesson? existingLesson = await GetLessonByIdAsync(id);
            if (existingLesson == null)
                return null;

            existingLesson.Status = lesson.Status;
            existingLesson.UpdatedAt = DateTime.Now;
            Lesson updatedLesson = await _lessonRepository.UpdateAsync(existingLesson);
            await _repositoryManager.SaveAsync();
            return updatedLesson;
        }

        public async Task<Lesson?> DeleteAsync(int id)
        {
            Lesson? existingLesson = await GetLessonByIdAsync(id);
            if (existingLesson == null)
                return null;

            Lesson? deletedLesson = await _lessonRepository.DeleteAsync(existingLesson);
            await _repositoryManager.SaveAsync();
            return deletedLesson;
        }
    }
}