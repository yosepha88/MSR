using System;
using System.Collections.Generic;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class User
    {
        public User()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
