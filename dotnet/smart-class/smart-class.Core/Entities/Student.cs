using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smart_class.Core.Classes;

namespace smart_class.Core.Entities
{
    public class Student : User
    {

        //[ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}