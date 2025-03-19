using smart_class.Core.Classes;
using System;

namespace smart_class.Core.DTOs
{
    public class StudentDto : UserDto
    {
        public int GroupId { get; set; }
    }
}