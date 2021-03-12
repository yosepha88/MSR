using System;
using System.Collections.Generic;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class JiraTicket
    {
        public int Id { get; set; }
        public int SubTaskId { get; set; }
        public int JiraTicketId { get; set; }

        public virtual SubTask SubTask { get; set; }
    }
}
