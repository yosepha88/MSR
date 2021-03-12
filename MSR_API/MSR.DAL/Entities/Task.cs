using System;
using System.Collections.Generic;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class Task
    {
        public Task()
        {
            SubTasks = new HashSet<SubTask>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int MonthId { get; set; }
        public int Year { get; set; }
        public int? Type { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual TaskStatus Status { get; set; }
        public virtual ICollection<SubTask> SubTasks { get; set; }
    }
}
