using smart_class.Core.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smart_class.Core.Entities;

namespace smart_class.Core.Classes
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Admin, AdminDto>().ReverseMap();
        }
    }
}
