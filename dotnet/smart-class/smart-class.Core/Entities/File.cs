using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_class.Core.Entities
{
    public class File
    {
        public int Id { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }

}