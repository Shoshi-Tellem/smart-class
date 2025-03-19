using System;
using System.Collections.Generic;

namespace smart_class.Core.DTOs
{
    public class FileDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public IEnumerable<int> LessonIds { get; set; }
    }
}