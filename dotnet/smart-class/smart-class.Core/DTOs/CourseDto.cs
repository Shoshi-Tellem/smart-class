using smart_class.Core.Entities;
using System;
using System.Collections.Generic;

namespace smart_class.Core.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}