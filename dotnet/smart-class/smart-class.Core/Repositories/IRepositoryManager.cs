using smart_class.Core.Entities;
using File = smart_class.Core.Entities.File;

namespace smart_class.Core.Repositories
{
    public interface IRepositoryManager
    {
        IRepository<Institution> InstitutionRepository { get; }
        IRepository<Admin> AdminRepository { get; }
        IRepository<Teacher> TeacherRepository { get; }
        IRepository<Student> StudentRepository { get; }
        IRepository<Group> GroupRepository { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<Lesson> LessonRepository { get; }
        IRepository<File> FileRepository { get; }

        Task SaveAsync();
    }
}
