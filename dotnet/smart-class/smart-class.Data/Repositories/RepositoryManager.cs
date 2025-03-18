using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using File = smart_class.Core.Entities.File;

namespace smart_class.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        public IRepository<Institution> InstitutionRepository { get; }
        public IRepository<Admin> AdminRepository { get; }
        public IRepository<Teacher> TeacherRepository { get; }
        public IRepository<Student> StudentRepository { get; }
        public IRepository<Group> GroupRepository { get; }
        public IRepository<Course> CourseRepository { get; }
        public IRepository<Lesson> LessonRepository { get; }
        public IRepository<File> FileRepository { get; }

        public RepositoryManager(DataContext context,
                                 IRepository<Institution> institution,
                                 IRepository<Admin> admin,
                                 IRepository<Teacher> teacher,
                                 IRepository<Student> student,
                                 IRepository<Group> group,
                                 IRepository<Course> course,
                                 IRepository<Lesson> lesson,
                                 IRepository<File> file)
        {
            _context = context;
            InstitutionRepository = institution;
            AdminRepository = admin;
            TeacherRepository = teacher;
            StudentRepository = student;
            GroupRepository = group;
            CourseRepository = course;
            LessonRepository = lesson;
            FileRepository = file;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
