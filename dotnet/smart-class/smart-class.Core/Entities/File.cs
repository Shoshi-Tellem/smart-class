using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_class.Core.Entities
{
    public class File
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }

}