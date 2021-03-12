using System;
using System.Collections.Generic;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class SubTask
    {
        public SubTask()
        {
            JiraTickets = new HashSet<JiraTicket>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int TaskId { get; set; }
        public bool IsActive { get; set; }

        public virtual Task Task { get; set; }
        public virtual ICollection<JiraTicket> JiraTickets { get; set; }
    }
}
