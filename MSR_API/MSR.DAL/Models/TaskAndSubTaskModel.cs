using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.DAL.Models
{
    public class TaskAndSubTaskModel
    {
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
        public int TaskId { get; set; }
        public string SubTaskDescription { get; set; }
        public int JiraTicketId { get; set; }

    }
}
