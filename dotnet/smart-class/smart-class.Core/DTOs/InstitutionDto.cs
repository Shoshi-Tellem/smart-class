using System;
using System.Collections.Generic;

namespace smart_class.Core.DTOs
{
    public class InstitutionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<int> AdminIds { get; set; }
        public IEnumerable<int> TeacherIds { get; set; }
        public IEnumerable<int> StudentIds { get; set; }
        public IEnumerable<int> GroupIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}