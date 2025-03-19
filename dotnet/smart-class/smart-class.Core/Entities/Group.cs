using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_class.Core.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[ForeignKey("Institution")]
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}