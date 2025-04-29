using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;

namespace smart_class.Service
{
    public class TeacherService(IRepositoryManager repositoryManager) : ITeacherService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IRepository<Teacher> _teacherRepository = repositoryManager.TeacherRepository;

        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await _teacherRepository.GetAsync();
        }
        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            return await _teacherRepository.GetByIdAsync(id);
        }
        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            Teacher addedTeacher = await _teacherRepository.AddAsync(teacher);
            await _repositoryManager.SaveAsync();
            return addedTeacher;
        }
        public async Task<Teacher?> UpdateTeacherAsync(int id, Teacher teacher)
        {
            Teacher? existingTeacher = await GetTeacherByIdAsync(id);
            if (existingTeacher == null)
                return null;
            existingTeacher.Name = teacher.Name;
            existingTeacher.Email = teacher.Email;
            existingTeacher.UpdatedAt = DateTime.Now;
            Teacher updatedTeacher = await _teacherRepository.UpdateAsync(existingTeacher);
            await _repositoryManager.SaveAsync();
            return updatedTeacher;
        }
        public async Task<Teacher> UpdateTeacherPasswordAsync(int id, string password)
        {
            Teacher? existingTeacher = await GetTeacherByIdAsync(id);
            if (existingTeacher == null)
                return null;
            existingTeacher.Password = password;
            existingTeacher.PasswordChanged = true;
            existingTeacher.UpdatedAt = DateTime.Now;
            Teacher updatedTeacher = await _teacherRepository.UpdateAsync(existingTeacher);
            await _repositoryManager.SaveAsync();
            return updatedTeacher;
        }
        public async Task<Teacher?> DeleteAsync(int id)
        {
            Teacher? existingTeacher = await GetTeacherByIdAsync(id);
            if (existingTeacher == null)
                return null;
            Teacher? deletedTeacher = await _teacherRepository.DeleteAsync(existingTeacher);
            await _repositoryManager.SaveAsync();
            return deletedTeacher;
        }
    }
}