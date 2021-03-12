using System;
using System.Collections.Generic;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
