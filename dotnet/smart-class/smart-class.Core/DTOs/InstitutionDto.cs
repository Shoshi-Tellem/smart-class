using smart_class.Core.Entities;
using System;
using System.Collections.Generic;

namespace smart_class.Core.DTOs
{
    public class InstitutionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<Admin> Admins { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}