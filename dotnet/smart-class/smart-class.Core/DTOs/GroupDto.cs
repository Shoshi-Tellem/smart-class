using System;
using System.Collections.Generic;

namespace smart_class.Core.DTOs
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstitutionId { get; set; }
        public IEnumerable<int> StudentIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}