using smart_class.Core.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smart_class.Core.Entities;
using File = smart_class.Core.Entities.File;

namespace smart_class.Core.Classes
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Institution, InstitutionDto>().ReverseMap();
            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
        }
    }
}
