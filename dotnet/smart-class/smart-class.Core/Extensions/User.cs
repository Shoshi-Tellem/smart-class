using smart_class.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_class.Core.Classes
{
    public enum ERole { admin, teacher, student }
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool PasswordChanged { get; set; }
        public string Email { get; set; }
        [ForeignKey("Institution")]
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}