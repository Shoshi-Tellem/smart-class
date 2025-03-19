using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_class.Core.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        //[ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        IEnumerable<File> Files { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}