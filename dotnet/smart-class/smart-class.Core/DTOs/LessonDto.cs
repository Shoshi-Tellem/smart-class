using File = smart_class.Core.Entities.File;

namespace smart_class.Core.DTOs
{
    public class LessonDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        IEnumerable<File> Files { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}