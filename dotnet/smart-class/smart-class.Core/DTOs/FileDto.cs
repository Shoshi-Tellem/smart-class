using System;
using System.Collections.Generic;

namespace smart_class.Core.DTOs
{
    public class FileDto
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Path { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}