using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;

namespace smart_class.Service
{
    public class CourseService : ICourseService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _courseRepository = repositoryManager.CourseRepository;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _courseRepository.GetAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            Course addedCourse = await _courseRepository.AddAsync(course);
            await _repositoryManager.SaveAsync();
            return addedCourse;
        }

        public async Task<Course?> UpdateCourseAsync(int id, Course course)
        {
            Course? existingCourse = await GetCourseByIdAsync(id);
            if (existingCourse == null)
                return null;

            existingCourse.Name = course.Name; // Assuming Course has a Name property
            existingCourse.Description = course.Description; // Assuming Course has a Description property
            Course updatedCourse = await _courseRepository.UpdateAsync(existingCourse);
            await _repositoryManager.SaveAsync();
            return updatedCourse;
        }

        public async Task<Course?> DeleteAsync(int id)
        {
            Course? existingCourse = await GetCourseByIdAsync(id);
            if (existingCourse == null)
                return null;

            Course? deletedCourse = await _courseRepository.DeleteAsync(existingCourse);
            await _repositoryManager.SaveAsync();
            return deletedCourse;
        }
    }
}