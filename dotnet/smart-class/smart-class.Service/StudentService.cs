using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;

namespace smart_class.Service
{
    public class StudentService(IRepositoryManager repositoryManager) : IStudentService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IRepository<Student> _studentRepository = repositoryManager.StudentRepository;

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetAsync();
        }
        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }
        public async Task<Student> AddStudentAsync(Student student)
        {
            Student addedStudent = await _studentRepository.AddAsync(student);
            await _repositoryManager.SaveAsync();
            return addedStudent;
        }
        public async Task<Student?> UpdateStudentAsync(int id, Student student)
        {
            Student? existingStudent = await GetStudentByIdAsync(id);
            if (existingStudent == null)
                return null;
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            Student updatedStudent = await _studentRepository.UpdateAsync(existingStudent);
            await _repositoryManager.SaveAsync();
            return updatedStudent;
        }
        public async Task<Student?> DeleteAsync(int id)
        {
            Student? existingStudent = await GetStudentByIdAsync(id);
            if (existingStudent == null)
                return null;
            Student? deletedStudent = await _studentRepository.DeleteAsync(existingStudent);
            await _repositoryManager.SaveAsync();
            return deletedStudent;
        }
    }
}